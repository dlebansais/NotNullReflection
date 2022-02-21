namespace NotNullReflection;

using System;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Gets the default assembly resolver delegate.
    /// </summary>
    public static Func<AssemblyName, Assembly> DefaultAssemblyResolver { get; } = (AssemblyName name) => Assembly.GetExecutingAssembly();

    /// <summary>
    /// Gets the default type resolver delegate.
    /// </summary>
    public static Func<Assembly, string, bool, Type> DefaultTypeResolver { get; } = (Assembly assembly, string typeName, bool ignoreCase) => GetInstanceOfMissing();
}
