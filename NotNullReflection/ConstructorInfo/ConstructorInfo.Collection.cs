namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;

/// <summary>
/// Discovers the attributes of a class constructor and provides access to constructor metadata.
/// </summary>
public partial class ConstructorInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginConstructorInfo"/> objects to a collection of <see cref="ConstructorInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginConstructorInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="ConstructorInfo"/> objects.</returns>
    internal static IEnumerable<ConstructorInfo> GetList(IEnumerable<OriginConstructorInfo> collection) => from OriginConstructorInfo Item in collection
                                                                                                           select CreateNew(Item);
}
