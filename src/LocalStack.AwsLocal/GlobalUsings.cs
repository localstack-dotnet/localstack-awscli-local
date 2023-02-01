global using System;
global using System.Collections.Generic;
global using LocalStack.AwsLocal.ProcessCore.IO;
global using LocalStack.AwsLocal.ProcessCore;
global using System.IO;
global using System.Linq;
global using LocalStack.Client.Contracts;
global using LocalStack.Client.Models;
global using LocalStack.AwsLocal.Contracts;
global using System.Globalization;
global using LocalStack.AwsLocal.ProcessCore.Arguments;
global using LocalStack.AwsLocal.Extensions;
global using System.Reflection;
global using System.Runtime.InteropServices;
global using LocalStack.AwsLocal.AmbientContexts;
global using System.Collections.Concurrent;
global using System.Diagnostics;
global using System.Collections;
global using System.Text;
global using LocalStack.Client.Enums;
global using System.Diagnostics.CodeAnalysis;
global using LocalStack.Client.Options;
global using LocalStack.Client;

#if NETCOREAPP3_1
namespace System.Runtime.CompilerServices
{
    using System.ComponentModel;
    /// <summary>
    /// Reserved to be used by the compiler for tracking metadata.
    /// This class should not be used by developers in source code.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit
    {
    }
}
#endif