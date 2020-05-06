/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, Dave Glick
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/PathCollapser.cs
*
***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalStack.AwsLocal.ProcessCore.IO
{
    internal static class PathCollapser
    {
        public static string Collapse(Path path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            var stack = new Stack<string>();
            var segments = path.FullPath.Split('/', '\\');
            foreach (string segment in segments)
            {
                switch (segment)
                {
                    case ".":
                        continue;
                    case "..":
                    {
                        if (stack.Count > 1)
                        {
                            stack.Pop();
                        }
                        continue;
                    }
                    default:
                        stack.Push(segment);
                        break;
                }
            }
            string collapsed = string.Join("/", stack.Reverse());
            return collapsed == string.Empty ? "." : collapsed;
        }
    }
}