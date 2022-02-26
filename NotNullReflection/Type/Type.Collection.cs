namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Converts a collection of <see cref="OriginType"/> objects to a collection of <see cref="Type"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginType"/> to convert.</param>
    /// <returns>A collection of <see cref="Type"/> objects.</returns>
    internal static IEnumerable<Type> GetList(IEnumerable<OriginType> collection) => from OriginType Item in collection
                                                                                     select CreateNew(Item);

    /// <summary>
    /// Converts a collection of <see cref="Type"/> objects to a collection of <see cref="OriginType"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="Type"/> to convert.</param>
    /// <returns>A collection of <see cref="OriginType"/> objects.</returns>
    internal static IEnumerable<OriginType> GetOriginList(IEnumerable<Type> collection) => from Type Item in collection
                                                                                           select Item.Origin;
}
