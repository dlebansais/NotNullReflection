﻿namespace NotNullReflection;

using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Type"/> class.
    /// </summary>
    /// <param name="origin">The origin type.</param>
    public Type(OriginType origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin type for which this class is a proxy.
    /// </summary>
    public new OriginType Origin { get; }
}
