using Vehicle.Insurance.Core.Contracts.InsuranceUnits.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.Entities;
using Vehicle.Insurance.Core.RequestResponse.InsuranceUnits.Commands.Delete;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.InsuranceUnits.Commands.Delete;

public sealed class DeleteInsuranceUnitHandler : CommandHandler<DeleteInsuranceUnitCommand>
{
    private readonly IInsuranceUnitCommandRepository _insuranceUnitCommandRepository;

    public DeleteInsuranceUnitHandler(ZaminServices zaminServices,
                                      IInsuranceUnitCommandRepository insuranceUnitCommandRepository)
        : base(zaminServices)
    {
        _insuranceUnitCommandRepository = insuranceUnitCommandRepository;
    }

    public override async Task<CommandResult> Handle(DeleteInsuranceUnitCommand command)
    {
        var insuranceUnit = await _insuranceUnitCommandRepository.GetAsync(command.InsuranceUnitId);
        EntityGuard.ThrowIfNull<InsuranceUnit, long>(insuranceUnit, ProjectTranslation.INSURANCE_UNIT);

        insuranceUnit.Delete();

        var subordinates = await _insuranceUnitCommandRepository.GetSubordinateInsuranceUnits(insuranceUnit.Priority);

        subordinates?.ForEach(country => country.PullUp());

        await _insuranceUnitCommandRepository.CommitAsync();

        return Ok();
    }
}
