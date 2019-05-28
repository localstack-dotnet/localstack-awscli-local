using System;

namespace LocalStack.AwsLocal.EnvironmentContext
{
    public class DefaultEnvironmentControl : EnvironmentControl
    {
        private static readonly Lazy<DefaultEnvironmentControl> LazyInstance = new Lazy<DefaultEnvironmentControl>(() => new DefaultEnvironmentControl());

        public override void Exit(int value)
        {
            Environment.Exit(value);
        }

        public static DefaultEnvironmentControl Instance => LazyInstance.Value;
    }
}