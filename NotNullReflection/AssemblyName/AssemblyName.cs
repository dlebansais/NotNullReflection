namespace NotNullReflection;

using System;
using System.Diagnostics;
using OriginAssemblyName = System.Reflection.AssemblyName;

/// <summary>
/// Describes an assembly's unique identity in full.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class AssemblyName : ICloneable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssemblyName"/> class.
    /// </summary>
    /// <param name="origin">The origin assembly name.</param>
    internal AssemblyName(OriginAssemblyName origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin assembly name for which this class is a proxy.
    /// </summary>
    public OriginAssemblyName Origin { get; }

    /// <summary>
    /// Makes a copy of this <see cref="AssemblyName"/> object.
    /// </summary>
    /// <returns>An object that is a copy of this <see cref="AssemblyName"/> object.</returns>
    public object Clone()
    {
        OriginAssemblyName CloneOrigin = (OriginAssemblyName)Origin.Clone();
        return new AssemblyName(CloneOrigin);
    }
}
