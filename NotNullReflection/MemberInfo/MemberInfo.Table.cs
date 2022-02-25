namespace NotNullReflection;

using System.Collections.Generic;
using NotSupportedException = System.NotSupportedException;
using OriginMemberInfo = System.Reflection.MemberInfo;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginMethodInfo = System.Reflection.MethodInfo;
using OriginPropertyInfo = System.Reflection.PropertyInfo;
using OriginType = System.Type;

/// <summary>
/// Obtains information about the attributes of a member and provides access to member metadata.
/// </summary>
public abstract partial class MemberInfo
{
    /// <summary>
    /// Creates a new instance of <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="origin">The origin member information.</param>
    /// <returns>The new instance.</returns>
    /// <exception cref="NotSupportedException">Conversion not supported.</exception>
    public static MemberInfo CreateNew(OriginMemberInfo origin)
    {
        if (MemberInfoCache.ContainsKey(origin))
            return MemberInfoCache[origin];
        else
        {
            MemberInfo NewInstance;

            switch (origin)
            {
                case OriginConstructorInfo AsConstructorInfo:
                    NewInstance = ConstructorInfo.CreateNew(AsConstructorInfo);
                    break;
                case OriginMethodInfo AsMethodInfo:
                    NewInstance = MethodInfo.CreateNew(AsMethodInfo);
                    break;
                case OriginPropertyInfo AsPropertyInfo:
                    NewInstance = PropertyInfo.CreateNew(AsPropertyInfo);
                    break;
                case OriginType AsType:
                    NewInstance = Type.CreateNew(AsType);
                    break;
                default:
                    throw new NotSupportedException("Conversion not supported.");
            }

            MemberInfoCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginMemberInfo, MemberInfo> MemberInfoCache = new Dictionary<OriginMemberInfo, MemberInfo>();
}
