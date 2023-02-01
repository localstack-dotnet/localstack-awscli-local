/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park, SharpeRAD
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/Arguments/TextArgument.cs
*
***************************************************************************************/


namespace LocalStack.AwsLocal.ProcessCore.Arguments;

/// <summary>
/// Represents a text argument.
/// </summary>
public sealed class TextArgument : IProcessArgument
{
    private readonly string _text;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextArgument"/> class.
    /// </summary>
    /// <param name="text">The text.</param>
    public TextArgument(string text)
    {
        _text = text;
    }

    /// <summary>
    /// Render the arguments as a <see cref="System.String" />.
    /// Sensitive information will be included.
    /// </summary>
    /// <returns>
    /// A string representation of the argument.
    /// </returns>
    public string Render()
    {
        return _text ?? string.Empty;
    }

    /// <summary>
    /// Renders the argument as a <see cref="System.String" />.
    /// Sensitive information will be redacted.
    /// </summary>
    /// <returns>
    /// A safe string representation of the argument.
    /// </returns>
    public string RenderSafe()
    {
        return Render();
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