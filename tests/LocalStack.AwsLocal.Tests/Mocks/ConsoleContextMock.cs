namespace LocalStack.AwsLocal.Tests.Mocks;

public class ConsoleContextMock : ConsoleContext
{
    private string _text;

    public override void WriteLine(string text)
    {
        _text = text;
    }

    public string Text => _text;
}