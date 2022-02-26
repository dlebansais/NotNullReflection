namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginMethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// Discovers the attributes of a method and provides access to method metadata.
/// </summary>
public partial class MethodInfo
{
    /// <summary>
    /// Converts a collection of <see cref="OriginMethodInfo"/> objects to a collection of <see cref="MethodInfo"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginMethodInfo"/> to convert.</param>
    /// <returns>A collection of <see cref="MethodInfo"/> objects.</returns>
    internal static IEnumerable<MethodInfo> GetList(IEnumerable<OriginMethodInfo> collection) => from OriginMethodInfo Item in collection
                                                                                                 select CreateNew(Item);
}
