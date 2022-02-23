namespace NotNullReflection;

using System.Diagnostics;
using NullReferenceException = System.NullReferenceException;
using OriginMethodBase = System.Reflection.MethodBase;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
public partial class MethodBase : MemberInfo
{
    /// <summary>
    /// Creates a new instance from a stack frame.
    /// </summary>
    /// <param name="frame">The stack frame.</param>
    /// <returns>The new instance.</returns>
    /// <exception cref="NullReferenceException">Method not found on frame.</exception>
    public static MethodBase FromStackFrame(StackFrame frame)
    {
        return new MethodBase(frame.GetMethod() ?? throw new NullReferenceException("Method not found on frame."));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MethodBase"/> class.
    /// </summary>
    /// <param name="origin">The origin member information.</param>
    internal MethodBase(OriginMethodBase origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin member information for which this class is a proxy.
    /// </summary>
    public new OriginMethodBase Origin { get; }
}
