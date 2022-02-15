namespace NotNullReflection;

using System.Collections.Generic;
using System.Linq;
using OriginAssemblyName = System.Reflection.AssemblyName;

/// <summary>
/// Describes an assembly's unique identity in full.
/// </summary>
public partial class AssemblyName
{
    /// <summary>
    /// Converts a collection of <see cref="OriginAssemblyName"/> objects to a collection of <see cref="AssemblyName"/> objects.
    /// </summary>
    /// <param name="collection">The collection of <see cref="OriginAssemblyName"/> to convert.</param>
    /// <returns>A collection of <see cref="AssemblyName"/> objects.</returns>
    public static IEnumerable<AssemblyName> GetList(IEnumerable<OriginAssemblyName> collection) => from OriginAssemblyName Item in collection
                                                                                                   select new AssemblyName(Item);
}
