using Grpc.Core;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Master.Data.Core.Resources;
using Zamin.Core.Contracts.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Common;
using Zamin.Extensions.Translations.Abstractions;

namespace Master.Data.Endpoints.API.Features.ServiceFeatures.Grpc;

public sealed class ServiceFeatureGrpcService : ServiceFeatureService.ServiceFeatureServiceBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ITranslator _translator;

    public ServiceFeatureGrpcService(IQueryDispatcher queryDispatcher,
                                     ITranslator translator)
    {
        _queryDispatcher = queryDispatcher;
        _translator = translator;
    }

    public override async Task<GetServiceFeatureByIdResponse> GetServiceFeatureById(GetServiceFeatureByIdRequest request,
                                                                                    ServerCallContext context)
    {
        var query = new GetServiceFeatureByIdQuery
        {
            ServiceFeatureId = request.ServiceFeatureId,
        };

        var queryResult = await _queryDispatcher.Execute<GetServiceFeatureByIdQuery, ServiceFeatureQr?>(query);

        if (queryResult is null ||
            queryResult.Status != ApplicationServiceStatus.Ok)
        {
            string message = queryResult switch
            {
                null => string.Format(_translator[ProjectTranslation.APPLICATION_ERROR_UNACCEPTED_ERROR_OCCURRED_IN],
                                      nameof(GetServiceFeatureByIdQuery)),

                { Data: null } => string.Format(_translator[ProjectValidationError.VALIDATION_ERROR_NOT_EXIST_ANY],
                                                ProjectTranslation.SERVICE_FEATURE),

                { Messages: var messages } when messages?.Any() == true => string.Join(", ", messages),

                _ => string.Format(_translator[ProjectTranslation.APPLICATION_ERROR_UNACCEPTED_ERROR_OCCURRED_IN],
                                      nameof(GetServiceFeatureByIdQuery)),
            };

            return new GetServiceFeatureByIdResponse
            {
                Error = new ErrorResponse
                {
                    Status = queryResult?.Status.ToString(),
                    Messages = { message }
                }
            };
        }

        var serviceFeature = queryResult.Data;

        return new GetServiceFeatureByIdResponse
        {
            Data = new ServiceFeatureData
            {
                Id = serviceFeature!.Id,
                Key = (ServiceFeatureKey)serviceFeature!.Key,
                KeyTitle = _translator[serviceFeature!.KeyTitle],
                ServiceName = serviceFeature!.ServiceName,
                FeatureName = serviceFeature!.FeatureName,
                Description = serviceFeature!.Description,
                IsActive = serviceFeature!.IsActive
            }
        };
    }
}
