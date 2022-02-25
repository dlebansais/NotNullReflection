namespace NotNullReflection;

using OriginMethodBase = System.Reflection.MethodBase;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
public abstract partial class MethodBase : MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MethodBase"/> class.
    /// </summary>
    /// <param name="origin">The origin method information.</param>
    protected MethodBase(OriginMethodBase origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin method information for which this class is a proxy.
    /// </summary>
    public new OriginMethodBase Origin { get; }
}
