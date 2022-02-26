namespace NotNullReflection;

using System.Diagnostics;
using OriginMethodBase = System.Reflection.MethodBase;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
[DebuggerDisplay("{Origin}")]
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
    /// Gets the origin method information this class encapsulates.
    /// </summary>
    public new OriginMethodBase Origin { get; }
}
