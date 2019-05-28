using System.Collections.Generic;
using System.Linq;

namespace LocalStack.AwsLocal.Extensions
{
    public static class ArgumentExtensions
    {
        public static string ExtractServiceName(this IEnumerable<string> args)
        {
            foreach (string arg in args)
            {
                if (arg.StartsWith('-'))
                {
                    continue;
                }

                return arg == "s3api" ? "s3" : arg;
            }

            return string.Empty;
        }

        public static string GetCliCommand(this IEnumerable<string> args, string serviceUrl)
        {
            var arguments = args.ToList();
            arguments.Insert(0, "aws");
            arguments.Insert(1, $"--endpoint-url={serviceUrl}");

            if (serviceUrl.StartsWith("https"))
            {
                arguments.Insert(2, "--no-verify-ssl");
            }

            return string.Join(' ', arguments);
        }
    }
}
