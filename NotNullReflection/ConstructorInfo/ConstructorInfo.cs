﻿namespace NotNullReflection;

using OriginConstructorInfo = System.Reflection.ConstructorInfo;

/// <summary>
/// Discovers the attributes of a class constructor and provides access to constructor metadata.
/// </summary>
public partial class ConstructorInfo : MethodBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin property information.</param>
    internal ConstructorInfo(OriginConstructorInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin property information for which this class is a proxy.
    /// </summary>
    public new OriginConstructorInfo Origin { get; }
}
