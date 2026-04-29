using Vehicle.Insurance.Core.Contracts.InsuranceTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.InsuranceTypes.Entities;
using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.InsuranceTypes.Commands.Delete;

public class DeleteInsuranceTypeHandler : CommandHandler<DeleteInsuranceTypeCommand>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;

    public DeleteInsuranceTypeHandler(ZaminServices zaminServices,
                                IInsuranceTypeCommandRepository commandRepository)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteInsuranceTypeCommand command)
    {
        var insuranceType = await _commandRepository.GetAsync(command.InsuranceTypeId);
        EntityGuard.ThrowIfNull<InsuranceType, long>(insuranceType, ProjectTranslation.INSURANCE_TYPE);

        insuranceType.Delete();

        var subordinates = await _commandRepository.GetSubordinateInsuranceTypes(insuranceType.Priority);

        subordinates?.ForEach(insuranceType => insuranceType.PullUp());

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
