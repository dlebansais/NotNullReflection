namespace NotNullReflection;

using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using ArgumentException = System.ArgumentException;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using Exception = System.Exception;
using FormatException = System.FormatException;
using InvalidOperationException = System.InvalidOperationException;
using MemberAccessException = System.MemberAccessException;
using MemberTypes = System.Reflection.MemberTypes;
using MethodAccessException = System.MethodAccessException;
using NullReferenceException = System.NullReferenceException;
using OriginParameterInfo = System.Reflection.ParameterInfo;
using PropertyAttributes = System.Reflection.PropertyAttributes;
using TargetException = System.Reflection.TargetException;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TargetParameterCountException = System.Reflection.TargetParameterCountException;

/// <summary>
/// Discovers the attributes of a property and provides access to property metadata.
/// </summary>
public partial class PropertyInfo
{
    /// <summary>
    /// Gets the attributes for this property.
    /// </summary>
    /// <returns>The attributes of this property.</returns>
    public PropertyAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the property can be read.
    /// </summary>
    /// <returns>true if this property can be read; otherwise, false.</returns>
    public bool CanRead
    {
        get
        {
            return Origin.CanRead;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the property can be written to.
    /// </summary>
    /// <returns>true if this property can be written to; otherwise, false.</returns>
    public bool CanWrite
    {
        get
        {
            return Origin.CanWrite;
        }
    }

    /// <summary>
    /// Gets the get accessor for this property.
    /// </summary>
    /// <returns>The get accessor for this property.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    public MethodInfo GetMethod
    {
        get
        {
            return MethodInfo.CreateNew(Origin.GetMethod ?? throw new NullReferenceException("Method not found."));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the property is the special name.
    /// </summary>
    /// <returns>true if this property is the special name; otherwise, false.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a property.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a property.</returns>
    public override MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets the type of this property.
    /// </summary>
    /// <returns>The type of this property.</returns>
    public Type PropertyType
    {
        get
        {
            return Type.CreateNew(Origin.PropertyType);
        }
    }

    /// <summary>
    /// Gets the set accessor for this property.
    /// </summary>
    /// <returns>The set accessor for this property, or throws an exception if the property is read-only.</returns>
    /// <exception cref="NullReferenceException">Property is read-only.</exception>
    public MethodInfo SetMethod
    {
        get
        {
            return MethodInfo.CreateNew(Origin.SetMethod ?? throw new NullReferenceException("Property is read-only."));
        }
    }

    /// <summary>
    /// Returns a value that indicates whether this instance is equal to a specified object.
    /// </summary>
    /// <param name="obj">The object to compare with this instance.</param>
    /// <returns>true if <paramref name="obj"/> equals the type and value of this instance; otherwise, false.</returns>
    public override bool Equals(object obj)
    {
        return obj is PropertyInfo AsPropertyInfo && Origin.Equals(AsPropertyInfo.Origin);
    }

    /// <summary>
    /// Returns an array whose elements reflect the public get and set accessors of the property reflected by the current instance.
    /// </summary>
    /// <returns>An array of <see cref="MethodInfo"/> objects that reflect the public get and set accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.</returns>
    public MethodInfo[] GetAccessors()
    {
        return MethodInfo.GetList(Origin.GetAccessors()).ToArray();
    }

    /// <summary>
    /// Returns an array whose elements reflect the public and, if specified, non-public get and set accessors of the property reflected by the current instance.
    /// </summary>
    /// <param name="nonPublic">Indicates whether non-public methods should be returned in the returned array. true if non-public methods are to be included; otherwise, false.</param>
    /// <returns>An array whose elements reflect the get and set accessors of the property reflected by the current instance. If <paramref name="nonPublic"/> is true, this array contains public and non-public get and set accessors. If <paramref name="nonPublic"/> is false, this array contains only public get and set accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.</returns>
    public MethodInfo[] GetAccessors(bool nonPublic)
    {
        return MethodInfo.GetList(Origin.GetAccessors(nonPublic)).ToArray();
    }

    /// <summary>
    /// Returns a literal value associated with the property by a compiler.
    /// </summary>
    /// <returns>An <see cref="object"/> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, an exception is thrown.</returns>
    /// <exception cref="InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
    /// <exception cref="FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata.</exception>
    /// <exception cref="NullReferenceException">Literal value is a class type with an element value of zero.</exception>
    public object GetConstantValue()
    {
        return Origin.GetConstantValue() ?? throw new NullReferenceException("Literal value is a class type with an element value of zero.");
    }

    /// <summary>
    /// Returns the public get accessor for this property.
    /// </summary>
    /// <returns>A <see cref="MethodInfo"/> object representing the public get accessor for this property, or throws an exception if the get accessor is non-public or does not exist.</returns>
    /// <exception cref="NullReferenceException">Get accessor is non-public or does not exist.</exception>
    public MethodInfo GetGetMethod()
    {
        return MethodInfo.CreateNew(Origin.GetGetMethod() ?? throw new NullReferenceException("Get accessor is non-public or does not exist."));
    }

    /// <summary>
    /// When overridden in a derived class, returns the public or non-public get accessor for this property.
    /// </summary>
    /// <param name="nonPublic">Indicates whether a non-public get accessor should be returned. true if a non-public accessor is to be returned; otherwise, false.</param>
    /// <returns>A <see cref="MethodInfo"/> object representing the get accessor for this property, if <paramref name="nonPublic"/> is true. Throws an exception if <paramref name="nonPublic"/> is false and the get accessor is non-public, or if <paramref name="nonPublic"/> is true but no get accessors exist.</returns>
    /// <exception cref="SecurityException">The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission to reflect on this non-public method.</exception>
    /// <exception cref="NullReferenceException"><paramref name="nonPublic"/> is false and the get accessor is non-public, or <paramref name="nonPublic"/> is true but no get accessors exist.</exception>
    public MethodInfo GetGetMethod(bool nonPublic)
    {
        return MethodInfo.CreateNew(Origin.GetGetMethod(nonPublic) ?? throw new NullReferenceException($"{nameof(nonPublic)} is false and the get accessor is non-public, or {nameof(nonPublic)} is true but no get accessors exist."));
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
    /// When overridden in a derived class, returns an array of all the index parameters for the property.
    /// </summary>
    /// <returns>An array of type <see cref="OriginParameterInfo"/> containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
    public OriginParameterInfo[] GetIndexParameters()
    {
        return Origin.GetIndexParameters();
    }

    /// <summary>
    /// Returns an array of types representing the optional custom modifiers of the property.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that identify the optional custom modifiers of the current property, such as <see cref="IsConst"/> or <see cref="IsImplicitlyDereferenced"/>.</returns>
    public Type[] GetOptionalCustomModifiers()
    {
        return Type.GetList(Origin.GetOptionalCustomModifiers()).ToArray();
    }

    /// <summary>
    /// Returns a literal value associated with the property by a compiler.
    /// </summary>
    /// <returns>An <see cref="object"/> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, an exception is thrown.</returns>
    /// <exception cref="InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
    /// <exception cref="FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
    /// <exception cref="NullReferenceException">Literal value is a class type with an element value of zero.</exception>
    public object GetRawConstantValue()
    {
        return Origin.GetRawConstantValue() ?? throw new NullReferenceException("Literal value is a class type with an element value of zero.");
    }

    /// <summary>
    /// Returns an array of types representing the required custom modifiers of the property.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that identify the required custom modifiers of the current property, such as <see cref="IsConst"/> or <see cref="IsImplicitlyDereferenced"/>.</returns>
    public Type[] GetRequiredCustomModifiers()
    {
        return Type.GetList(Origin.GetRequiredCustomModifiers()).ToArray();
    }

    /// <summary>
    /// Returns the public set accessor for this property.
    /// </summary>
    /// <returns>The <see cref="MethodInfo"/> object representing the Set method for this property if the set accessor is public, or throws an exception if the set accessor is not public.</returns>
    /// <exception cref="NullReferenceException">Set accessor not public.</exception>
    public MethodInfo GetSetMethod()
    {
        return MethodInfo.CreateNew(Origin.GetSetMethod() ?? throw new NullReferenceException("Set accessor not public."));
    }

    /// <summary>
    /// When overridden in a derived class, returns the set accessor for this property.
    /// </summary>
    /// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if a non-public accessor is to be returned; otherwise, false.</param>
    /// <returns>This property's Set method, or null, as shown in the following table. Value – Condition The Set method for this property. – The set accessor is public, OR <paramref name="nonPublic"/> is true and the set accessor is non-public. Exception thrown –<paramref name="nonPublic"/> is true, but the property is read-only, OR <paramref name="nonPublic"/> is false and the set accessor is non-public, OR there is no set accessor.</returns>
    /// <exception cref="SecurityException">The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission to reflect on this non-public method.</exception>
    /// <exception cref="NullReferenceException"><paramref name="nonPublic"/> is false and the set accessor is non-public, or <paramref name="nonPublic"/> is true but no set accessors exist.</exception>
    public MethodInfo GetSetMethod(bool nonPublic)
    {
        return MethodInfo.CreateNew(Origin.GetSetMethod(nonPublic) ?? throw new NullReferenceException($"{nameof(nonPublic)} is false and the set accessor is non-public, or {nameof(nonPublic)} is true but no set accessors exist."));
    }

    /// <summary>
    /// Returns the property value of a specified object.
    /// </summary>
    /// <param name="obj">The object whose property value will be returned.</param>
    /// <returns>The property value of the specified object.</returns>
    /// <exception cref="NullReferenceException">Invalid property value.</exception>
    public object GetValue(object obj)
    {
        return Origin.GetValue(obj) ?? throw new NullReferenceException("Invalid property value.");
    }

    /// <summary>
    /// Returns the property value of a specified object with optional index values for indexed properties.
    /// </summary>
    /// <param name="obj">The object whose property value will be returned.</param>
    /// <param name="index">Optional index values for indexed properties. The indexes of indexed properties are zero-based. This value should be an empty array for non-indexed properties.</param>
    /// <returns>The property value of the specified object.</returns>
    /// <exception cref="ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed. -or- The property's get accessor is not found.</exception>
    /// <exception cref="TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is <see cref="Type.Void"/>. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes.</exception>
    /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="TargetInvocationException">An error occurred while retrieving the property value. For example, an <paramref name="index"/> value specified for an indexed property is out of range. The <see cref="Exception.InnerException"/> property indicates the reason for the error.</exception>
    /// <exception cref="NullReferenceException">Invalid property value.</exception>
    public object GetValue(object obj, object[] index)
    {
        return Origin.GetValue(obj != Type.Void ? obj : null, index.Length > 0 ? index : null) ?? throw new NullReferenceException("Invalid property value.");
    }

    /// <summary>
    /// When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.
    /// </summary>
    /// <param name="obj">The object whose property value will be returned.</param>
    /// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, and SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag.</param>
    /// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="MemberInfo"/> objects through reflection.</param>
    /// <param name="index">Optional index values for indexed properties. This value should be an empty array for non-indexed properties.</param>
    /// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="CultureInfo.Parent"/> property will be called successively in search of a match. Use <see cref="CultureInfo.CurrentUICulture"/> if the default is desired.</param>
    /// <returns>The property value of the specified object.</returns>
    /// <exception cref="ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed. -or- The property's get accessor is not found.</exception>
    /// <exception cref="TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj"/> is <see cref="Type.Void"/>.</exception>
    /// <exception cref="TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes.</exception>
    /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
    /// <exception cref="TargetInvocationException">An error occurred while retrieving the property value. For example, an <paramref name="index"/> value specified for an indexed property is out of range. The <see cref="Exception.InnerException"/> property indicates the reason for the error.</exception>
    /// <exception cref="NullReferenceException">Invalid property value.</exception>
    public object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
        return Origin.GetValue(obj != Type.Void ? obj : null, invokeAttr, binder != Assembly.DefaultBinder ? binder : null, index.Length > 0 ? index : null, culture) ?? throw new NullReferenceException("Invalid property value.");
    }

    /// <summary>
    /// Indicates whether two <see cref="PropertyInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(PropertyInfo left, PropertyInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="PropertyInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(PropertyInfo left, PropertyInfo right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Sets the property value of a specified object.
    /// </summary>
    /// <param name="obj">The object whose property value will be set.</param>
    /// <param name="value">The new property value.</param>
    /// <exception cref="ArgumentException">The property's set accessor is not found. -or- <paramref name="value"/> cannot be converted to the type of <see cref="PropertyType"/>.</exception>
    /// <exception cref="TargetException">The type of <paramref name="obj"/> does not match the target type, or a property is an instance property but obj is <see cref="Type.Void"/>. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="TargetInvocationException">An error occurred while setting the property value. The <see cref="Exception.InnerException"/> property indicates the reason for the error.</exception>
    public void SetValue(object obj, object value)
    {
        Origin.SetValue(obj, value);
    }

    /// <summary>
    /// Sets the property value of a specified object with optional index values for index properties.
    /// </summary>
    /// <param name="obj">The object whose property value will be set.</param>
    /// <param name="value">The new property value.</param>
    /// <param name="index">Optional index values for indexed properties. This value should be an empty array for non-indexed properties.</param>
    /// <exception cref="ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed. -or- The property's set accessor is not found. -or- <paramref name="value"/> cannot be converted to the type of <see cref="PropertyType"/>.</exception>
    /// <exception cref="TargetException">The type of <paramref name="obj"/> does not match the target type, or a property is an instance property but obj is <see cref="Type.Void"/>. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes.</exception>
    /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="Exception.InnerException"/> property indicates the reason for the error.</exception>
    public virtual void SetValue(object obj, object value, object[] index)
    {
        Origin.SetValue(obj, value, index.Length > 0 ? index : null);
    }

    /// <summary>
    /// When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.
    /// </summary>
    /// <param name="obj">The object whose property value will be set.</param>
    /// <param name="value">The new property value.</param>
    /// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, and SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag.</param>
    /// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="MemberInfo"/> objects through reflection.</param>
    /// <param name="index">Optional index values for indexed properties. This value should be an empty array for non-indexed properties.</param>
    /// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="CultureInfo.Parent"/> property will be called successively in search of a match. Use <see cref="CultureInfo.CurrentUICulture"/> if the default is desired.</param>
    /// <exception cref="ArgumentException">The <paramref name="index"/> array does not contain the type of arguments needed. -or- The property's set accessor is not found. -or- <paramref name="value"/> cannot be converted to the type of <see cref="PropertyType"/>.</exception>
    /// <exception cref="TargetException">The object does not match the target type, or a property is an instance property but obj is <see cref="Type.Void"/>.</exception>
    /// <exception cref="TargetParameterCountException">The number of parameters in <paramref name="index"/> does not match the number of parameters the indexed property takes.</exception>
    /// <exception cref="MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
    /// <exception cref="TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="Exception.InnerException"/> property indicates the reason for the error.</exception>
    public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
    {
        Origin.SetValue(obj, value, invokeAttr, binder != Assembly.DefaultBinder ? binder : null, index.Length > 0 ? index : null, culture);
    }
}
