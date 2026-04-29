using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions.Grpc.Interceptors;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Extensions.Grpc;

public static class GrpcServiceExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services)
    {
        services.AddTransient<LoggingInterceptor>();
        services.AddTransient<ExceptionInterceptor>();

        //#region Factor
        //services.AddGrpcClient<FactorService.FactorServiceClient>((provider, options) =>
        //{
        //    var grpcOption = provider.GetRequiredService<GrpcOption>();
        //    var factorsGrpcServer = grpcOption.GrpcServers.FirstOrDefault(c => c.ServerName == ProjectConsts.FACTORS_GRPC_CLIENT_NAME);
        //    options.Address = new Uri(factorsGrpcServer?.ServerBaseAddress ?? string.Empty);
        //})
        //.ConfigurePrimaryHttpMessageHandler(() =>
        //{
        //    var handler = new HttpClientHandler();
        //    handler.ServerCertificateCustomValidationCallback =
        //        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        //    return handler;
        //})
        //.AddInterceptor<ExceptionInterceptor>()
        //.AddInterceptor<LoggingInterceptor>()
        //.AddInterceptor(provider =>
        //{
        //    var grpcOption = provider.GetRequiredService<GrpcOption>();
        //    var logger = provider.GetRequiredService<ILogger<ApiKeyInterceptor>>();
        //    return new ApiKeyInterceptor(grpcOption, logger, ProjectConsts.FACTORS_GRPC_CLIENT_NAME);
        //});
        //#endregion

        //// پیکربندی Polly برای retry (اختیاری)
        //services.AddPolicyRegistry()
        //    .Add("grpcRetryPolicy", Policy.Handle<RpcException>(ex =>
        //            ex.StatusCode == StatusCode.Unavailable ||
        //            ex.StatusCode == StatusCode.DeadlineExceeded)
        //        .WaitAndRetryAsync(3, retryAttempt =>
        //            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))));

        return services;
    }
}
