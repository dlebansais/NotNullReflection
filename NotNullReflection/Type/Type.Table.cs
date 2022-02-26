namespace NotNullReflection;

using System.Collections.Generic;
using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Creates a new instance of <see cref="Type"/>.
    /// </summary>
    /// <param name="origin">The origin type.</param>
    /// <returns>The new instance.</returns>
    internal static Type CreateNew(OriginType origin)
    {
        if (TypeCache.ContainsKey(origin))
            return TypeCache[origin];
        else
        {
            Type NewInstance = new Type(origin);
            TypeCache.Add(origin, NewInstance);
            return NewInstance;
        }
    }

    private static Dictionary<OriginType, Type> TypeCache = new Dictionary<OriginType, Type>();
}
