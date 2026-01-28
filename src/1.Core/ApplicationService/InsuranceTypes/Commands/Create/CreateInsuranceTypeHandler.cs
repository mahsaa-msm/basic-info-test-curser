using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Commands.Create;

public class CreateInsuranceTypeHandler : CommandHandler<CreateInsuranceTypeCommand, long>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;
    private readonly Dictionary<bool, Func<CreateInsuranceTypeCommand, long, InsuranceType, Task<InsuranceType>>> _actions;

    public CreateInsuranceTypeHandler(ZaminServices zaminServices,
                                IInsuranceTypeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _actions = new()
        {
            [true] = async (command, nextPriority, insuranceType) => await Create(command, nextPriority, insuranceType),
            [false] = async (command, nextPriority, insuranceType) => await Restore(command, nextPriority, insuranceType),
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateInsuranceTypeCommand command)
    {
        var isDuplicateInsuranceType = await _commandRepository
            .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                              Code.FromString(command.Code).Equals(e.Code) ||
                              CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateInsuranceType)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        InsuranceType? insuranceType = await _commandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _commandRepository.GetNextPriority();

        insuranceType = await _actions[insuranceType is null](command, nextPriority, insuranceType);

        await _commandRepository.CommitAsync();

        return Ok(insuranceType.Id);
    }

    #region Methods
    private async Task<InsuranceType> Create(CreateInsuranceTypeCommand command, long nextPriority, InsuranceType? insuranceType)
    {
        insuranceType = InsuranceType.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(insuranceType);

        return insuranceType;
    }

    private async Task<InsuranceType> Restore(CreateInsuranceTypeCommand command, long nextPriority, InsuranceType? insuranceType)
    {
        insuranceType?.Restore(command.ToRestoreParameter(nextPriority));

        return insuranceType;
    }
    #endregion
}