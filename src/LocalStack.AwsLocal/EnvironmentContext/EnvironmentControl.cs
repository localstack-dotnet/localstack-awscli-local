using System;

namespace LocalStack.AwsLocal.EnvironmentContext
{
    public abstract class EnvironmentControl
    {
        private static EnvironmentControl _current = DefaultEnvironmentControl.Instance;

        public static EnvironmentControl Current
        {
            get => _current;

            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract void Exit(int value);

        public static void ResetToDefault()
        {
            _current = DefaultEnvironmentControl.Instance;
        }
    }
}
