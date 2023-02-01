![Nuget](https://img.shields.io/nuget/dt/LocalStack.AwsLocal) [![NuGet](https://img.shields.io/nuget/v/LocalStack.AwsLocal.svg)](https://www.nuget.org/packages/LocalStack.AwsLocal/)

# LocalStack.NET AWS CLI

This .NET Core global tool provides the `awslocal` command, which is a thin wrapper around the `aws`
command line interface for use with [LocalStack](https://github.com/localstack/localstack). This tool is a .NET Core port of the 
[LocalStack AWS CLI](https://github.com/localstack/awscli-local) for the people who have experienced issues with LocalStack AWS CLI.

## Continuous integration

| Build server     | Platform  | Build status                                                                                                                                                                                                                                                                          |
|----------------- |---------- |-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Github Actions  | Ubuntu    | [![build-ubuntu](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-ubuntu.yml/badge.svg)](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-ubuntu.yml)  |
| Github Actions   | Windows    | [![build-windows](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-windows.yml/badge.svg)](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-windows.yml)  |
| Github Actions   | macOS    | [![build-macos](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-macos.yml/badge.svg)](https://github.com/localstack-dotnet/localstack-awscli-local/actions/workflows/build-macos.yml) |

## Installation

You can install the `awslocal` command via `.NET Core CLI`:

```
dotnet tool install --global LocalStack.AwsLocal
```

| Stable                                                                                                              | Nightly                                                                                                                                                                        |
|---------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [![NuGet](https://img.shields.io/nuget/v/LocalStack.AwsLocal.svg)](https://www.nuget.org/packages/LocalStack.AwsLocal/) | [![MyGet](https://img.shields.io/myget/localstack-awscli-local/v/LocalStack.AwsLocal.svg?label=myget)](https://www.myget.org/feed/localstack-awscli-local/package/nuget/LocalStack.AwsLocal) |

## Usage

The `awslocal` command has the same usage as the `aws` command. For detailed usage,
please refer to the man pages of `aws help`.

## Example

Instead of the following command ...

```
aws --endpoint-url=http://localhost:4566 kinesis list-streams
```

... you can simply use this:

```
awslocal kinesis list-streams
```

## Configurations

You can use the following environment variables for configuration:

* `LOCALSTACK_HOST`: Set the hostname for the localstack instance. Useful when you have
localstack is bound to another interface (i.e. docker-machine). Defaults to `localhost`.
* `USE_SSL`: Whether to use `https` endpoint URLs (required if LocalStack has been started
with `USE_SSL=true` enabled). Defaults to `false`.
* `USE_LEGACY_PORTS`: Whether to use old endpoint ports. Starting with LocalStack releases after `v0.11.5`, all services are now exposed via the edge service (port 4566) only! If you are using a version of LocalStack lower than v0.11.5, you should set `USE_LEGACY_PORTS` to `true`. Defaults to `false`.
* `EDGE_PORT`: Set the edge port. Edge port can be set to any available port ([see LocalStack configuration section](https://github.com/localstack/localstack#configurations)). If you have made such a change in LocalStack's configuration, be sure to set the same port value to `EDGE_PORT`. Defaults to `4566`.
* `DEFAULT_REGION`: Set the default region. Overrides `AWS_DEFAULT_REGION` environment variable.

## Changelog

### [v1.3.3](https://github.com/localstack-dotnet/localstack-awscli-local/releases/tag/v1.3.3) 
- Support for new endpoints in the official [Localstack Python Client](https://github.com/localstack/localstack-python-client) v1.2.2 have been added.
   - EFS, Backup, LakeFormation, WAF, WAF V2 and QLDB Session

### [v1.3.0](https://github.com/localstack-dotnet/localstack-awscli-local/releases/tag/v1.3.0) 
- Add .NET 5.0 support
- Default port set to 4566
- DEFAULT_REGION support

### [v1.2.0](https://github.com/localstack-dotnet/localstack-awscli-local/releases/tag/v1.2.0) 
- Add .NET Core 2.1 support
- Fix "Quotation Marks with Strings" issues when passing JSON as argument
See details : https://docs.aws.amazon.com/cli/latest/userguide/cli-usage-parameters-quoting-strings.html

## <a name="license"></a> License

Licensed under MIT, see [LICENSE](LICENSE) for the full text.