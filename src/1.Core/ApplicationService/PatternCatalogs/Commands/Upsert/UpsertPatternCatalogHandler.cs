using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Commands;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.Entities;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;
using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Upsert;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.PatternCatalogs.Commands.Upsert;

public sealed class UpsertPatternCatalogHandler : CommandHandler<UpsertPatternCatalogCommand, long?>
{
    private readonly IPatternCatalogCommandRepository _patternCatalogCommandRepository;
    private readonly ICommandDispatcher _commandDispatcher;

    public UpsertPatternCatalogHandler(ZaminServices zaminServices,
                                       IPatternCatalogCommandRepository patternCatalogCommandRepository,
                                       ICommandDispatcher commandDispatcher)
        : base(zaminServices)
    {
        _patternCatalogCommandRepository = patternCatalogCommandRepository;
        _commandDispatcher = commandDispatcher;
    }

    public override async Task<CommandResult<long?>> Handle(UpsertPatternCatalogCommand command)
    {
        var patternCatalog = await _patternCatalogCommandRepository.GetByKeyAsync(PatternKey.FromString(command.Key));

        if (patternCatalog is null)
        {
            long nextPriority = await _patternCatalogCommandRepository.GetNextPriority();
            patternCatalog = PatternCatalog.Create(command.ToCreateParameter(nextPriority));

            await _patternCatalogCommandRepository.InsertAsync(patternCatalog);
            await _patternCatalogCommandRepository.CommitAsync();

            return Ok(patternCatalog.Id);
        }
        else
        {
            var updateResult = await _commandDispatcher.Send(new UpdatePatternCatalogCommand
            {
                PatternCatalogId = patternCatalog.Id,
                Pattern = command.Pattern,
                Type = command.Type,
                Description = patternCatalog.Description?.Value,
                Priority = patternCatalog.Priority.Value,
            });

            result.AddMessages(updateResult.Messages);

            if (updateResult.Status == Zamin.Core.RequestResponse.Common.ApplicationServiceStatus.Ok)
                return Ok(patternCatalog.Id);
            return Result(null, updateResult.Status);
        }


    }
}
