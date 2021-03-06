namespace NotNullReflection;

using System.Diagnostics;
using OriginMemberInfo = System.Reflection.MemberInfo;

/// <summary>
/// Obtains information about the attributes of a member and provides access to member metadata.
/// </summary>
[DebuggerDisplay("{Origin}")]
public abstract partial class MemberInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberInfo"/> class.
    /// </summary>
    /// <param name="origin">The origin member information.</param>
    protected MemberInfo(OriginMemberInfo origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin member information this class encapsulates.
    /// </summary>
    public OriginMemberInfo Origin { get; }
}
