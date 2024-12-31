using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;

namespace BookWise.Api;

public class LambdaEntryPoint : APIGatewayProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder
            .UseStartup<Startup>();
    }

    public override Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
    {
        Environment.SetEnvironmentVariable("LAMBDA_VERSION", lambdaContext.FunctionVersion);

        return base.FunctionHandlerAsync(request, lambdaContext);
    }

    protected override void Init(IHostBuilder builder)
    {
    }
}