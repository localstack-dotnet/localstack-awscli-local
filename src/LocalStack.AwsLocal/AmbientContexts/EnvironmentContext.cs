using System;

namespace LocalStack.AwsLocal.AmbientContexts
{
    public abstract class EnvironmentContext
    {
        private static EnvironmentContext _current = DefaultEnvironmentContext.Instance;

        public static EnvironmentContext Current
        {
            get => _current;

            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract void Exit(int value);

        public static void ResetToDefault()
        {
            _current = DefaultEnvironmentContext.Instance;
        }
    }
}
