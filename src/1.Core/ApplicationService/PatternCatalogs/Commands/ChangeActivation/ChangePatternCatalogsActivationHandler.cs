using Master.Data.Core.Contracts.PatternCatalogs.Commands;
using Master.Data.Core.Domain.Common.Guards;
using Master.Data.Core.Domain.PatternCatalogs.Entities;
using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.ChangeActivation;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.PatternCatalogs.Commands.ChangeActivation;

public sealed class ChangePatternCatalogsActivationHandler : CommandHandler<ChangePatternCatalogsActivationCommand>
{
    private readonly IPatternCatalogCommandRepository _patternCatalogCommandRepository;
    private static readonly Dictionary<bool, Action<List<PatternCatalog>>> _actions = new()
    {
        [true] = c => c.ForEach(c => c.Active()),
        [false] = c => c.ForEach(c => c.Deactive())
    };

    public ChangePatternCatalogsActivationHandler(ZaminServices zaminServices,
                                         IPatternCatalogCommandRepository patternCatalogCommandRepository)
        : base(zaminServices)
    {
        _patternCatalogCommandRepository = patternCatalogCommandRepository;
    }

    public override async Task<CommandResult> Handle(ChangePatternCatalogsActivationCommand command)
    {
        List<PatternCatalog> patternCatalogs = await _patternCatalogCommandRepository.GetByIds(command.PatternCatalogsId);

        EntityGuard.ThrowIfListIsEmptyWithLongId(patternCatalogs, ProjectTranslation.CITY);

        _actions[command.IsActive](patternCatalogs);
        await _patternCatalogCommandRepository.CommitAsync();

        return Ok();

    }
}
