using System;

namespace LocalStack.AwsLocal.AmbientContexts
{
    public class DefaultEnvironmentContext : EnvironmentContext
    {
        private static readonly Lazy<DefaultEnvironmentContext> LazyInstance = new Lazy<DefaultEnvironmentContext>(() => new DefaultEnvironmentContext());

        public override void Exit(int value)
        {
            Environment.Exit(value);
        }

        public static DefaultEnvironmentContext Instance => LazyInstance.Value;
    }
}