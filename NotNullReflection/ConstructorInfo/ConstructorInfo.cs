namespace NotNullReflection;

using System.Diagnostics;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;

/// <summary>
/// Discovers the attributes of a class constructor and provides access to constructor metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class ConstructorInfo : MethodBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConstructorInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin constructor information.</param>
    private ConstructorInfo(OriginConstructorInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin constructor information this class encapsulates.
    /// </summary>
    public new OriginConstructorInfo Origin { get; }
}
