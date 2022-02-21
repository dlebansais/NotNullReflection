namespace NotNullReflection;

using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Globalization;
using System.Linq;
using NullReferenceException = System.NullReferenceException;
using InvalidOperationException = System.NullReferenceException;
using NotSupportedException = System.NotSupportedException;
using ArgumentException = System.ArgumentException;
using TypeLoadException = System.TypeLoadException;
using BadImageFormatException = System.BadImageFormatException;
using MethodAccessException = System.MethodAccessException;
using MissingFieldException = System.MissingFieldException;
using MissingMethodException = System.MissingMethodException;
using IndexOutOfRangeException = System.IndexOutOfRangeException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using Guid = System.Guid;
using Array = System.Array;
using TypedReference = System.TypedReference;
using TypeCode = System.TypeCode;
using RuntimeTypeHandle = System.RuntimeTypeHandle;
using TypeAttributes = System.Reflection.TypeAttributes;
using FieldAttributes = System.Reflection.FieldAttributes;
using MethodAttributes = System.Reflection.MethodAttributes;
using MethodImplAttributes = System.Reflection.MethodImplAttributes;
using BindingFlags = System.Reflection.BindingFlags;
using GenericParameterAttributes = System.Reflection.GenericParameterAttributes;
using MemberTypes = System.Reflection.MemberTypes;
using CallingConventions = System.Reflection.CallingConventions;
using OriginType = System.Type;
using OriginAssembly = System.Reflection.Assembly;
using OriginAssemblyName = System.Reflection.AssemblyName;
using OriginMethodBase = System.Reflection.MethodBase;
using OriginBinder = System.Reflection.Binder;
using OriginParameterModifier = System.Reflection.ParameterModifier;
using OriginModule = System.Reflection.Module;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginMemberInfo = System.Reflection.MemberInfo;
using OriginEventInfo = System.Reflection.EventInfo;
using OriginFieldInfo = System.Reflection.FieldInfo;
using OriginMethodInfo = System.Reflection.MethodInfo;
using OriginPropertyInfo = System.Reflection.PropertyInfo;
using OriginDefaultMemberAttribute = System.Reflection.DefaultMemberAttribute;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TypeFilter = System.Reflection.TypeFilter;
using MemberFilter = System.Reflection.MemberFilter;
using AmbiguousMatchException = System.Reflection.AmbiguousMatchException;
using TargetException = System.Reflection.TargetException;
using InterfaceMapping = System.Reflection.InterfaceMapping;
using System.IO;
using System.Runtime.Versioning;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Gets the <see cref="Assembly"/> in which the type is declared. For generic types, gets the <see cref="Assembly"/> in which the generic type is defined.
    /// </summary>
    /// <returns>A <see cref="Assembly"/> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
    public Assembly Assembly
    {
        get
        {
            return new Assembly(Origin.Assembly);
        }
    }

    /// <summary>
    /// Gets the assembly-qualified name of the type, which includes the name of the assembly from which this <see cref="Type"/> object was loaded.
    /// </summary>
    /// <returns>The assembly-qualified name of the <see cref="Type"/>, which includes the name of the assembly from which the <see cref="Type"/> was loaded, or throws an exception if the current instance represents a generic type parameter.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have an assembly-qualified name.</exception>
    public string AssemblyQualifiedName
    {
        get
        {
            return Origin.AssemblyQualifiedName ?? throw new NullReferenceException("Type doesn't have an assembly-qualified name.");
        }
    }

    /// <summary>
    /// Gets the attributes associated with the <see cref="Type"/>.
    /// </summary>
    /// <returns>A <see cref="TypeAttributes"/> object representing the attribute set of the <see cref="Type"/>, unless the <see cref="Type"/> represents a generic type parameter, in which case the value is unspecified.</returns>
    public TypeAttributes Attributes
    {
        get
        {
            return Origin.Attributes;
        }
    }

    /// <summary>
    /// Gets the type from which the current <see cref="Type"/> directly inherits.
    /// </summary>
    /// <returns>The <see cref="Type"/> from which the current <see cref="Type"/> directly inherits, or throws an exception if the current <see cref="Type"/> represents the <see cref="object"/> class or an interface.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a base type.</exception>
    public Type BaseType
    {
        get
        {
            return new Type(Origin.BaseType ?? throw new NullReferenceException("Type doesn't have a base type."));
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> object has type parameters that have not been replaced by specific types.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, false.</returns>
    public virtual bool ContainsGenericParameters
    {
        get
        {
            return Origin.ContainsGenericParameters;
        }
    }

    /// <summary>
    /// Gets a <see cref="OriginMethodBase"/> that represents the declaring method, if the current <see cref="Type"/> represents a type parameter of a generic method.
    /// </summary>
    /// <returns>If the current <see cref="Type"/> represents a type parameter of a generic method, a <see cref="OriginMethodBase"/> that represents declaring method; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a declaring methods.</exception>
    public OriginMethodBase DeclaringMethod
    {
        get
        {
            return Origin.DeclaringMethod ?? throw new NullReferenceException("Type doesn't have a declaring methods.");
        }
    }

    /// <summary>
    /// Gets the type that declares the current nested type or generic type parameter.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a declaring type.</exception>
    public Type DeclaringType
    {
        get
        {
            return new Type(Origin.DeclaringType ?? throw new NullReferenceException("Type doesn't have a declaring type."));
        }
    }

    /// <summary>
    /// Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="InvokeMember(string,BindingFlags,OriginBinder,object,object[],OriginParameterModifier[],CultureInfo,string[])"/>.
    /// </summary>
    /// <returns>A reference to the default binder used by the system.</returns>
    public static OriginBinder DefaultBinder
    {
        get
        {
            return OriginType.DefaultBinder;
        }
    }

    /// <summary>
    /// Gets the fully qualified name of the type, including its namespace but not its assembly.
    /// </summary>
    /// <returns>The fully qualified name of the type, including its namespace but not its assembly; or throws an exception if the current instance represents a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a full name.</exception>
    public string FullName
    {
        get
        {
            return Origin.FullName ?? throw new NullReferenceException("Type doesn't have a full name.");
        }
    }

    /// <summary>
    /// Gets a combination of <see cref="OriginType.GenericParameterAttributes"/> flags that describe the covariance and special constraints of the current generic type parameter.
    /// </summary>
    /// <returns>A bitwise combination of <see cref="OriginType.GenericParameterAttributes"/> values that describes the covariance and special constraints of the current generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> object is not a generic type parameter. That is, the <see cref="IsGenericParameter"/> property returns false.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    public GenericParameterAttributes GenericParameterAttributes
    {
        get
        {
            return Origin.GenericParameterAttributes;
        }
    }

    /// <summary>
    /// Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="Type"/> object represents a type parameter of a generic type or a generic method.
    /// </summary>
    /// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
    /// <exception cref="InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="IsGenericParameter"/> returns false.</exception>
    public int GenericParameterPosition
    {
        get
        {
            return Origin.GenericParameterPosition;
        }
    }

    /// <summary>
    /// Gets an array of the generic type arguments for this type.
    /// </summary>
    /// <returns>An array of the generic type arguments for this type.</returns>
    public Type[] GenericTypeArguments
    {
        get
        {
            return GetList(Origin.GenericTypeArguments).ToArray();
        }
    }

    /// <summary>
    /// Gets the GUID associated with the <see cref="Type"/>.
    /// </summary>
    /// <returns>The GUID associated with the <see cref="Type"/>.</returns>
    public Guid GUID
    {
        get
        {
            return Origin.GUID;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current System.Type encompasses or refers to another type; that is, whether the current System.Type is an array, a pointer, or is passed by reference.
    /// </summary>
    /// <returns>true if the System.Type is an array, a pointer, or is passed by reference; otherwise, false.</returns>
    public bool HasElementType
    {
        get
        {
            return Origin.HasElementType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is abstract and must be overridden.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is abstract; otherwise, false.</returns>
    public bool IsAbstract
    {
        get
        {
            return Origin.IsAbstract;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the string format attribute AnsiClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns>true if the string format attribute AnsiClass is selected for the <see cref="Type"/>; otherwise, false.</returns>
    public bool IsAnsiClass
    {
        get
        {
            return Origin.IsAnsiClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is an array.
    /// </summary>
    /// <returns>true if the current type is an array; otherwise, false.</returns>
    public bool IsArray
    {
        get
        {
            return Origin.IsArray;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the string format attribute AutoClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns>true if the string format attribute AutoClass is selected for the <see cref="Type"/>; otherwise, false.</returns>
    public bool IsAutoClass
    {
        get
        {
            return Origin.IsAutoClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out automatically by the common language runtime.
    /// </summary>
    /// <returns>true if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.AutoLayout"/>; otherwise, false.</returns>
    public bool IsAutoLayout
    {
        get
        {
            return Origin.IsAutoLayout;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is passed by reference.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is passed by reference; otherwise, false.</returns>
    public bool IsByRef
    {
        get
        {
            return Origin.IsByRef;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is a byref-like structure.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a a byref-like structure; otherwise, false.</returns>
    public bool IsByRefLike
    {
        get
        {
            return Origin.IsByRefLike;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a class or a delegate; that is, not a value type or interface.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a class; otherwise, false.</returns>
    public bool IsClass
    {
        get
        {
            return Origin.IsClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a COM object.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a COM object; otherwise, false.</returns>
    public bool IsCOMObject
    {
        get
        {
            return Origin.IsCOMObject;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this object represents a constructed generic type. You can create instances of a constructed generic type.
    /// </summary>
    /// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
    public bool IsConstructedGenericType
    {
        get
        {
            return Origin.IsConstructedGenericType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> can be hosted in a context.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> can be hosted in a context; otherwise, false.</returns>
    public bool IsContextful
    {
        get
        {
            return Origin.IsContextful;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents an enumeration.
    /// </summary>
    /// <returns>true if the current <see cref="Type"/> represents an enumeration; otherwise, false.</returns>
    public bool IsEnum
    {
        get
        {
            return Origin.IsEnum;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out at explicitly specified offsets.
    /// </summary>
    /// <returns>true if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.ExplicitLayout"/>; otherwise, false.</returns>
    public bool IsExplicitLayout
    {
        get
        {
            return Origin.IsExplicitLayout;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic method.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> object represents a type parameter of a generic method definition; otherwise, false.</returns>
    public bool IsGenericMethodParameter
    {
        get
        {
            return Origin.IsGenericMethodParameter;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic type or method.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> object represents a type parameter of a generic type definition or generic method definition; otherwise, false.</returns>
    public bool IsGenericParameter
    {
        get
        {
            return Origin.IsGenericParameter;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is a generic type.
    /// </summary>
    /// <returns>true if the current type is a generic type; otherwise, false.</returns>
    public bool IsGenericType
    {
        get
        {
            return Origin.IsGenericType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a generic type definition, from which other generic types can be constructed.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> object represents a generic type definition; otherwise, false.</returns>
    public bool IsGenericTypeDefinition
    {
        get
        {
            return Origin.IsGenericTypeDefinition;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> represents a type parameter in the definition of a generic type.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> object represents a type parameter of a generic type definition; otherwise, false.</returns>
    public bool IsGenericTypeParameter
    {
        get
        {
            return Origin.IsGenericTypeParameter;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> has a <see cref="ComImportAttribute"/> attribute applied, indicating that it was imported from a COM type library.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> has a <see cref="ComImportAttribute"/>; otherwise, false.</returns>
    public bool IsImport
    {
        get
        {
            return Origin.IsImport;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is an interface; that is, not a class or a value type.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is an interface; otherwise, false.</returns>
    public bool IsInterface
    {
        get
        {
            return Origin.IsInterface;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the fields of the current type are laid out sequentially, in the order that they were defined or emitted to the metadata.
    /// </summary>
    /// <returns>true if the <see cref="Attributes"/> property of the current type includes <see cref="TypeAttributes.SequentialLayout"/>; otherwise, false.</returns>
    public bool IsLayoutSequential
    {
        get
        {
            return Origin.IsLayoutSequential;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is marshaled by reference.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is marshaled by reference; otherwise, false.</returns>
    public bool IsMarshalByRef
    {
        get
        {
            return Origin.IsMarshalByRef;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current <see cref="Type"/> object represents a type whose definition is nested inside the definition of another type.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested inside another type; otherwise, false.</returns>
    public bool IsNested
    {
        get
        {
            return Origin.IsNested;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only within its own assembly.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested and visible only within its own assembly; otherwise, false.</returns>
    public bool IsNestedAssembly
    {
        get
        {
            return Origin.IsNestedAssembly;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only to classes that belong to both its own family and its own assembly.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, false.</returns>
    public bool IsNestedFamANDAssem
    {
        get
        {
            return Origin.IsNestedFamANDAssem;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only within its own family.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested and visible only within its own family; otherwise, false.</returns>
    public bool IsNestedFamily
    {
        get
        {
            return Origin.IsNestedFamily;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and visible only to classes that belong to either its own family or to its own assembly.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, false.</returns>
    public bool IsNestedFamORAssem
    {
        get
        {
            return Origin.IsNestedFamORAssem;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is nested and declared private.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is nested and declared private; otherwise, false.</returns>
    public bool IsNestedPrivate
    {
        get
        {
            return Origin.IsNestedPrivate;
        }
    }

    /// <summary>
    /// Gets a value indicating whether a class is nested and declared public.
    /// </summary>
    /// <returns>true if the class is nested and declared public; otherwise, false.</returns>
    public bool IsNestedPublic
    {
        get
        {
            return Origin.IsNestedPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is not declared public.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is not declared public and is not a nested type; otherwise, false.</returns>
    public bool IsNotPublic
    {
        get
        {
            return Origin.IsNotPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a pointer.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a pointer; otherwise, false.</returns>
    public bool IsPointer
    {
        get
        {
            return Origin.IsPointer;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is one of the primitive types.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is one of the primitive types; otherwise, false.</returns>
    public bool IsPrimitive
    {
        get
        {
            return Origin.IsPointer;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is declared public.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is declared public and is not a nested type; otherwise, false.</returns>
    public bool IsPublic
    {
        get
        {
            return Origin.IsPublic;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is declared sealed.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is declared sealed; otherwise, false.</returns>
    public bool IsSealed
    {
        get
        {
            return Origin.IsSealed;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.
    /// </summary>
    /// <returns>true if the current type is security-critical or security-safe-critical at the current trust level; false if it is transparent.</returns>
    public bool IsSecurityCritical
    {
        get
        {
            return Origin.IsSecurityCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.
    /// </summary>
    /// <returns>true if the current type is security-safe-critical at the current trust level; false if it is security-critical or transparent.</returns>
    public bool IsSecuritySafeCritical
    {
        get
        {
            return Origin.IsSecuritySafeCritical;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the current type is transparent at the current trust level, and therefore cannot perform critical operations.
    /// </summary>
    /// <returns>true if the type is security-transparent at the current trust level; otherwise, false.</returns>
    public bool IsSecurityTransparent
    {
        get
        {
            return Origin.IsSecurityTransparent;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is serializable.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is serializable; otherwise, false.</returns>
    public bool IsSerializable
    {
        get
        {
            return Origin.IsSerializable;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is a signature type.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a signature type; otherwise, false.</returns>
    public bool IsSignatureType
    {
        get
        {
            return Origin.IsSignatureType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type has a name that requires special handling.
    /// </summary>
    /// <returns>true if the type has a name that requires special handling; otherwise, false.</returns>
    public bool IsSpecialName
    {
        get
        {
            return Origin.IsSpecialName;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is an array type that can represent only a single-dimensional array with a zero lower bound.
    /// </summary>
    /// <returns>true if the current <see cref="Type"/> is an array type that can represent only a single-dimensional array with a zero lower bound; otherwise, false.</returns>
    public bool IsSZArray
    {
        get
        {
            return Origin.IsSZArray;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is a type definition.
    /// </summary>
    /// <returns>true if the current <see cref="Type"/> is a type definition; otherwise, false.</returns>
    public bool IsTypeDefinition
    {
        get
        {
            return Origin.IsTypeDefinition;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the string format attribute UnicodeClass is selected for the <see cref="Type"/>.
    /// </summary>
    /// <returns>true if the string format attribute UnicodeClass is selected for the <see cref="Type"/>; otherwise, false.</returns>
    public bool IsUnicodeClass
    {
        get
        {
            return Origin.IsUnicodeClass;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> is a value type.
    /// </summary>
    /// <returns>true if the <see cref="Type"/> is a value type; otherwise, false.</returns>
    public bool IsValueType
    {
        get
        {
            return Origin.IsValueType;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the type is an array type that can represent a multi-dimensional array or an array with an arbitrary lower bound.
    /// </summary>
    /// <returns>true if the current <see cref="Type"/> is an array type that can represent a multi-dimensional array or an array with an arbitrary lower bound; otherwise, false.</returns>
    public bool IsVariableBoundArray
    {
        get
        {
            return Origin.IsVariableBoundArray;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Type"/> can be accessed by code outside the assembly.
    /// </summary>
    /// <returns>true if the current <see cref="Type"/> is a public type or a public nested type such that all the enclosing types are public; otherwise, false.</returns>
    public bool IsVisible
    {
        get
        {
            return Origin.IsVisible;
        }
    }

    /// <summary>
    /// Gets a <see cref="MemberTypes"/> value indicating that this member is a type or a nested type.
    /// </summary>
    /// <returns>A <see cref="MemberTypes"/> value indicating that this member is a type or a nested type.</returns>
    public MemberTypes MemberType
    {
        get
        {
            return Origin.MemberType;
        }
    }

    /// <summary>
    /// Gets the module (the DLL) in which the current <see cref="Type"/> is defined.
    /// </summary>
    /// <returns>The module in which the current <see cref="Type"/> is defined.</returns>
    public OriginModule Module
    {
        get
        {
            return Origin.Module;
        }
    }

    /// <summary>
    /// Gets the namespace of the <see cref="Type"/>.
    /// </summary>
    /// <returns>The namespace of the <see cref="Type"/>; throws an exception if the current instance has no namespace or represents a generic parameter.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a namespace.</exception>
    public string Namespace
    {
        get
        {
            return Origin.Namespace ?? throw new NullReferenceException("Type doesn't have a namespace.");
        }
    }

    /// <summary>
    /// Gets the class object that was used to obtain this member.
    /// </summary>
    /// <returns>The Type object through which this <see cref="Type"/> object was obtained.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a reflected type.</exception>
    public Type ReflectedType
    {
        get
        {
            return new Type(Origin.ReflectedType ?? throw new NullReferenceException("Type doesn't have a reflected type."));
        }
    }

    /// <summary>
    /// Gets a <see cref="System.Runtime.InteropServices.StructLayoutAttribute"/> that describes the layout of the current type.
    /// </summary>
    /// <returns>Gets a <see cref="System.Runtime.InteropServices.StructLayoutAttribute"/> that describes the gross layout features of the current type.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="NullReferenceException">Type doesn't have a StructLayout attribute.</exception>
    public StructLayoutAttribute StructLayoutAttribute
    {
        get
        {
            return Origin.StructLayoutAttribute ?? throw new NullReferenceException("Type doesn't have a StructLayout attribute.");
        }
    }

    /// <summary>
    /// Gets the handle for the current <see cref="Type"/>.
    /// </summary>
    /// <returns>The handle for the current <see cref="Type"/>.</returns>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
    public RuntimeTypeHandle TypeHandle
    {
        get
        {
            return Origin.TypeHandle;
        }
    }

    /// <summary>
    /// Gets the initializer for the type.
    /// </summary>
    /// <returns>An object that contains the name of the class constructor for the <see cref="Type"/>.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have a type initializer.</exception>
    public OriginConstructorInfo TypeInitializer
    {
        get
        {
            return Origin.TypeInitializer ?? throw new NullReferenceException("Type doesn't have a type initializer.");
        }
    }

    /// <summary>
    /// Gets the type provided by the common language runtime that represents this type.
    /// </summary>
    /// <returns>The underlying system type for the System.Type.</returns>
    public Type UnderlyingSystemType
    {
        get
        {
            return new Type(Origin.UnderlyingSystemType);
        }
    }

    /// <summary>
    /// Determines if the underlying system type of the current <see cref="Type"/> object is the same as the underlying system type of the specified <see cref="object"/>.
    /// </summary>
    /// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="Type"/>. For the comparison to succeed, <paramref name="o"/> must be able to be cast or converted to an object of type <see cref="Type"/>.</param>
    /// <returns>true if the underlying system type of <paramref name="o"/> is the same as the underlying system type of the current S<see cref="Type"/>; otherwise, false. This method also returns false if <paramref name="o"/> cannot be cast or converted to a <see cref="Type"/> object.</returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object o)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        return o is Type AsType && Origin.Equals(AsType.Origin);
    }

    /// <summary>
    /// Determines if the underlying system type of the current <see cref="Type"/> is the same as the underlying system type of the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="Type"/>.</param>
    /// <returns>true if the underlying system type of <paramref name="o"/> is the same as the underlying system type of the current <see cref="Type"/>; otherwise, false.</returns>
    public bool Equals(Type o)
    {
        return Origin.Equals(o.Origin);
    }

    /// <summary>
    /// Returns an array of <see cref="Type"/> objects representing a filtered list of interfaces implemented or inherited by the current <see cref="Type"/>.
    /// </summary>
    /// <param name="filter">The delegate that compares the interfaces against <paramref name="filterCriteria"/>.</param>
    /// <param name="filterCriteria">The search criteria that determines whether an interface should be included in the returned array.</param>
    /// <returns>An array of <see cref="Type"/> objects representing a filtered list of the interfaces implemented or inherited by the current System.Type, or an empty array if no interfaces matching the filter are implemented or inherited by the current <see cref="Type"/>.</returns>
    /// <exception cref="TargetInvocationException">A static initializer is invoked and throws an exception.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
    {
        return GetList(Origin.FindInterfaces(filter, filterCriteria)).ToArray();
    }

    /// <summary>
    /// Returns a filtered array of <see cref="OriginMemberInfo"/> objects of the specified member type.
    /// </summary>
    /// <param name="memberType">A bitwise combination of the enumeration values that indicates the type of member to search for.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="filter">The delegate that does the comparisons, returning true if the member currently being inspected matches the <paramref name="filterCriteria"/> and false otherwise.</param>
    /// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of <see cref="OriginMemberInfo"/> objects. The fields of <see cref="FieldAttributes"/>, <see cref="MethodAttributes"/>, and <see cref="MethodImplAttributes"/> can be used in conjunction with the <see cref="OriginType.FilterAttribute"/> delegate supplied by this class.</param>
    /// <returns>A filtered array of <see cref="OriginMemberInfo"/> objects of the specified member type. -or- An empty array if the current <see cref="Type"/> does not have members of type <paramref name="memberType"/> that match the filter criteria.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public virtual OriginMemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
    {
        return Origin.FindMembers(memberType, bindingAttr, filter, filterCriteria);
    }

    /// <summary>
    /// Gets the number of dimensions in an array.
    /// </summary>
    /// <returns>An integer that contains the number of dimensions in the current type.</returns>
    /// <exception cref="NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead.</exception>
    /// <exception cref="ArgumentException">The current type is not an array.</exception>
    public int GetArrayRank()
    {
        return Origin.GetArrayRank();
    }

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. -or- <see cref="OriginType.EmptyTypes"/>.</param>
    /// <returns>A <see cref="OriginConstructorInfo"/> object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo GetConstructor(BindingFlags bindingAttr, Type[] types)
    {
        return Origin.GetConstructor(bindingAttr, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters..</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo GetConstructor(BindingFlags bindingAttr, OriginBinder binder, CallingConventions callConvention, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetConstructor(bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. -or- A null reference (Nothing in Visual Basic), to use the System.Type.DefaultBinder.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. -or- <see cref="OriginType.EmptyTypes"/>.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>A <see cref="OriginConstructorInfo"/> object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo GetConstructor(BindingFlags bindingAttr, OriginBinder binder, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetConstructor(bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Searches for a public instance constructor whose parameters match the types in the specified array.
    /// </summary>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="OriginType.EmptyTypes"/>.</param>
    /// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public OriginConstructorInfo GetConstructor(Type[] types)
    {
        return Origin.GetConstructor(GetOriginList(types).ToArray()) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Returns all the public constructors defined for the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginConstructorInfo"/> objects representing all the public instance constructors defined for the current <see cref="Type"/>, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="Type"/>, or if the current <see cref="Type"/> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="OriginConstructorInfo"/> is returned.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
    public OriginConstructorInfo[] GetConstructors()
    {
        return Origin.GetConstructors();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the constructors defined for the current <see cref="Type"/>, using the specified <see cref="BindingFlags"/>.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginConstructorInfo"/> objects representing all constructors defined for the current <see cref="Type"/> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="OriginConstructorInfo"/> if no constructors are defined for the current <see cref="Type"/>, if none of the defined constructors match the binding constraints, or if the current <see cref="Type"/> represents a type parameter in the definition of a generic type or generic method.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
        return Origin.GetConstructors(bindingAttr);
    }

    /// <summary>
    /// Searches for the members defined for the current <see cref="Type"/> whose <see cref="OriginDefaultMemberAttribute"/> is set.
    /// </summary>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing all default members of the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginMemberInfo"/>, if the current <see cref="Type"/> does not have default members.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
    public OriginMemberInfo[] GetDefaultMembers()
    {
        return Origin.GetDefaultMembers();
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="Type"/> of the object encompassed or referred to by the current array, pointer or reference type.
    /// </summary>
    /// <returns>The <see cref="Type"/> of the object encompassed or referred to by the current array, pointer, or reference type, or throws an exception if the current <see cref="Type"/> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have an element type.</exception>
    public Type GetElementType()
    {
        return new Type(Origin.GetElementType() ?? throw new NullReferenceException("Type doesn't have an element type."));
    }

    /// <summary>
    /// Returns the name of the constant that has the specified value, for the current enumeration type.
    /// </summary>
    /// <param name="value">The value whose name is to be retrieved.</param>
    /// <returns>The name of the member of the current enumeration type that has the specified value, or throws an exception if no such constant is found.</returns>
    /// <exception cref="ArgumentException">The current type is not an enumeration. -or- value is neither of the current type nor does it have the same underlying type as the current type.</exception>
    /// <exception cref="NullReferenceException">Enum name not found.</exception>
    public string GetEnumName(object value)
    {
        return Origin.GetEnumName(value) ?? throw new NullReferenceException("Enum name not found.");
    }

    /// <summary>
    /// Returns the names of the members of the current enumeration type.
    /// </summary>
    /// <returns>An array that contains the names of the members of the enumeration.</returns>
    /// <exception cref="ArgumentException">The current type is not an enumeration.</exception>
    public string[] GetEnumNames()
    {
        return Origin.GetEnumNames();
    }

    /// <summary>
    /// Returns the underlying type of the current enumeration type.
    /// </summary>
    /// <returns>The underlying type of the current enumeration.</returns>
    /// <exception cref="ArgumentException">The current type is not an enumeration. -or- The enumeration type is not valid, because it contains more than one instance field.</exception>
    public Type GetEnumUnderlyingType()
    {
        return new Type(Origin.GetEnumUnderlyingType());
    }

    /// <summary>
    /// Returns an array of the values of the constants in the current enumeration type.
    /// </summary>
    /// <returns>An array that contains the values. The elements of the array are sorted by the binary values (that is, the unsigned values) of the enumeration constants.</returns>
    /// <exception cref="ArgumentException">The current type is not an enumeration.</exception>
    public Array GetEnumValues()
    {
        return Origin.GetEnumValues();
    }

    /// <summary>
    /// Returns the <see cref="OriginEventInfo"/> object representing the specified public event.
    /// </summary>
    /// <param name="name">The string containing the name of an event that is declared or inherited by the current <see cref="Type"/>.</param>
    /// <returns>The object representing the specified public event that is declared or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Event not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
    public OriginEventInfo GetEvent(string name)
    {
        return Origin.GetEvent(name) ?? throw new NullReferenceException("Event not found.");
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="OriginEventInfo"/> object representing the specified event, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of an event which is declared or inherited by the current <see cref="Type"/>.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>The object representing the specified event that is declared or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Event not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
    public OriginEventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
        return Origin.GetEvent(name, bindingAttr) ?? throw new NullReferenceException("Event not found.");
    }

    /// <summary>
    /// Returns all the public events that are declared or inherited by the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginEventInfo"/> objects representing all the public events which are declared or inherited by the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginEventInfo"/>, if the current <see cref="Type"/> does not have public events.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
    public OriginEventInfo[] GetEvents()
    {
        return Origin.GetEvents();
    }

    /// <summary>
    /// When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginEventInfo"/> objects representing all events that are declared or inherited by the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="OriginEventInfo"/>, if the current <see cref="Type"/> does not have events, or if none of the events match the binding constraints.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
    public OriginEventInfo[] GetEvents(BindingFlags bindingAttr)
    {
        return Origin.GetEvents(bindingAttr);
    }

    /// <summary>
    /// Searches for the public field with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the data field to get.</param>
    /// <returns>An object representing the public field with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NotSupportedException">This <see cref="Type"/> object is a <see cref="TypeBuilder"/> whose <see cref="TypeBuilder.CreateType"/> method has not yet been called.</exception>
    /// <exception cref="NullReferenceException">Field not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
    public OriginFieldInfo GetField(string name)
    {
        return Origin.GetField(name) ?? throw new NullReferenceException("Field not found.");
    }

    /// <summary>
    /// Searches for the specified field, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the data field to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the field that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Field not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
    public OriginFieldInfo GetField(string name, BindingFlags bindingAttr)
    {
        return Origin.GetField(name, bindingAttr) ?? throw new NullReferenceException("Field not found.");
    }

    /// <summary>
    /// Returns all the public fields of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginFieldInfo"/> objects representing all the public fields defined for the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginFieldInfo"/>, if no public fields are defined for the current <see cref="Type"/>.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
    public OriginFieldInfo[] GetFields()
    {
        return Origin.GetFields();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the fields defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginFieldInfo"/> objects representing all fields defined for the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="OriginFieldInfo"/>, if no fields are defined for the current <see cref="Type"/>, or if none of the defined fields match the binding constraints.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
    public OriginFieldInfo[] GetFields(BindingFlags bindingAttr)
    {
        return Origin.GetFields(bindingAttr);
    }

    /// <summary>
    /// Returns an array of <see cref="Type"/> objects that represent the type arguments of a closed generic type or the type parameters of a generic type definition.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that represent the type arguments of a generic type. Returns an empty array if the current type is not a generic type.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    public Type[] GetGenericArguments()
    {
        return GetList(Origin.GetGenericArguments()).ToArray();
    }

    /// <summary>
    /// Returns an array of <see cref="Type"/> objects that represent the constraints on the current generic type parameter.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects that represent the constraints on the current generic type parameter.</returns>
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> object is not a generic type parameter. That is, the <see cref="Type.IsGenericParameter"/> property returns false.</exception>
    public virtual Type[] GetGenericParameterConstraints()
    {
        return GetList(Origin.GetGenericParameterConstraints()).ToArray();
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents a generic type definition from which the current generic type can be constructed.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing a generic type from which the current type can be constructed.</returns>
    /// <exception cref="InvalidOperationException">The current type is not a generic type. That is, <see cref="Type.IsGenericType"/> returns false.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    public virtual Type GetGenericTypeDefinition()
    {
        return new Type(Origin.GetGenericTypeDefinition());
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code for this instance.</returns>
    public override int GetHashCode()
    {
        return Origin.GetHashCode();
    }

    /// <summary>
    /// Searches for the interface with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
    /// <returns>An object representing the interface with the specified name, implemented or inherited by the current The current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">The current <see cref="Type"/> represents a type that implements the same generic interface with different type arguments.</exception>
    /// <exception cref="NullReferenceException">Interface not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public Type GetInterface(string name)
    {
        return new Type(Origin.GetInterface(name) ?? throw new NullReferenceException("Interface not found."));
    }

    /// <summary>
    /// When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.
    /// </summary>
    /// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
    /// <param name="ignoreCase">true to ignore the case of that part of name that specifies the simple interface name (the part that specifies the namespace must be correctly cased). -or- false to perform a case-sensitive search for all parts of name.</param>
    /// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">The current <see cref="Type"/> represents a type that implements the same generic interface with different type arguments.</exception>
    /// <exception cref="NullReferenceException">Interface not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public Type GetInterface(string name, bool ignoreCase)
    {
        return new Type(Origin.GetInterface(name, ignoreCase) ?? throw new NullReferenceException("Interface not found."));
    }

    /// <summary>
    /// Returns an interface mapping for the specified interface type.
    /// </summary>
    /// <param name="interfaceType">The interface type to retrieve a mapping for.</param>
    /// <returns>An object that represents the interface mapping for <paramref name="interfaceType"/>.</returns>
    /// <exception cref="ArgumentException"><paramref name="interfaceType"/> is not implemented by the current type. -or- The <paramref name="interfaceType"/> argument does not refer to an interface. -or- The current instance of <paramref name="interfaceType"/> argument is an open generic type; that is, the <see cref="Type.ContainsGenericParameters"/> property returns true. -or- <paramref name="interfaceType"/> is a generic interface, and the current type is an array type.</exception>
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> represents a generic type parameter; that is, <see cref="Type.IsGenericParameter"/> is true.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    public virtual InterfaceMapping GetInterfaceMap([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] Type interfaceType)
    {
        return Origin.GetInterfaceMap(interfaceType.Origin);
    }

    /// <summary>
    /// When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects representing all the interfaces implemented or inherited by the current <see cref="Type"/>. -or- An empty array of type <see cref="Type"/>, if no interfaces are implemented or inherited by the current <see cref="Type"/>.</returns>
    /// <exception cref="TargetInvocationException">A static initializer is invoked and throws an exception.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public Type[] GetInterfaces()
    {
        return GetList(Origin.GetInterfaces()).ToArray();
    }

    /// <summary>
    /// Searches for the public members with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public members to get.</param>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
    public OriginMemberInfo[] GetMember(string name)
    {
        return Origin.GetMember(name);
    }

    /// <summary>
    /// Searches for the specified members, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the members to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public OriginMemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
        return Origin.GetMember(name, bindingAttr);
    }

    /// <summary>
    /// Searches for the specified members of the specified member type, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the members to get.</param>
    /// <param name="type">The value to search for.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
    /// <exception cref="NotSupportedException">A derived class must provide an implementation.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public OriginMemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
        return Origin.GetMember(name, type, bindingAttr);
    }

    /// <summary>
    /// Searches for the <see cref="OriginMemberInfo"/> on the current <see cref="Type"/> that matches the specified <see cref="OriginMemberInfo"/>.
    /// </summary>
    /// <param name="member">The <see cref="OriginMemberInfo"/> to find on the current <see cref="Type"/>.</param>
    /// <returns>An object representing the member on the current <see cref="Type"/> that matches the specified member.</returns>
    /// <exception cref="ArgumentException">member does not match a member on the current <see cref="Type"/>.</exception>
    public OriginMemberInfo GetMemberWithSameMetadataDefinitionAs(OriginMemberInfo member)
    {
        return Origin.GetMemberWithSameMetadataDefinitionAs(member);
    }

    /// <summary>
    /// Returns all the public members of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing all the public members of the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginMemberInfo"/>, if the current <see cref="Type"/> does not have public members.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
    public OriginMemberInfo[] GetMembers()
    {
        return Origin.GetMembers();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the members defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginMemberInfo"/> objects representing all members defined for the current System.Type that match the specified binding constraints. -or- An empty array if no members are defined for the current <see cref="Type"/>, or if none of the defined members match the binding constraints.</returns>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public OriginMemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
        return Origin.GetMembers(bindingAttr);
    }

    /// <summary>
    /// Searches for the public method with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <returns>An object that represents the public method with the specified name, if found; otherwise, null.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo GetMethod(string name)
    {
        return Origin.GetMethod(name) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified generic parameter count, argument types, modifiers, binding constraints and calling convention, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, OriginBinder binder, CallingConventions callConvention, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, genericParameterCount, bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified generic parameter count, argument types, modifiers and binding constraints, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, OriginBinder binder, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, genericParameterCount, bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified generic parameter count and argument types.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <returns>An object representing the public method whose parameters match the specified generic parameter count and argument types, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo GetMethod(string name, int genericParameterCount, Type[] types)
    {
        return Origin.GetMethod(name, genericParameterCount, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified generic parameter count, argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public method that matches the specified generic parameter count, argument types and modifiers, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo GetMethod(string name, int genericParameterCount, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, genericParameterCount, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
        return Origin.GetMethod(name, bindingAttr) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, BindingFlags bindingAttr, Type[] types)
    {
        return Origin.GetMethod(name, bindingAttr, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, BindingFlags bindingAttr, OriginBinder binder, CallingConventions callConvention, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo GetMethod(string name, BindingFlags bindingAttr, OriginBinder binder, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified argument types.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <returns>An object representing the public method whose parameters match the specified argument types, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and specified parameters.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo GetMethod(string name, Type[] types)
    {
        return Origin.GetMethod(name, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and specified parameters.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo GetMethod(string name, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetMethod(name, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found.");
    }

    /// <summary>
    /// Returns all the public methods of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginMethodInfo"/> objects representing all the public methods defined for the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginMethodInfo"/>, if no public methods are defined for the current <see cref="Type"/>.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public OriginMethodInfo[] GetMethods()
    {
        return Origin.GetMethods();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the methods defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginMethodInfo"/> objects representing all methods defined for the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="OriginMethodInfo"/>, if no methods are defined for the current <see cref="Type"/>, or if none of the defined methods match the binding constraints.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public OriginMethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
        return Origin.GetMethods(bindingAttr);
    }

    /// <summary>
    /// Searches for the public nested type with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the nested type to get.</param>
    /// <returns>An object representing the public nested type with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Nested type not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
    public Type GetNestedType(string name)
    {
        return new Type(Origin.GetNestedType(name) ?? throw new NullReferenceException("Nested type not found."));
    }

    /// <summary>
    /// When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the nested type to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the nested type that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Nested type not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
    public Type GetNestedType(string name, BindingFlags bindingAttr)
    {
        return new Type(Origin.GetNestedType(name, bindingAttr) ?? throw new NullReferenceException("Nested type not found."));
    }

    /// <summary>
    /// Returns the public types nested in the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects representing the public types nested in the current <see cref="Type"/> (the search is not recursive), or an empty array of type <see cref="Type"/> if no public types are nested in the current <see cref="Type"/>.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
    public Type[] GetNestedTypes()
    {
        return GetList(Origin.GetNestedTypes()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the types nested in the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An array of <see cref="Type"/> objects representing all the types nested in the current <see cref="Type"/> that match the specified binding constraints (the search is not recursive), or an empty array of type <see cref="Type"/>, if no nested types are found that match the binding constraints.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
    public Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
        return GetList(Origin.GetNestedTypes(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Returns all the public properties of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginPropertyInfo"/> objects representing all public properties of the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginPropertyInfo"/>, if the current <see cref="Type"/> does not have public properties.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo[] GetProperties()
    {
        return Origin.GetProperties();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the properties of the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of objects representing all properties of the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="OriginPropertyInfo"/>, if the current <see cref="Type"/> does not have properties, or if none of the properties match the binding constraints.</returns>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public OriginPropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
        return Origin.GetProperties(bindingAttr);
    }

    /// <summary>
    /// Searches for the public property with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <returns>An object representing the public property with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo GetProperty(string name)
    {
        return Origin.GetProperty(name) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the specified property, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the property to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public OriginPropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
        return Origin.GetProperty(name, bindingAttr) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the property to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public OriginPropertyInfo GetProperty(string name, BindingFlags bindingAttr, OriginBinder binder, Type returnType, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetProperty(name, bindingAttr, binder != DefaultBinder ? binder : null, returnType.Origin, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the public property with the specified name and return type.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <returns>An object representing the public property with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo GetProperty(string name, Type returnType)
    {
        return Origin.GetProperty(name, returnType.Origin) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the specified public property whose parameters match the specified argument types.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo GetProperty(string name, Type returnType, Type[] types)
    {
        return Origin.GetProperty(name, returnType.Origin, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the specified public property whose parameters match the specified argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo? GetProperty(string name, Type returnType, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetProperty(name, returnType.Origin, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Searches for the specified public property whose parameters match the specified argument types.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public OriginPropertyInfo? GetProperty(string name, Type[] types)
    {
        return Origin.GetProperty(name, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Property not found.");
    }

    /// <summary>
    /// Gets the current <see cref="Type"/>.
    /// </summary>
    /// <returns>The current <see cref="Type"/>.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    public new Type GetType()
    {
        return new Type(Origin.GetType());
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, performing a case-sensitive search.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <returns>The type with the specified name, if found; otherwise, thows an exception.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="ArgumentException"><paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- typeName represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="TypeLoadException"><paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException">Type not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName)
    {
        return new Type(OriginType.GetType(typeName) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, performing a case-sensitive search and specifying exception to throw if the type is not found.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="throwOnError">true to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; false to throw a <see cref="NullReferenceException"/> exception. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true and the type is not found. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid syntax. For example, "MyType[,*,]". -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is true and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="IOException"/>, instead.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type is not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName, bool throwOnError)
    {
        return new Type(OriginType.GetType(typeName, throwOnError) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the <see cref="Type"/> with the specified name, specifying whether to throw an exception if the type is not found and whether to perform a case-sensitive search.
    /// </summary>
    /// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="Type.AssemblyQualifiedName"/>. If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="throwOnError">true to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; false to throw a <see cref="NullReferenceException"/> exception. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName"/>, false to perform a case-sensitive search for <paramref name="typeName"/>.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true and the type is not found. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid syntax. For example, "MyType[,*,]". -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is true and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type is not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
        return new Type(OriginType.GetType(typeName, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <returns>The type with the specified name, or throws an exception if the type is not found.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="TypeLoadException"><paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException">Type not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : new Assembly(assembly), typeName, ignoreCase).Origin : null;

        return new Type(OriginType.GetType(typeName, AssemblyResolver, TypeResolver) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, specifying whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <param name="throwOnError">true to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; false to throw a <see cref="NullReferenceException"/> exception. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true and the type is not found. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid syntax (for example, "MyType[,*,]"). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is true and the assembly or one of its dependencies was not found. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type is not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : new Assembly(assembly), typeName, ignoreCase).Origin : null;

        return new Type(OriginType.GetType(typeName, AssemblyResolver, TypeResolver, throwOnError) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the type with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.
    /// </summary>
    /// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver"/> parameter is provided, the type name can be any string that <paramref name="typeResolver"/> is capable of resolving. If the <paramref name="assemblyResolver"/> parameter is provided or if standard type resolution is used, <paramref name="typeName"/> must be an assembly-qualified name (see <see cref="Type.AssemblyQualifiedName"/>), unless the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
    /// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName"/>. The assembly name is passed to <paramref name="assemblyResolver"/> as an <see cref="OriginAssemblyName"/> object. If <paramref name="typeName"/> does not contain the name of an assembly, <paramref name="assemblyResolver"/> is not called. If <paramref name="assemblyResolver"/> is not supplied, standard assembly resolution is performed. Caution Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
    /// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName"/> from the assembly that is returned by <paramref name="assemblyResolver"/> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver"/> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution Do not pass methods from unknown or untrusted callers.</param>
    /// <param name="throwOnError">true to throw a <see cref="TypeLoadException"/> exception if the type cannot be found; false to throw a <see cref="NullReferenceException"/> exception. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
    /// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName"/>, false to perform a case-sensitive search for <paramref name="typeName"/>.</param>
    /// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError"/> parameter specifies which exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError"/>. See the Exceptions section.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="TypeLoadException"><paramref name="throwOnError"/> is true and the type is not found. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid characters, such as an embedded tab. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> is an empty string. -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> represents an array type with an invalid size. -or- <paramref name="typeName"/> represents an array of <see cref="TypedReference"/>.</exception>
    /// <exception cref="ArgumentException">An error occurs when <paramref name="typeName"/> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character). -or- <paramref name="throwOnError"/> is true and <paramref name="typeName"/> contains invalid syntax (for example, "MyType[,*,]"). -or- <paramref name="typeName"/> represents a generic type that has a pointer type, a ByRef type, or <see cref="System.Void"/> as one of its type arguments. -or- <paramref name="typeName"/> represents a generic type that has an incorrect number of type arguments. -or- <paramref name="typeName"/> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
    /// <exception cref="FileNotFoundException"><paramref name="throwOnError"/> is true and the assembly or one of its dependencies was not found.</exception>
    /// <exception cref="FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or- <paramref name="typeName"/> contains an invalid assembly name. -or- <paramref name="typeName"/> is a valid assembly name without a type name.</exception>
    /// <exception cref="BadImageFormatException">The assembly or one of its dependencies is not valid. -or- The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the type is not found.</exception>
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type GetType(string typeName, System.Func<AssemblyName, Assembly> assemblyResolver, System.Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
    {
        System.Func<OriginAssemblyName, OriginAssembly?>? AssemblyResolver = (assemblyResolver != DefaultAssemblyResolver) ? (OriginAssemblyName name) => assemblyResolver(new AssemblyName(name)).Origin : null;
        System.Func<OriginAssembly?, string, bool, OriginType?>? TypeResolver = (typeResolver != DefaultTypeResolver) ? (OriginAssembly? assembly, string typeName, bool ignoreCase) => typeResolver(assembly is null ? Assembly.Missing : new Assembly(assembly), typeName, ignoreCase).Origin : null;

        return new Type(OriginType.GetType(typeName, AssemblyResolver, TypeResolver, throwOnError, ignoreCase) ?? throw new NullReferenceException("Type not found."));
    }

    /// <summary>
    /// Gets the types of the objects in the specified array.
    /// </summary>
    /// <param name="args">An array of objects whose types to determine.</param>
    /// <returns>An array of <see cref="Type"/> objects representing the types of the corresponding elements in args.</returns>
    /// <exception cref="TargetInvocationException">The class initializers are invoked and at least one throws an exception.</exception>
    public static Type[] GetTypeArray(object[] args)
    {
        return GetList(OriginType.GetTypeArray(args)).ToArray();
    }

    /// <summary>
    /// Gets the underlying type code of the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The type whose underlying type code to get.</param>
    /// <returns>The code of the underlying type, or <see cref="TypeCode.Empty"/> if type is <see cref="Missing"/>.</returns>
    public static TypeCode GetTypeCode(Type type)
    {
        return OriginType.GetTypeCode(type != Missing ? type.Origin : null);
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID).
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException">Platform not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid)
    {
        return new Type(OriginType.GetTypeFromCLSID(clsid) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID), specifying which exception to throw if an error occurs while loading the type.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="throwOnError">true to throw any exception that occurs. -or- false to throw <see cref="NullReferenceException"/> if an error occurs.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the platform is not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
        return new Type(OriginType.GetTypeFromCLSID(clsid, throwOnError) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID) from the specified server.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException">Platform not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, string server)
    {
        return new Type(OriginType.GetTypeFromCLSID(clsid, server.Length > 0 ? server : null) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="clsid">The CLSID of the type to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <param name="throwOnError">true to throw any exception that occurs. -or- false to throw <see cref="NullReferenceException"/> if an error occurs.</param>
    /// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the platform is not supported.</exception>
    public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
        return new Type(OriginType.GetTypeFromCLSID(clsid, server.Length > 0 ? server : null, throwOnError) ?? throw new NullReferenceException("Platform not supported."));
    }

    /// <summary>
    /// Gets the type referenced by the specified type handle.
    /// </summary>
    /// <param name="handle">The object that refers to the type.</param>
    /// <returns>The type referenced by the specified <see cref="RuntimeTypeHandle"/>, or throws an exception if the <see cref="RuntimeTypeHandle.Value"/> property of handle is null.</returns>
    /// <exception cref="TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
    /// <exception cref="NullReferenceException"><paramref name="handle"/> value is null.</exception>
    public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
    {
        return new Type(OriginType.GetTypeFromHandle(handle));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (ProgID), throwing an exception if an error is encountered while loading the <see cref="Type"/>.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <returns>The type associated with the specified ProgID, if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID)
    {
        return new Type(OriginType.GetTypeFromProgID(progID) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (ProgID), specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="throwOnError">true to throw any exception that occurs. -or- false to throw the <see cref="NullReferenceException"/> exception if an error occurs.</param>
    /// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="COMException">The specified <paramref name="progID"/> is not registered.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, bool throwOnError)
    {
        return new Type(OriginType.GetTypeFromProgID(progID, throwOnError) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (progID) from the specified server, throwing an exception if an error is encountered while loading the <see cref="Type"/>.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is <see cref="string.Empty"/>, this method automatically reverts to the local machine.</param>
    /// <returns>The type associated with the specified ProgID, if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, string server)
    {
        return new Type(OriginType.GetTypeFromProgID(progID, server.Length > 0 ? server : null) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the type associated with the specified program identifier (progID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.
    /// </summary>
    /// <param name="progID">The progID of the <see cref="Type"/> to get.</param>
    /// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine.</param>
    /// <param name="throwOnError">true to throw any exception that occurs. -or- false to throw the <see cref="NullReferenceException"/> exception if an error occurs.</param>
    /// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID"/> is a valid entry in the registry and a type is associated with it; otherwise, throws an exception.</returns>
    /// <exception cref="COMException">The specified <paramref name="progID"/> is not registered.</exception>
    /// <exception cref="NullReferenceException"><paramref name="throwOnError"/> is false, and the platform not supported or <paramref name="progID"/> is not a valid entry in the registry.</exception>
    public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
        return new Type(OriginType.GetTypeFromProgID(progID, server.Length > 0 ? server : null, throwOnError) ?? throw new NullReferenceException($"Platform not supported or {nameof(progID)} is not a valid entry in the registry."));
    }

    /// <summary>
    /// Gets the handle for the <see cref="Type"/> of a specified object.
    /// </summary>
    /// <param name="o">The object for which to get the type handle.</param>
    /// <returns>The handle for the <see cref="Type"/> of the specified <see cref="object"/>.</returns>
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
        return OriginType.GetTypeHandle(o);
    }

    /// <summary>
    /// Invokes the specified member, using the specified binding constraints and matching the specified argument list.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="OriginBinder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns true.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="OriginMethodBase.ContainsGenericParameters"/> property returns true.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object InvokeMember(string name, BindingFlags invokeAttr, OriginBinder binder, object target, object[] args)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args) ?? Void;
    }

    /// <summary>
    /// Invokes the specified member, using the specified binding constraints and matching the specified argument list and culture.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="OriginBinder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="string"/> to a <see cref="double"/>. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns true.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="OriginMethodBase.ContainsGenericParameters"/> property returns true.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object InvokeMember(string name, BindingFlags invokeAttr, OriginBinder binder, object target, object[] args, CultureInfo culture)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args, culture) ?? Void;
    }

    /// <summary>
    /// When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="OriginBinder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the args array. A parameter's associated attributes are stored in the member's signature. The default binder processes this parameter only when calling a COM component.</param>
    /// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="string"/> to a <see cref="double"/>. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired.</param>
    /// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args"/> array are passed.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns true.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="OriginMethodBase.ContainsGenericParameters"/> property returns true.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object InvokeMember(string name, BindingFlags invokeAttr, OriginBinder binder, object target, object[] args, OriginParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args, modifiers, culture, namedParameters) ?? Void;
    }

    /// <summary>
    /// Determines whether an instance of a specified type <paramref name="c"/> can be assigned to a variable of the current type.
    /// </summary>
    /// <param name="c">The type to compare with the current type.</param>
    /// <returns>true if any of the following conditions is true: - <paramref name="c"/> and the current instance represent the same type. - <paramref name="c"/> is derived either directly or indirectly from the current instance. <paramref name="c"/> is derived directly from the current instance if it inherits from the current instance; <paramref name="c"/> is derived indirectly from the current instance if it inherits from a succession of one or more classes that inherit from the current instance. - The current instance is an interface that <paramref name="c"/> implements. - <paramref name="c"/> is a generic type parameter, and the current instance represents one of the constraints of <paramref name="c"/>. - <paramref name="c"/> represents a value type, and the current instance represents <see cref="System.Nullable{c}"/>. false if none of these conditions are true.</returns>
    public bool IsAssignableFrom(Type c)
    {
        return Origin.IsAssignableFrom(c.Origin);
    }

    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified <paramref name="targetType"/>.
    /// </summary>
    /// <param name="targetType">The type to compare with the current type.</param>
    /// <returns>true if any of the following conditions is true: - The current instance and <paramref name="targetType"/> represent the same type. - The current type is derived either directly or indirectly from <paramref name="targetType"/>. The current type is derived directly from <paramref name="targetType"/> if it inherits from <paramref name="targetType"/>; the current type is derived indirectly from <paramref name="targetType"/> if it inherits from a succession of one or more classes that inherit from <paramref name="targetType"/>. - <paramref name="targetType"/> is an interface that the current type implements. - The current type is a generic type parameter, and <paramref name="targetType"/> represents one of the constraints of the current type. - The current type represents a value type, and <paramref name="targetType"/> represents <see cref="System.Nullable{c}"/>. false if none of these conditions are true.</returns>
    public bool IsAssignableTo(Type targetType)
    {
        return Origin.IsAssignableTo(targetType.Origin);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified value exists in the current enumeration type.
    /// </summary>
    /// <param name="value">The value to be tested.</param>
    /// <returns>true if the specified value is a member of the current enumeration type; otherwise, false.</returns>
    /// <exception cref="ArgumentException">The current type is not an enumeration.</exception>
    /// <exception cref="InvalidOperationException"><paramref name="value"/> is of a type that cannot be the underlying type of an enumeration.</exception>
    public bool IsEnumDefined(object value)
    {
        return Origin.IsEnumDefined(value);
    }

    /// <summary>
    /// Determines whether two COM types have the same identity and are eligible for type equivalence.
    /// </summary>
    /// <param name="other">The COM type that is tested for equivalence with the current type.</param>
    /// <returns>true if the COM types are equivalent; otherwise, false. This method also returns false if one type is in an assembly that is loaded for execution, and the other is in an assembly that is loaded into the reflection-only context.</returns>
    public bool IsEquivalentTo(Type other)
    {
        return Origin.IsEquivalentTo(other.Origin);
    }

    /// <summary>
    /// Determines whether the specified object is an instance of the current <see cref="Type"/>.
    /// </summary>
    /// <param name="o">The object to compare with the current type.</param>
    /// <returns>true if the current <see cref="Type"/> is in the inheritance hierarchy of the object represented by <paramref name="o"/>, or if the current <see cref="Type"/> is an interface that <paramref name="o"/> implements. false if neither of these conditions is the case or if the current <see cref="Type"/> is an open generic type (that is, <see cref="Type.ContainsGenericParameters"/> returns true).</returns>
    public bool IsInstanceOfType(object o)
    {
        return Origin.IsInstanceOfType(o);
    }

    /// <summary>
    /// Determines whether the current <see cref="Type"/> derives from the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="c">The type to compare with the current type.</param>
    /// <returns>true if the current <see cref="Type"/> derives from <paramref name="c"/>; otherwise, false. This method also returns false if <paramref name="c"/> and the current <see cref="Type"/> are equal.</returns>
    public bool IsSubclassOf(Type c)
    {
        return Origin.IsSubclassOf(c.Origin);
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object representing a one-dimensional array of the current type, with a lower bound of zero.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns true.</exception>
    public Type MakeArrayType()
    {
        return new Type(Origin.MakeArrayType());
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object representing an array of the current type, with the specified number of dimensions.
    /// </summary>
    /// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
    /// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="rank"/> is invalid. For example, 0 or negative.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns true. -or- <paramref name="rank"/> is greater than 32.</exception>
    public Type MakeArrayType(int rank)
    {
        return new Type(Origin.MakeArrayType(rank));
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents the current type when passed as a ref parameter.
    /// </summary>
    /// <returns>A <see cref="Type"/> object that represents the current type when passed as a ref parameter.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns true.</exception>
    public Type MakeByRefType()
    {
        return new Type(Origin.MakeByRefType());
    }

    /// <summary>
    /// Returns a signature type object that can be passed into the <see cref="Type"/>[] array parameter of a Overload:System.Type.GetMethod method to represent a generic parameter reference.
    /// </summary>
    /// <param name="position">The typed parameter position.</param>
    /// <returns>A signature type object that can be passed into the <see cref="Type"/>[] array parameter of a Overload:System.Type.GetMethod method to represent a generic parameter reference.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/> is negative.</exception>
    public static Type MakeGenericMethodParameter(int position)
    {
        return new Type(OriginType.MakeGenericMethodParameter(position));
    }

    /// <summary>
    /// Creates a generic signature type, which allows third party reimplementations of Reflection to fully support the use of signature types in querying type members.
    /// </summary>
    /// <param name="genericTypeDefinition">The generic type definition.</param>
    /// <param name="typeArguments">An array of type arguments.</param>
    /// <returns>A generic signature type.</returns>
    public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
    {
        return new Type(OriginType.MakeGenericSignatureType(genericTypeDefinition.Origin, GetOriginList(typeArguments).ToArray()));
    }

    /// <summary>
    /// Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="Type"/> object representing the resulting constructed type.
    /// </summary>
    /// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
    /// <returns>A <see cref="Type"/> representing the constructed type formed by substituting the elements of <paramref name="typeArguments"/> for the type parameters of the current generic type.</returns>
    /// <exception cref="InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="IsGenericTypeDefinition"/> returns false.</exception>
    /// <exception cref="ArgumentException">The number of elements in <paramref name="typeArguments"/> is not the same as the number of type parameters in the current generic type definition. -or- Any element of <paramref name="typeArguments"/> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. -or- <paramref name="typeArguments"/> contains an element that is a pointer type (<see cref="IsPointer"/> returns true), a by-ref type (<see cref="IsByRef"/> returns true), or <see cref="System.Void"/>.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    [RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
    public Type MakeGenericType(params Type[] typeArguments)
    {
        return new Type(Origin.MakeGenericType(GetOriginList(typeArguments).ToArray()));
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents a pointer to the current type.
    /// </summary>
    /// <returns>A <see cref="Type"/> object that represents a pointer to the current type.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns true.</exception>
    public Type MakePointerType()
    {
        return new Type(Origin.MakePointerType());
    }

    /// <summary>
    /// Indicates whether two <see cref="Type"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator ==(Type left, Type right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="Type"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>true if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, false.</returns>
    public static bool operator !=(Type left, Type right)
    {
        return left.Origin != right.Origin;
    }

    /// <summary>
    /// Returns a String representing the name of the current Type.
    /// </summary>
    /// <returns>A System.String representing the name of the current System.Type.</returns>
    public override string ToString()
    {
        return Origin.ToString();
    }
}
