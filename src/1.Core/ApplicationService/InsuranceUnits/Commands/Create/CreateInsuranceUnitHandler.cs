using Master.Data.Core.Contracts.InsuranceUnits.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.Entities;
using Master.Data.Core.RequestResponse.InsuranceUnits.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceUnits.Commands.Create;

public sealed class CreateInsuranceUnitHandler : CommandHandler<CreateInsuranceUnitCommand, long>
{
    private readonly IInsuranceUnitCommandRepository _insuranceUnitCommandRepository;
    private readonly Dictionary<bool, Func<CreateInsuranceUnitCommand, long, InsuranceUnit, Task<InsuranceUnit>>> _actions;

    public CreateInsuranceUnitHandler(ZaminServices zaminServices,
                                      IInsuranceUnitCommandRepository insuranceUnitCommandRepository)
        : base(zaminServices)
    {
        _insuranceUnitCommandRepository = insuranceUnitCommandRepository;
        _actions = new()
        {
            [true] = Create,
            [false] = Restore,
        };
    }

    public override async Task<CommandResult<long>> Handle(CreateInsuranceUnitCommand command)
    {
        var isDuplicateInsuranceUnit = await _insuranceUnitCommandRepository
        .ExistsAsync(e => DIPTitle.FromString(command.Title).Equals(e.Title) ||
                          DIPTitle.FromString(command.Name).Equals(e.Name) ||
                          Code.FromString(command.Code).Equals(e.Code) ||
                          CoreId.FromString(command.CoreId).Equals(e.CoreId));

        if (isDuplicateInsuranceUnit)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        InsuranceUnit? insuranceUnit = await _insuranceUnitCommandRepository.GetByCoreIdIgnoreQueryFiltersAsync(command.CoreId);

        long nextPriority = await _insuranceUnitCommandRepository.GetNextPriority();

        insuranceUnit = await _actions[insuranceUnit is null](command, nextPriority, insuranceUnit);

        await _insuranceUnitCommandRepository.CommitAsync();

        return Ok(insuranceUnit.Id);
    }

    #region Methods
    private async Task<InsuranceUnit> Create(CreateInsuranceUnitCommand command, long nextPriority, InsuranceUnit? insuranceUnit)
    {
        insuranceUnit = InsuranceUnit.Create(command.ToCreateParameter(nextPriority));

        await _insuranceUnitCommandRepository.InsertAsync(insuranceUnit);

        return insuranceUnit;
    }

    private async Task<InsuranceUnit> Restore(CreateInsuranceUnitCommand command, long nextPriority, InsuranceUnit? insuranceUnit)
    {
        insuranceUnit?.Restore(command.ToRestoreParameter(nextPriority));

        return insuranceUnit;
    }
    #endregion
}