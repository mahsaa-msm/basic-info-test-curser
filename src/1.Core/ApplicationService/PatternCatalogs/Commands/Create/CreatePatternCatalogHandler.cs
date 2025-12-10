using Master.Data.Core.Contracts.PatternCatalogs.Commands;
using Master.Data.Core.Domain.PatternCatalogs.Entities;
using Master.Data.Core.Domain.PatternCatalogs.ValueObjects;
using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.PatternCatalogs.Commands.Create;

public sealed class CreatePatternCatalogHandler : CommandHandler<CreatePatternCatalogCommand, long>
{
    private readonly IPatternCatalogCommandRepository _patternCatalogCommandRepository;

    public CreatePatternCatalogHandler(ZaminServices zaminServices,
                                       IPatternCatalogCommandRepository patternCatalogCommandRepository)
        : base(zaminServices)
    {
        _patternCatalogCommandRepository = patternCatalogCommandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreatePatternCatalogCommand command)
    {
        var isDuplicatePatternCatalog = await _patternCatalogCommandRepository
            .ExistsAsync(e => e.Key == PatternKey.FromString(command.Key));

        if (isDuplicatePatternCatalog)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.PATTERN_KEY]);

        long nextPriority = await _patternCatalogCommandRepository.GetNextPriority();

        var patternCatalog = PatternCatalog.Create(command.ToCreateParameter(nextPriority));

        await _patternCatalogCommandRepository.InsertAsync(patternCatalog);

        await _patternCatalogCommandRepository.CommitAsync();

        return Ok(patternCatalog.Id);
    }
}