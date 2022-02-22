namespace NotNullReflection;

using OriginType = System.Type;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type : MemberInfo
{
    /// <summary>
    /// Creates a new instance of a typeof(<typeparamref name="T"/>) object.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <returns>The new instance.</returns>
    public static Type FromTypeof<T>()
    {
        return new Type(typeof(T));
    }

    /// <summary>
    /// Creates a new instance from the type of an object.
    /// </summary>
    /// <param name="obj">The object whose type is to be used to created the new instance.</param>
    /// <returns>The new instance.</returns>
    public static Type FromGetType(object obj)
    {
        return new Type(obj.GetType());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Type"/> class.
    /// </summary>
    /// <param name="origin">The origin type.</param>
    internal Type(OriginType origin)
        : base(origin)
    {
        Origin = origin;
    }

    /// <summary>
    /// Gets the origin type for which this class is a proxy.
    /// </summary>
    public new OriginType Origin { get; }
}
