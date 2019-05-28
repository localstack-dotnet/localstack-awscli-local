using System.Linq;
using LocalStack.Client.Contracts;
using LocalStack.Client.Models;

namespace LocalStack.AwsLocal.Extensions
{
    public static class ConfigExtensions
    {
        public static AwsServiceEndpoint GetServiceEndpoint(this IConfig config, string serviceName)
        {
            var awsServiceEndpoints = config.GetAwsServiceEndpoints();

            return awsServiceEndpoints.SingleOrDefault(endpoint => endpoint.CliName == serviceName);
        }
    }
}
