namespace LocalStack.AwsLocal.Tests.Mocks;

public class EnvironmentContextMock : EnvironmentContext
{
    private int _exitValue;

    public override void Exit(int value)
    {
        _exitValue = value;
    }

    public int ExitValue => _exitValue;
}