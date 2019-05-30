# LocalStack.NET AWS CLI

This .NET Core global tool provides the `awslocal` command, which is a thin wrapper around the `aws`
command line interface for use with [LocalStack](https://github.com/localstack/localstack). This tool is a .NET Core port of the 
[LocalStack AWS CLI](https://github.com/localstack/awscli-local) for the people who have experienced issues with LocalStack AWS CLI.

## Installation

You can install the `awslocal` command via `.NET Core CLI`:

```
dotnet tool install --global LocalStack.AwsLocal
```

## Usage

The `awslocal` command has the same usage as the `aws` command. For detailed usage,
please refer to the man pages of `aws help`.

## Example

Instead of the following command ...

```
aws --endpoint-url=http://localhost:4568 kinesis list-streams
```

... you can simply use this:

```
awslocal kinesis list-streams
```

## Configurations

You can use the following environment variables for configuration:

* `LOCALSTACK_HOST`: Set the hostname for the localstack instance. Useful when you have
localstack is bound to another interface (i.e. docker-machine).
* `USE_SSL`: Whether to use `https` endpoint URLs (required if LocalStack has been started
with `USE_SSL=true` enabled). Defaults to `false`.

<!-- ## Change Log

* v0.4: Minor fix for Python 3 compatibility -->

## <a name="license"></a> License

Licensed under MIT, see [LICENSE](LICENSE) for the full text.

