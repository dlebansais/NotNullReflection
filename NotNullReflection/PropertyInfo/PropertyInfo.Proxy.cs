namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MemberTypes = System.Reflection.MemberTypes;
using NotSupportedException = System.NotSupportedException;
using NotImplementedException = System.NotImplementedException;
using InvalidOperationException = System.InvalidOperationException;
using ArgumentException = System.ArgumentException;
using OriginParameterInfo = System.Reflection.ParameterInfo;
using ICustomAttributeProvider = System.Reflection.ICustomAttributeProvider;
using Delegate = System.Delegate;

/// <summary>
/// Discovers the attributes of a property and provides access to property metadata.
/// </summary>
public partial class PropertyInfo
{
    //
    // Summary:
    //     Gets the attributes for this property.
    //
    // Returns:
    //     The attributes of this property.
    public abstract PropertyAttributes Attributes
    {
        get;
    }

    //
    // Summary:
    //     Gets a value indicating whether the property can be read.
    //
    // Returns:
    //     true if this property can be read; otherwise, false.
    public abstract bool CanRead
    {
        get;
    }

    //
    // Summary:
    //     Gets a value indicating whether the property can be written to.
    //
    // Returns:
    //     true if this property can be written to; otherwise, false.
    public abstract bool CanWrite
    {
        get;
    }

    //
    // Summary:
    //     Gets the get accessor for this property.
    //
    // Returns:
    //     The get accessor for this property.
    public virtual MethodInfo? GetMethod
    {
        get
        {
            throw null;
        }
    }

    //
    // Summary:
    //     Gets a value indicating whether the property is the special name.
    //
    // Returns:
    //     true if this property is the special name; otherwise, false.
    public bool IsSpecialName
    {
        get
        {
            throw null;
        }
    }

    //
    // Summary:
    //     Gets a System.Reflection.MemberTypes value indicating that this member is a property.
    //
    // Returns:
    //     A System.Reflection.MemberTypes value indicating that this member is a property.
    public override MemberTypes MemberType
    {
        get
        {
            throw null;
        }
    }

    //
    // Summary:
    //     Gets the type of this property.
    //
    // Returns:
    //     The type of this property.
    public abstract Type PropertyType
    {
        [NullableContext(1)]
        get;
    }

    //
    // Summary:
    //     Gets the set accessor for this property.
    //
    // Returns:
    //     The set accessor for this property, or null if the property is read-only.
    public virtual MethodInfo? SetMethod
    {
        get
        {
            throw null;
        }
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Reflection.PropertyInfo class.
    protected PropertyInfo()
    {
    }

    //
    // Summary:
    //     Returns a value that indicates whether this instance is equal to a specified
    //     object.
    //
    // Parameters:
    //   obj:
    //     An object to compare with this instance, or null.
    //
    // Returns:
    //     true if obj equals the type and value of this instance; otherwise, false.
    public override bool Equals(object? obj)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns an array whose elements reflect the public get and set accessors of the
    //     property reflected by the current instance.
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects that reflect the public get
    //     and set accessors of the property reflected by the current instance, if found;
    //     otherwise, this method returns an array with zero (0) elements.
    public MethodInfo[] GetAccessors()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns an array whose elements reflect the public and, if specified, non-public
    //     get and set accessors of the property reflected by the current instance.
    //
    // Parameters:
    //   nonPublic:
    //     Indicates whether non-public methods should be returned in the returned array.
    //     true if non-public methods are to be included; otherwise, false.
    //
    // Returns:
    //     An array whose elements reflect the get and set accessors of the property reflected
    //     by the current instance. If nonPublic is true, this array contains public and
    //     non-public get and set accessors. If nonPublic is false, this array contains
    //     only public get and set accessors. If no accessors with the specified visibility
    //     are found, this method returns an array with zero (0) elements.
    public abstract MethodInfo[] GetAccessors(bool nonPublic);

    //
    // Summary:
    //     Returns a literal value associated with the property by a compiler.
    //
    // Returns:
    //     An System.Object that contains the literal value associated with the property.
    //     If the literal value is a class type with an element value of zero, the return
    //     value is null.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     The Constant table in unmanaged metadata does not contain a constant value for
    //     the current property.
    //
    //   T:System.FormatException:
    //     The type of the value is not one of the types permitted by the Common Language
    //     Specification (CLS). See the ECMA Partition II specification, Metadata.
    public virtual object? GetConstantValue()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns the public get accessor for this property.
    //
    // Returns:
    //     A MethodInfo object representing the public get accessor for this property, or
    //     null if the get accessor is non-public or does not exist.
    public MethodInfo? GetGetMethod()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, returns the public or non-public get accessor
    //     for this property.
    //
    // Parameters:
    //   nonPublic:
    //     Indicates whether a non-public get accessor should be returned. true if a non-public
    //     accessor is to be returned; otherwise, false.
    //
    // Returns:
    //     A MethodInfo object representing the get accessor for this property, if nonPublic
    //     is true. Returns null if nonPublic is false and the get accessor is non-public,
    //     or if nonPublic is true but no get accessors exist.
    //
    // Exceptions:
    //   T:System.Security.SecurityException:
    //     The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission
    //     to reflect on this non-public method.
    public abstract MethodInfo? GetGetMethod(bool nonPublic);

    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
    public override int GetHashCode()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, returns an array of all the index parameters
    //     for the property.
    //
    // Returns:
    //     An array of type ParameterInfo containing the parameters for the indexes. If
    //     the property is not indexed, the array has 0 (zero) elements.
    public abstract ParameterInfo[] GetIndexParameters();

    //
    // Summary:
    //     Returns an array of types representing the optional custom modifiers of the property.
    //
    // Returns:
    //     An array of System.Type objects that identify the optional custom modifiers of
    //     the current property, such as System.Runtime.CompilerServices.IsConst or System.Runtime.CompilerServices.IsImplicitlyDereferenced.
    public virtual Type[] GetOptionalCustomModifiers()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a literal value associated with the property by a compiler.
    //
    // Returns:
    //     An System.Object that contains the literal value associated with the property.
    //     If the literal value is a class type with an element value of zero, the return
    //     value is null.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     The Constant table in unmanaged metadata does not contain a constant value for
    //     the current property.
    //
    //   T:System.FormatException:
    //     The type of the value is not one of the types permitted by the Common Language
    //     Specification (CLS). See the ECMA Partition II specification, Metadata Logical
    //     Format: Other Structures, Element Types used in Signatures.
    public virtual object? GetRawConstantValue()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns an array of types representing the required custom modifiers of the property.
    //
    // Returns:
    //     An array of System.Type objects that identify the required custom modifiers of
    //     the current property, such as System.Runtime.CompilerServices.IsConst or System.Runtime.CompilerServices.IsImplicitlyDereferenced.
    public virtual Type[] GetRequiredCustomModifiers()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns the public set accessor for this property.
    //
    // Returns:
    //     The MethodInfo object representing the Set method for this property if the set
    //     accessor is public, or null if the set accessor is not public.
    public MethodInfo? GetSetMethod()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, returns the set accessor for this property.
    //
    // Parameters:
    //   nonPublic:
    //     Indicates whether the accessor should be returned if it is non-public. true if
    //     a non-public accessor is to be returned; otherwise, false.
    //
    // Returns:
    //     This property's Set method, or null, as shown in the following table.
    //     Value – Condition
    //     The Set method for this property. – The set accessor is public, OR nonPublic
    //     is true and the set accessor is non-public.
    //     null –nonPublic is true, but the property is read-only, OR nonPublic is false
    //     and the set accessor is non-public, OR there is no set accessor.
    //
    // Exceptions:
    //   T:System.Security.SecurityException:
    //     The requested method is non-public and the caller does not have System.Security.Permissions.ReflectionPermission
    //     to reflect on this non-public method.
    public abstract MethodInfo? GetSetMethod(bool nonPublic);

    //
    // Summary:
    //     Returns the property value of a specified object.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be returned.
    //
    // Returns:
    //     The property value of the specified object.
    public object? GetValue(object? obj)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns the property value of a specified object with optional index values for
    //     indexed properties.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be returned.
    //
    //   index:
    //     Optional index values for indexed properties. The indexes of indexed properties
    //     are zero-based. This value should be null for non-indexed properties.
    //
    // Returns:
    //     The property value of the specified object.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The index array does not contain the type of arguments needed. -or- The property's
    //     get accessor is not found.
    //
    //   T:System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null. Note: In .NET for Windows Store apps or the Portable Class Library,
    //     catch System.Exception instead.
    //
    //   T:System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters the
    //     indexed property takes.
    //
    //   T:System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside a
    //     class. Note: In .NET for Windows Store apps or the Portable Class Library, catch
    //     the base class exception, System.MemberAccessException, instead.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     An error occurred while retrieving the property value. For example, an index
    //     value specified for an indexed property is out of range. The System.Exception.InnerException
    //     property indicates the reason for the error.
    public virtual object? GetValue(object? obj, object?[]? index)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, returns the property value of a specified
    //     object that has the specified binding, index, and culture-specific information.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be returned.
    //
    //   invokeAttr:
    //     A bitwise combination of the following enumeration members that specify the invocation
    //     attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty,
    //     and SetProperty. You must specify a suitable invocation attribute. For example,
    //     to invoke a static member, set the Static flag.
    //
    //   binder:
    //     An object that enables the binding, coercion of argument types, invocation of
    //     members, and retrieval of System.Reflection.MemberInfo objects through reflection.
    //     If binder is null, the default binder is used.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for non-indexed
    //     properties.
    //
    //   culture:
    //     The culture for which the resource is to be localized. If the resource is not
    //     localized for this culture, the System.Globalization.CultureInfo.Parent property
    //     will be called successively in search of a match. If this value is null, the
    //     culture-specific information is obtained from the System.Globalization.CultureInfo.CurrentUICulture
    //     property.
    //
    // Returns:
    //     The property value of the specified object.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The index array does not contain the type of arguments needed. -or- The property's
    //     get accessor is not found.
    //
    //   T:System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   T:System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters the
    //     indexed property takes.
    //
    //   T:System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside a
    //     class.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     An error occurred while retrieving the property value. For example, an index
    //     value specified for an indexed property is out of range. The System.Exception.InnerException
    //     property indicates the reason for the error.
    public abstract object? GetValue(object? obj, BindingFlags invokeAttr, Binder? binder, object?[]? index, CultureInfo? culture);

    //
    // Summary:
    //     Indicates whether two System.Reflection.PropertyInfo objects are equal.
    //
    // Parameters:
    //   left:
    //     The first object to compare.
    //
    //   right:
    //     The second object to compare.
    //
    // Returns:
    //     true if left is equal to right; otherwise, false.
    public static bool operator ==(PropertyInfo? left, PropertyInfo? right)
    {
        throw null;
    }

    //
    // Summary:
    //     Indicates whether two System.Reflection.PropertyInfo objects are not equal.
    //
    // Parameters:
    //   left:
    //     The first object to compare.
    //
    //   right:
    //     The second object to compare.
    //
    // Returns:
    //     true if left is not equal to right; otherwise, false.
    public static bool operator !=(PropertyInfo? left, PropertyInfo? right)
    {
        throw null;
    }

    //
    // Summary:
    //     Sets the property value of a specified object.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be set.
    //
    //   value:
    //     The new property value.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The property's set accessor is not found. -or- value cannot be converted to the
    //     type of System.Reflection.PropertyInfo.PropertyType.
    //
    //   T:System.Reflection.TargetException:
    //     The type of obj does not match the target type, or a property is an instance
    //     property but obj is null. Note: In .NET for Windows Store apps or the Portable
    //     Class Library, catch System.Exception instead.
    //
    //   T:System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside a
    //     class. Note: In .NET for Windows Store apps or the Portable Class Library, catch
    //     the base class exception, System.MemberAccessException, instead.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     An error occurred while setting the property value. The System.Exception.InnerException
    //     property indicates the reason for the error.
    public void SetValue(object? obj, object? value)
    {
    }

    //
    // Summary:
    //     Sets the property value of a specified object with optional index values for
    //     index properties.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be set.
    //
    //   value:
    //     The new property value.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for non-indexed
    //     properties.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The index array does not contain the type of arguments needed. -or- The property's
    //     set accessor is not found. -or- value cannot be converted to the type of System.Reflection.PropertyInfo.PropertyType.
    //
    //   T:System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null. Note: In .NET for Windows Store apps or the Portable Class Library,
    //     catch System.Exception instead.
    //
    //   T:System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters the
    //     indexed property takes.
    //
    //   T:System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside a
    //     class. Note: In .NET for Windows Store apps or the Portable Class Library, catch
    //     the base class exception, System.MemberAccessException, instead.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     An error occurred while setting the property value. For example, an index value
    //     specified for an indexed property is out of range. The System.Exception.InnerException
    //     property indicates the reason for the error.
    public virtual void SetValue(object? obj, object? value, object?[]? index)
    {
    }

    //
    // Summary:
    //     When overridden in a derived class, sets the property value for a specified object
    //     that has the specified binding, index, and culture-specific information.
    //
    // Parameters:
    //   obj:
    //     The object whose property value will be set.
    //
    //   value:
    //     The new property value.
    //
    //   invokeAttr:
    //     A bitwise combination of the following enumeration members that specify the invocation
    //     attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty,
    //     or SetProperty. You must specify a suitable invocation attribute. For example,
    //     to invoke a static member, set the Static flag.
    //
    //   binder:
    //     An object that enables the binding, coercion of argument types, invocation of
    //     members, and retrieval of System.Reflection.MemberInfo objects through reflection.
    //     If binder is null, the default binder is used.
    //
    //   index:
    //     Optional index values for indexed properties. This value should be null for non-indexed
    //     properties.
    //
    //   culture:
    //     The culture for which the resource is to be localized. If the resource is not
    //     localized for this culture, the System.Globalization.CultureInfo.Parent property
    //     will be called successively in search of a match. If this value is null, the
    //     culture-specific information is obtained from the System.Globalization.CultureInfo.CurrentUICulture
    //     property.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The index array does not contain the type of arguments needed. -or- The property's
    //     set accessor is not found. -or- value cannot be converted to the type of System.Reflection.PropertyInfo.PropertyType.
    //
    //   T:System.Reflection.TargetException:
    //     The object does not match the target type, or a property is an instance property
    //     but obj is null.
    //
    //   T:System.Reflection.TargetParameterCountException:
    //     The number of parameters in index does not match the number of parameters the
    //     indexed property takes.
    //
    //   T:System.MethodAccessException:
    //     There was an illegal attempt to access a private or protected method inside a
    //     class.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     An error occurred while setting the property value. For example, an index value
    //     specified for an indexed property is out of range. The System.Exception.InnerException
    //     property indicates the reason for the error.
    public abstract void SetValue(object? obj, object? value, BindingFlags invokeAttr, Binder? binder, object?[]? index, CultureInfo? culture);
}
