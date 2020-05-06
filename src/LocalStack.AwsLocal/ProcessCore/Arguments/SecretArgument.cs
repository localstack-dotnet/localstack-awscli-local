/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, SharpeRAD
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/Arguments/SecretArgument.cs
*
***************************************************************************************/

using LocalStack.AwsLocal.Contracts;

namespace LocalStack.AwsLocal.ProcessCore.Arguments
{
    public sealed class SecretArgument : IProcessArgument
    {
        private readonly IProcessArgument _argument;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretArgument"/> class.
        /// </summary>
        /// <param name="argument">The argument.</param>
        public SecretArgument(IProcessArgument argument)
        {
            _argument = argument;
        }

        /// <summary>
        /// Render the arguments as a <see cref="System.String" />.
        /// The secret text will be included.
        /// </summary>
        /// <returns>A string representation of the argument.</returns>
        public string Render()
        {
            return _argument.Render();
        }

        /// <summary>
        /// Renders the argument as a <see cref="System.String" />.
        ///  The secret text will be redacted.
        /// </summary>
        /// <returns>A safe string representation of the argument.</returns>
        public string RenderSafe()
        {
            return "[REDACTED]";
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return RenderSafe();
        }
    }
}
