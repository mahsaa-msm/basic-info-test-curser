using Master.Data.Core.Contracts.IssuanceSchemes.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.IssuanceSchemes.Entities;
using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Create;
using Master.Data.Core.Resources;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.IssuanceSchemes.Commands.Create;

public class CreateIssuanceSchemeHandler : CommandHandler<CreateIssuanceSchemeCommand, long>
{
    private readonly IIssuanceSchemeCommandRepository _commandRepository;

    public CreateIssuanceSchemeHandler(ZaminServices zaminServices,
                                IIssuanceSchemeCommandRepository commandRepository) : base(zaminServices)
    {
        _commandRepository = commandRepository;
    }

    public override async Task<CommandResult<long>> Handle(CreateIssuanceSchemeCommand command)
    {
        var isDuplicateIssuanceScheme = await _commandRepository
            .ExistsAsync(e => e.Title == Title.FromString(command.Title) ||
                              e.Code == Code.FromString(command.Code) ||
                              e.CoreId == CoreId.FromString(command.CoreId));

        if (isDuplicateIssuanceScheme)
            throw new DuplicateWaitObjectException(_zaminServices.Translator[ProjectValidationError.VALIDATION_ERROR_DUPLICATE,
                                                                             ProjectTranslation.NAME]);

        long nextPriority = await _commandRepository.GetNextPriority();

        var issuanceScheme = await Create(command, nextPriority);

        await _commandRepository.CommitAsync();

        return Ok(issuanceScheme.Id);
    }

    #region Methods
    private async Task<IssuanceScheme> Create(CreateIssuanceSchemeCommand command, long nextPriority)
    {
        var issuanceScheme = IssuanceScheme.Create(command.ToCreateParameter(nextPriority));

        await _commandRepository.InsertAsync(issuanceScheme);

        return issuanceScheme;
    }

    #endregion
}