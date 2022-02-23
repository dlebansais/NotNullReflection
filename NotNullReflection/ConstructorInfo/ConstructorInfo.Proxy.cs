namespace NotNullReflection;

using System.Globalization;
using System.Security;
using TypedReference = System.TypedReference;
using RuntimeArgumentHandle = System.RuntimeArgumentHandle;
using NotSupportedException = System.NotSupportedException;
using ArgumentException = System.ArgumentException;
using MethodAccessException = System.MethodAccessException;
using MemberAccessException = System.MemberAccessException;
using MemberTypes = System.Reflection.MemberTypes;
using BindingFlags = System.Reflection.BindingFlags;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginBinder = System.Reflection.Binder;
using TargetParameterCountException = System.Reflection.TargetParameterCountException;
using TargetInvocationException = System.Reflection.TargetInvocationException;

/// <summary>
/// Discovers the attributes of a class constructor and provides access to constructor metadata.
/// </summary>
public partial class ConstructorInfo
{
    /// <summary>
    /// Represents the name of the class constructor method as it is stored in metadata. This name is always ".ctor". This field is read-only.
    /// </summary>
    public static readonly string ConstructorName = OriginConstructorInfo.ConstructorName;

    /// <summary>
    /// Represents the name of the type constructor method as it is stored in metadata. This name is always ".cctor". This property is read-only.
    /// </summary>
    public static readonly string TypeConstructorName = OriginConstructorInfo.TypeConstructorName;

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a constructor.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a constructor.</returns>
    public override MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>true if obj equals the type and value of this instance; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is ConstructorInfo AsConstructorInfo && Origin.Equals(AsConstructorInfo.Origin);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode()
    {
        return Origin.GetHashCode();
    }

    /// <summary>
    /// Invokes the constructor reflected by the instance that has the specified parameters, providing default values for the parameters not commonly used.
    /// </summary>
    /// <param name="parameters">An array of values that matches the number, order and type (under the constraints of the default binder) of the parameters for this constructor. If this constructor takes no parameters, then use an array with zero elements, as in <see cref="object"/>[] parameters = new <see cref="object"/>[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
    /// <returns>An instance of the class associated with the constructor.</returns>
    /// <exception cref="MemberAccessException">The class is abstract. -or- The constructor is a class initializer.</exception>
    /// <exception cref="MethodAccessException">The constructor is private or protected, and the caller lacks System.Security.Permissions.ReflectionPermissionFlag.MemberAccess. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="ArgumentException">The <paramref name="parameters"/> array does not contain values that match the types accepted by this constructor.</exception>
    /// <exception cref="TargetInvocationException">The invoked constructor throws an exception.</exception>
    /// <exception cref="TargetParameterCountException">An incorrect number of parameters was passed.</exception>
    /// <exception cref="NotSupportedException">Creation of <see cref="TypedReference"/>, System.ArgIterator, and <see cref="RuntimeArgumentHandle"/> types is not supported.</exception>
    /// <exception cref="SecurityException">The caller does not have the necessary code access permission.</exception>
    public object Invoke(object[] parameters)
    {
        return Origin.Invoke(parameters);
    }

    /// <summary>
    /// When implemented in a derived class, invokes the constructor reflected by this ConstructorInfo with the specified arguments, under the constraints of the specified Binder.
    /// </summary>
    /// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding.</param>
    /// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If binder is null, then Binder.DefaultBinding is used.</param>
    /// <param name="parameters">An array of values that matches the number, order and type (under the constraints of the default binder) of the parameters for this constructor. If this constructor takes no parameters, then use an array with zero elements, as in <see cref="object"/>[] parameters = new <see cref="object"/>[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> used to govern the coercion of types. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired.</param>
    /// <returns>An instance of the class associated with the constructor.</returns>
    /// <exception cref="ArgumentException">The <paramref name="parameters"/> array does not contain values that match the types accepted by this constructor, under the constraints of the binder.</exception>
    /// <exception cref="TargetInvocationException">The invoked constructor throws an exception.</exception>
    /// <exception cref="TargetParameterCountException">An incorrect number of parameters was passed.</exception>
    /// <exception cref="NotSupportedException">Creation of <see cref="TypedReference"/>, System.ArgIterator, and <see cref="RuntimeArgumentHandle"/> types is not supported.</exception>
    /// <exception cref="SecurityException">The caller does not have the necessary code access permission.</exception>
    /// <exception cref="MemberAccessException">The class is abstract. -or- The constructor is a class initializer.</exception>
    /// <exception cref="MethodAccessException">The constructor is private or protected, and the caller lacks System.Security.Permissions.ReflectionPermissionFlag.MemberAccess.</exception>
    public object Invoke(BindingFlags invokeAttr, OriginBinder binder, object[] parameters, CultureInfo culture)
    {
        return Origin.Invoke(invokeAttr, binder != Assembly.DefaultBinder ? binder : null, parameters, culture);
    }

    /// <summary>
    /// Indicates whether two <see cref="ConstructorInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="ConstructorInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
    {
        return left.Origin != right.Origin;
    }
}
