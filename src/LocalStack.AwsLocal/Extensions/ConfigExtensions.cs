namespace LocalStack.AwsLocal.Extensions;

public static class ConfigExtensions
{
    public static AwsServiceEndpoint GetServiceEndpoint(this IConfig config, string serviceName)
    {
        IEnumerable<AwsServiceEndpoint> awsServiceEndpoints = config.GetAwsServiceEndpoints();

        return awsServiceEndpoints.SingleOrDefault(endpoint => endpoint.CliName == serviceName);
    }
}