namespace NotNullReflection;

using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using ArgumentException = System.ArgumentException;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using Exception = System.Exception;
using FieldAccessException = System.FieldAccessException;
using FieldAttributes = System.Reflection.FieldAttributes;
using FormatException = System.FormatException;
using InvalidOperationException = System.InvalidOperationException;
using MemberAccessException = System.MemberAccessException;
using MemberTypes = System.Reflection.MemberTypes;
using NotSupportedException = System.NotSupportedException;
using NullReferenceException = System.NullReferenceException;
using OriginFieldInfo = System.Reflection.FieldInfo;
using RuntimeFieldHandle = System.RuntimeFieldHandle;
using RuntimeTypeHandle = System.RuntimeTypeHandle;
using TargetException = System.Reflection.TargetException;
using TypedReference = System.TypedReference;

/// <summary>
/// Discovers the attributes of a field and provides access to field metadata.
/// </summary>
public partial class FieldInfo
{
    /// <summary>
    /// Gets the attributes associated with this field.
    /// </summary>
    /// <returns>The FieldAttributes for this field.</returns>
    public FieldAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets a <see cref="RuntimeFieldHandle"/>, which is a handle to the internal metadata representation of a field.
    /// </summary>
    /// <returns>A handle to the internal metadata representation of a field.</returns>
    public RuntimeFieldHandle FieldHandle
    {
        get
        {
            return Origin.FieldHandle;
        }
    }

    /// <summary>
    /// Gets the type of this field object.
    /// </summary>
    /// <returns>The type of this field object.</returns>
    public Type FieldType
    {
        get
        {
            return Type.CreateNew(Origin.FieldType);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the potential visibility of this field is described by <see cref="FieldAttributes.Assembly"/>; that is, the field is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.
    /// </summary>
    /// <returns><see langword="true"/> if the visibility of this field is exactly described by <see cref="FieldAttributes.Assembly"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsAssembly
    {
        get
        {
            return Origin.IsAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the visibility of this field is described by <see cref="FieldAttributes.Family"/>; that is, the field is visible only within its class and derived classes.
    /// </summary>
    /// <returns><see langword="true"/> if access to this field is exactly described by <see cref="FieldAttributes.Family"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsFamily
    {
        get
        {
            return Origin.IsFamily;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the visibility of this field is described by <see cref="FieldAttributes.FamANDAssem"/>; that is, the field can be accessed from derived classes, but only if they are in the same assembly.
    /// </summary>
    /// <returns><see langword="true"/> if access to this field is exactly described by <see cref="FieldAttributes.FamANDAssem"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsFamilyAndAssembly
    {
        get
        {
            return Origin.IsFamilyAndAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the potential visibility of this field is described by <see cref="FieldAttributes.FamORAssem"/>; that is, the field can be accessed by derived classes wherever they are, and by classes in the same assembly.
    /// </summary>
    /// <returns><see langword="true"/> if access to this field is exactly described by <see cref="FieldAttributes.FamORAssem"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsFamilyOrAssembly
    {
        get
        {
            return Origin.IsFamilyOrAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the field can only be set in the body of the constructor.
    /// </summary>
    /// <returns><see langword="true"/> if the field has the <see cref="FieldAttributes.InitOnly"/> attribute set; otherwise, <see langword="false"/>.</returns>
    public bool IsInitOnly
    {
        get
        {
            return Origin.IsInitOnly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the value is written at compile time and cannot be changed.
    /// </summary>
    /// <returns><see langword="true"/> if the field has the <see cref="FieldAttributes.Literal"/> attribute set; otherwise, <see langword="false"/>.</returns>
    public bool IsLiteral
    {
        get
        {
            return Origin.IsLiteral;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this field has the <see cref="FieldAttributes.NotSerialized"/> attribute.
    /// </summary>
    /// <returns><see langword="true"/> if the field has the <see cref="FieldAttributes.NotSerialized"/> attribute set; otherwise, <see langword="false"/>.</returns>
    public bool IsNotSerialized
    {
        get
        {
            return Origin.IsNotSerialized;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the corresponding <see cref="FieldAttributes.PinvokeImpl"/> attribute is set in <see cref="FieldAttributes"/>.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="FieldAttributes.PinvokeImpl"/> attribute is set in <see cref="FieldAttributes"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsPinvokeImpl
    {
        get
        {
            return Origin.IsPinvokeImpl;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the field is private.
    /// </summary>
    /// <returns><see langword="true"/> if the field is private; otherwise, <see langword="false"/>.</returns>
    public bool IsPrivate
    {
        get
        {
            return Origin.IsPrivate;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the field is public.
    /// </summary>
    /// <returns><see langword="true"/> if this field is public; otherwise, <see langword="false"/>.</returns>
    public bool IsPublic
    {
        get
        {
            return Origin.IsPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current field is security-critical or security-safe-critical at the current trust level.
    /// </summary>
    /// <returns><see langword="true"/> if the current field is security-critical or security-safe-critical at the current trust level; <see langword="false"/> if it is transparent.</returns>
    public bool IsSecurityCritical
    {
        get
        {
            return Origin.IsSecurityCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current field is security-safe-critical at the current trust level.
    /// </summary>
    /// <returns><see langword="true"/> if the current field is security-safe-critical at the current trust level; <see langword="false"/> if it is security-critical or transparent.</returns>
    public bool IsSecuritySafeCritical
    {
        get
        {
            return Origin.IsSecuritySafeCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current field is transparent at the current trust level.
    /// </summary>
    /// <returns><see langword="true"/> if the field is security-transparent at the current trust level; otherwise, <see langword="false"/>.</returns>
    public bool IsSecurityTransparent
    {
        get
        {
            return Origin.IsSecurityTransparent;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the corresponding <see cref="FieldAttributes.SpecialName"/> attribute is set in the <see cref="FieldAttributes"/> enumerator.
    /// </summary>
    /// <returns><see langword="true"/> if the <see cref="FieldAttributes.SpecialName"/> attribute is set in <see cref="FieldAttributes"/>; otherwise, <see langword="false"/>.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the field is static.
    /// </summary>
    /// <returns><see langword="true"/> if this field is static; otherwise, <see langword="false"/>.</returns>
    public bool IsStatic
    {
        get
        {
            return Origin.IsStatic;
        }
    }

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a field.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a field.</returns>
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
    /// <returns><see langword="true"/> if <paramref name="obj"/> equals the type and value of this instance; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object obj)
    {
        return obj is FieldInfo AsFieldInfo && Origin.Equals(AsFieldInfo.Origin);
    }

    /// <summary>
    /// Gets a <see cref="FieldInfo"/> for the field represented by the specified handle.
    /// </summary>
    /// <param name="handle">A <see cref="RuntimeFieldHandle"/> structure that contains the handle to the internal metadata representation of a field.</param>
    /// <returns>A <see cref="FieldInfo"/> object representing the field specified by handle.</returns>
    /// <exception cref="ArgumentException">handle is invalid.</exception>
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
    {
        return CreateNew(OriginFieldInfo.GetFieldFromHandle(handle));
    }

    /// <summary>
    /// Gets a <see cref="FieldInfo"/> for the field represented by the specified handle, for the specified generic type.
    /// </summary>
    /// <param name="handle">A <see cref="RuntimeFieldHandle"/> structure that contains the handle to the internal metadata representation of a field.</param>
    /// <param name="declaringType">A <see cref="RuntimeFieldHandle"/> structure that contains the handle to the generic type that defines the field.</param>
    /// <returns>A <see cref="FieldInfo"/> object representing the field specified by <paramref name="handle"/>, in the generic type specified by <paramref name="declaringType"/>.</returns>
    /// <exception cref="ArgumentException"><paramref name="handle"/> is invalid. -or- <paramref name="declaringType"/> is not compatible with <paramref name="handle"/>. For example, <paramref name="declaringType"/> is the runtime type handle of the generic type definition, and <paramref name="handle"/> comes from a constructed type.</exception>
    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
    {
        return CreateNew(OriginFieldInfo.GetFieldFromHandle(handle, declaringType));
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
    /// Gets an array of types that identify the optional custom modifiers of the field.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that identify the optional custom modifiers of the current field, such as <see cref="IsConst"/>.</returns>
    public Type[] GetOptionalCustomModifiers()
    {
        return Type.GetList(Origin.GetOptionalCustomModifiers()).ToArray();
    }

    /// <summary>
    /// Returns a literal value associated with the field by a compiler.
    /// </summary>
    /// <returns>An <see cref="object"/> that contains the literal value associated with the field. If the literal value is a class type with an element value of zero, an exception is thrown.</returns>
    /// <exception cref="InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current field.</exception>
    /// <exception cref="FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
    /// <exception cref="NotSupportedException">The constant value for the field is not set.</exception>
    /// <exception cref="NullReferenceException">No Raw Constant Value.</exception>
    public object GetRawConstantValue()
    {
        return Origin.GetRawConstantValue() ?? throw new NullReferenceException("No Raw Constant Value.");
    }

    /// <summary>
    /// Gets an array of types that identify the required custom modifiers of the property.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that identify the required custom modifiers of the current property, such as <see cref="IsConst"/> or <see cref="IsImplicitlyDereferenced"/>.</returns>
    public Type[] GetRequiredCustomModifiers()
    {
        return Type.GetList(Origin.GetRequiredCustomModifiers()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, returns the value of a field supported by a given object.
    /// </summary>
    /// <param name="obj">The object whose field value will be returned.</param>
    /// <returns>An object containing the value of the field reflected by this instance.</returns>
    /// <exception cref="TargetException">The field is non-static and <paramref name="obj"/> is <see cref="Type.Void"/>. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="NotSupportedException">A field is marked literal, but the field does not have one of the accepted literal types.</exception>
    /// <exception cref="FieldAccessException">The caller does not have permission to access this field. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="ArgumentException">The method is neither declared nor inherited by the class of <paramref name="obj"/>.</exception>
    /// <exception cref="NullReferenceException">Invalid field value.</exception>
    public object GetValue(object obj)
    {
        return Origin.GetValue(obj != Type.Void ? obj : null) ?? throw new NullReferenceException("Invalid field value.");
    }

    /// <summary>
    /// Returns the value of a field supported by a given object.
    /// </summary>
    /// <param name="obj">A <see cref="TypedReference"/> structure that encapsulates a managed pointer to a location and a runtime representation of the type that might be stored at that location.</param>
    /// <returns>An <see cref="object"/> containing a field value.</returns>
    /// <exception cref="NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead.</exception>
    /// <exception cref="NullReferenceException">Invalid field value.</exception>
    public object GetValueDirect(TypedReference obj)
    {
        return Origin.GetValueDirect(obj) ?? throw new NullReferenceException("Invalid field value.");
    }

    /// <summary>
    /// Indicates whether two <see cref="FieldInfo"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(FieldInfo left, FieldInfo right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="FieldInfo"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(FieldInfo left, FieldInfo right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Sets the value of the field supported by the given object.
    /// </summary>
    /// <param name="obj">The object whose field value will be set.</param>
    /// <param name="value">The value to assign to the field.</param>
    /// <exception cref="FieldAccessException">The caller does not have permission to access this field. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="MemberAccessException"/>, instead.</exception>
    /// <exception cref="TargetException">The <paramref name="obj"/> parameter is <see cref="Type.Void"/> and the field is an instance field. Note: In .NET for Windows Store apps or the Portable Class Library, catch <see cref="Exception"/> instead.</exception>
    /// <exception cref="ArgumentException">The field does not exist on the object. -or- The <paramref name="value"/> parameter cannot be converted and stored in the field.</exception>
    public void SetValue(object obj, object value)
    {
        Origin.SetValue(obj != Type.Void ? obj : null, value);
    }

    /// <summary>
    /// When overridden in a derived class, sets the value of the field supported by the given object.
    /// </summary>
    /// <param name="obj">The object whose field value will be set.</param>
    /// <param name="value">The value to assign to the field.</param>
    /// <param name="invokeAttr">A field of <see cref="Binder"/> that specifies the type of binding that is desired (for example, <see cref="BindingFlags.CreateInstance"/> or <see cref="BindingFlags.ExactBinding"/>).</param>
    /// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection.</param>
    /// <param name="culture">The software preferences of a particular culture.</param>
    /// <exception cref="FieldAccessException">The caller does not have permission to access this field.</exception>
    /// <exception cref="TargetException">The <paramref name="obj"/> parameter is <see cref="Type.Void"/>and the field is an instance field.</exception>
    /// <exception cref="ArgumentException">The field does not exist on the object. -or- The <paramref name="value"/> parameter cannot be converted and stored in the field.</exception>
    public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
    {
        Origin.SetValue(obj != Type.Void ? obj : null, value, invokeAttr, binder != Assembly.DefaultBinder ? binder : null, culture);
    }

    /// <summary>
    /// Sets the value of the field supported by the given object.
    /// </summary>
    /// <param name="obj">A <see cref="TypedReference"/> structure that encapsulates a managed pointer to a location and a runtime representation of the type that can be stored at that location.</param>
    /// <param name="value">The value to assign to the field.</param>
    /// <exception cref="NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead.</exception>
    public void SetValueDirect(TypedReference obj, object value)
    {
        Origin.SetValueDirect(obj, value);
    }
}
