namespace NotNullReflection;

using System.Diagnostics;
using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class Type : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Type"/> class.
    /// </summary>
    /// <param name="origin">The origin type.</param>
    private Type(OriginType origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin type this class encapsulates.
    /// </summary>
    public new OriginType Origin { get; }
}
