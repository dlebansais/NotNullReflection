namespace NotNullReflection;

using System.Diagnostics;
using OriginMethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// Discovers the attributes of a method and provides access to method metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public partial class MethodInfo : MethodBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin method information.</param>
    private MethodInfo(OriginMethodInfo origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin method information this class encapsulates.
    /// </summary>
    public new OriginMethodInfo Origin { get; }
}
