namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginMemberInfo = System.Reflection.MemberInfo;

/// <summary>
/// Obtains information about the attributes of a member and provides access to member metadata.
/// </summary>
public abstract partial class MemberInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginMemberInfo"/> objects to a collection of <see cref="MemberInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginMemberInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="MemberInfo"/> objects.</returns>
    internal static IEnumerable<MemberInfo> GetList(IEnumerable<OriginMemberInfo> collection) => from OriginMemberInfo Item in collection
                                                                                                 select CreateNew(Item);
}
