namespace NotNullReflection;

using System.Diagnostics;
using NullReferenceException = System.NullReferenceException;
using NotSupportedException = System.NotSupportedException;
using OriginMethodBase = System.Reflection.MethodBase;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginMethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// Provides information about methods and constructors.
/// </summary>
public abstract partial class MethodBase
{
    /// <summary>
    /// Creates a new instance from a stack frame.
    /// </summary>
    /// <param name="frame">The stack frame.</param>
    /// <returns>The new instance.</returns>
    /// <exception cref="NullReferenceException">Method not found on frame.</exception>
    public static MethodBase FromStackFrame(StackFrame frame)
    {
        return CreateNew(frame.GetMethod() ?? throw new NullReferenceException("Method not found on frame."));
    }

    /// <summary>
    /// Creates a new instance of <see cref="MethodBase"/>.
    /// </summary>
    /// <param name="origin">The origin method information.</param>
    /// <returns>The new instance.</returns>
    /// <exception cref="NotSupportedException">Conversion not supported.</exception>
    public static MethodBase CreateNew(OriginMethodBase origin)
    {
        switch (origin)
        {
            case OriginConstructorInfo AsConstructorInfo:
                return ConstructorInfo.CreateNew(AsConstructorInfo);
            case OriginMethodInfo AsMethodInfo:
                return MethodInfo.CreateNew(AsMethodInfo);
            default:
                throw new NotSupportedException("Conversion not supported.");
        }
    }
}
