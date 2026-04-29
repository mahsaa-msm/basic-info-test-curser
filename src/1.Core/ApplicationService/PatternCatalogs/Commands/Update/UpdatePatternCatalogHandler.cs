using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Commands;
using Vehicle.Insurance.Core.Domain.Common.Guards;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.Entities;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.ApplicationService.PatternCatalogs.Commands.Update;

public sealed class UpdatePatternCatalogHandler : CommandHandler<UpdatePatternCatalogCommand>
{
    private readonly IPatternCatalogCommandRepository _patternCatalogCommandRepository;
    private readonly Dictionary<MoveDirection, Func<PatternCatalog, UpdatePatternCatalogCommand, Task>> _actions;

    public UpdatePatternCatalogHandler(ZaminServices zaminServices,
                                       IPatternCatalogCommandRepository patternCatalogCommandRepository)
        : base(zaminServices)
    {
        _patternCatalogCommandRepository = patternCatalogCommandRepository;
        _actions = new()
        {
            [MoveDirection.Up] = async (patternCatalog, command)
            => (await _patternCatalogCommandRepository.GetSuperiorPatternCatalogs(patternCatalog.Priority, command.Priority)).ForEach(c => c.PushDown()),
            [MoveDirection.Down] = async (patternCatalog, command)
            => (await _patternCatalogCommandRepository.GetSubordinatePatternCatalogs(patternCatalog.Priority, command.Priority)).ForEach(c => c.PullUp()),
        };
    }

    public override async Task<CommandResult> Handle(UpdatePatternCatalogCommand command)
    {
        PatternCatalog patternCatalog = await _patternCatalogCommandRepository.GetAsync(command.PatternCatalogId);

        EntityGuard.ThrowIfNullWithLongId(patternCatalog, ProjectTranslation.PATTERN_CATALOG);

        await CheckPriority(command);

        await MovePatternCatalogsIfNeeded(patternCatalog, command);

        patternCatalog.Update(command.ToUpdateParameter());

        await _patternCatalogCommandRepository.CommitAsync();

        return Ok();
    }

    #region Methods
    private async Task CheckPriority(UpdatePatternCatalogCommand command)
    {
        var nextPriority = await _patternCatalogCommandRepository.GetNextPriority();
        if (command.Priority > nextPriority - 1)
            command.Priority = nextPriority - 1;
    }
    private async Task MovePatternCatalogsIfNeeded(PatternCatalog current, UpdatePatternCatalogCommand command)
    {
        var moveDirection = current.GetMoveDirection(command.Priority);
        if (moveDirection != MoveDirection.NoChange)
        {
            await _actions[moveDirection](current, command);
        }
    }
    #endregion
}

