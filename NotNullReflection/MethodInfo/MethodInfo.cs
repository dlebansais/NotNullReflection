namespace NotNullReflection;

using OriginMethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// Discovers the attributes of a method and provides access to method metadata.
/// </summary>
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
    /// Gets the origin member information for which this class is a proxy.
    /// </summary>
    public new OriginMethodInfo Origin { get; }
}
