/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/Polyfill/ProcessHelper.cs
*
***************************************************************************************/

namespace LocalStack.AwsLocal.ProcessCore;

internal static class ProcessHelper
{
    public static void SetEnvironmentVariable(ProcessStartInfo info, string key, string value)
    {
        string envKey = info.Environment.Keys.FirstOrDefault(existingKey => StringComparer.OrdinalIgnoreCase.Equals(existingKey, key)) ?? key;
        info.Environment[envKey] = value;
    }
}