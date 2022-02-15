namespace NotNullReflection;

using System;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    private class NotLoaded
    {
    }

    /// <summary>
    /// Gets the placeholder for types that could not be loaded.
    /// </summary>
    public static Type TypeNotLoaded { get; } = typeof(NotLoaded);
}
