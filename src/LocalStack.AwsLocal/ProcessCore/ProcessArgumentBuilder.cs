﻿/***************************************************************************************
*    Title: cake-build/cake
*    Author: Mattias Karlsson, Patrik Svensson, Gary Ewan Park
*    Date: 01.05.2020
*    Code version: commit: 0c46856abe976f6c122f05a5e82ccb854fd923c1
*    Availability: https://github.com/cake-build/cake/blob/develop/src/Cake.Core/IO/ProcessArgumentBuilder.cs
*
***************************************************************************************/


namespace LocalStack.AwsLocal.ProcessCore;

/// <summary>
/// Utility for building process arguments.
/// </summary>
public sealed class ProcessArgumentBuilder : IReadOnlyCollection<IProcessArgument>
{
    private readonly List<IProcessArgument> _tokens;

    /// <summary>
    /// Gets the number of arguments contained in the <see cref="ProcessArgumentBuilder"/>.
    /// </summary>
    public int Count => _tokens.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessArgumentBuilder"/> class.
    /// </summary>
    public ProcessArgumentBuilder()
    {
        _tokens = new List<IProcessArgument>();
    }

    /// <summary>
    /// Clears all arguments from the builder.
    /// </summary>
    public void Clear()
    {
        _tokens.Clear();
    }

    /// <summary>
    /// Appends an argument.
    /// </summary>
    /// <param name="argument">The argument.</param>
    public void Append(IProcessArgument argument)
    {
        _tokens.Add(argument);
    }

    /// <summary>
    /// Prepends an argument.
    /// </summary>
    /// <param name="argument">The argument.</param>
    public void Prepend(IProcessArgument argument)
    {
        _tokens.Insert(0, argument);
    }

    /// <summary>
    /// Renders the arguments as a <see cref="string"/>.
    /// Sensitive information will be included.
    /// </summary>
    /// <returns>A string representation of the arguments.</returns>
    public string Render()
    {
        return string.Join(" ", _tokens.Select(t => t.Render()));
    }

    /// <summary>
    /// Renders the arguments as a <see cref="string"/>.
    /// Sensitive information will be redacted.
    /// </summary>
    /// <returns>A safe string representation of the arguments.</returns>
    public string RenderSafe()
    {
        return string.Join(" ", _tokens.Select(t => t.RenderSafe()));
    }

    /// <summary>
    /// Tries to filer any unsafe arguments from string
    /// </summary>
    /// <param name="source">unsafe source string.</param>
    /// <returns>Filtered string.</returns>
    public string FilterUnsafe(string source)
    {
        if (string.IsNullOrWhiteSpace(source))
        {
            return source;
        }

        return _tokens
            .Select(token => new
            {
                Safe = token.RenderSafe().Trim('"').Trim(),
                Unsafe = token.Render().Trim('"').Trim()
            })
            .Where(token => token.Safe != token.Unsafe)
            .Aggregate(
                new StringBuilder(source),
                (sb, token) => sb.Replace(token.Unsafe, token.Safe),
                sb => sb.ToString());
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="ProcessArgumentBuilder"/>.
    /// </summary>
    /// <param name="value">The text value to convert.</param>
    /// <returns>A <see cref="FilePath"/>.</returns>
    public static implicit operator ProcessArgumentBuilder(string value)
    {
        return FromString(value);
    }

    /// <summary>
    /// Performs a conversion from <see cref="System.String"/> to <see cref="ProcessArgumentBuilder"/>.
    /// </summary>
    /// <param name="value">The text value to convert.</param>
    /// <returns>A <see cref="FilePath"/>.</returns>
    public static ProcessArgumentBuilder FromString(string value)
    {
        var builder = new ProcessArgumentBuilder();
        builder.Append(new TextArgument(value));
        return builder;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    IEnumerator<IProcessArgument> IEnumerable<IProcessArgument>.GetEnumerator()
    {
        return _tokens.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_tokens).GetEnumerator();
    }
}