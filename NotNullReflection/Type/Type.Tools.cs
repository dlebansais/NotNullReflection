namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using AmbiguousMatchException = System.Reflection.AmbiguousMatchException;
using OriginPropertyInfo = System.Reflection.PropertyInfo;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Creates a new instance of a typeof(<typeparamref name="T"/>) object.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>The new instance.</returns>
    public static Type FromTypeof<T>()
    {
        return CreateNew(typeof(T));
    }

    /// <summary>
    /// Creates a new instance from the type of an object.
    /// </summary>
    /// <param name="obj">The object whose type is to be used to created the new instance.</param>
    /// <returns>The new instance.</returns>
    public static Type FromGetType(object obj)
    {
        return CreateNew(obj.GetType());
    }

    /// <summary>
    /// Gets a value indicating whether the current type is the typeof(<typeparamref name="T"/>) object.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>true if the current type is the typeof(<typeparamref name="T"/>) object.</returns>
    public bool IsTypeof<T>()
    {
        return Origin == typeof(T);
    }

    /// <summary>
    /// Gets a value indicating whether the current type is the typeof(<see cref="void"/>) object.
    /// </summary>
    /// <returns>true if the current type is the typeof(<see cref="void"/>) object.</returns>
    public bool IsTypeofVoid()
    {
        return Origin == typeof(void);
    }

    /// <summary>
    /// Determines whether the current type has a public property with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to look for.</param>
    /// <param name="property">The property upon return.</param>
    /// <returns>true if found; otherwise, false.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public bool IsProperty(string name, out PropertyInfo property)
    {
        if (Origin.GetProperty(name) is OriginPropertyInfo OriginProperty)
        {
            property = PropertyInfo.CreateNew(OriginProperty);
            return true;
        }
        else
        {
            property = null!;
            return false;
        }
    }
}
