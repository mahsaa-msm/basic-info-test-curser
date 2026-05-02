using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Commands.Create;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.LicensePlateTypes.Commands.Create;

public sealed class CreateLicensePlateTypeHandler : CommandHandler<CreateLicensePlateTypeCommand, long>
{
    private readonly ILicensePlateTypeCommandRepository _commandRepository;

    public CreateLicensePlateTypeHandler(ZaminServices zaminServices,
        ILicensePlateTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateLicensePlateTypeCommand command)
    {
        var duplicate = await _commandRepository.ExistsAsync(e =>
            DIPTitle.FromString(command.Title).Equals(e.Title) ||
            CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (duplicate)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                ProjectTranslation.LICENSE_PLATE_TYPE]);

        var nextPriority = await _commandRepository.GetNextPriority();
        var entity = LicensePlateType.Create(command.ToCreateParameter(nextPriority));
        await _commandRepository.InsertAsync(entity);
        await _commandRepository.CommitAsync();
        return Ok(entity.Id);
    }
}
