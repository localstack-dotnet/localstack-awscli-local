﻿namespace LocalStack.AwsLocal.AmbientContexts;

public class DefaultConsoleContext : ConsoleContext
{
    private static readonly Lazy<DefaultConsoleContext> LazyInstance = new(() => new DefaultConsoleContext());

    public override void WriteLine(string text)
    {
        Console.WriteLine(text);
    }

    public static DefaultConsoleContext Instance => LazyInstance.Value;
}