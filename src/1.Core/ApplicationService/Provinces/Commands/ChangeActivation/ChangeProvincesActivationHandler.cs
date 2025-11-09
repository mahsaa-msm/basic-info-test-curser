using Master.Data.Core.Contracts.Provinces.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.Provinces.Entities;
using Master.Data.Core.RequestResponse.Provinces.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Provinces.Commands.ChangeActivation;

public class ChangeProvincesActivationHandler : CommandHandler<ChangeProvincesActivationCommand>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;
    private static readonly Dictionary<bool, Action<List<Province>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };
    public ChangeProvincesActivationHandler(ZaminServices zaminServices,
                                            IProvinceCommandRepository provinceCommandRepository)
        : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangeProvincesActivationCommand command)
    {
        List<Province> provinces = await _provinceCommandRepository.GetByIds(command.ProvincesId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(provinces, ProjectTranslation.PROVINCE);

        _actions[command.IsActive](provinces);
        await _provinceCommandRepository.CommitAsync();

        return Ok();

    }
}
