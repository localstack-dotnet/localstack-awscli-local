using System;

namespace LocalStack.AwsLocal.AmbientContexts
{
    public abstract class ConsoleContext
    {
        private static ConsoleContext _current = DefaultConsoleContext.Instance;

        public static ConsoleContext Current
        {
            get => _current;

            set => _current = value ?? throw new ArgumentNullException(nameof(value));
        }

        public abstract void WriteLine(string text);

        public static void ResetToDefault()
        {
            _current = DefaultConsoleContext.Instance;
        }
    }
}
