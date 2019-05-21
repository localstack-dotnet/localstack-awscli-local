# Depedencies

The dependencies are taken from the [LocalStack.Client](https://github.com/localstack-dotnet/localstack-dotnet-client), 
which is added as git submodule. The reason behind this approach is to keep the [LocalStack.AwsLocal](https://github.com/localstack-dotnet/localstack-awscli-local) library as simple and light as possible. 
What we need is just a few enums and utily classes.