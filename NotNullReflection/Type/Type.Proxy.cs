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
using Guid = System.Guid;
using Array = System.Array;
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
using OriginMethodBase = System.Reflection.MethodBase;
using OriginBinder = System.Reflection.Binder;
using OriginParameterModifier = System.Reflection.ParameterModifier;
using OriginModule = System.Reflection.Module;
using OriginConstructorInfo = System.Reflection.ConstructorInfo;
using OriginMemberInfo = System.Reflection.MemberInfo;
using OriginEventInfo = System.Reflection.EventInfo;
using OriginFieldInfo = System.Reflection.FieldInfo;
using OriginDefaultMemberAttribute = System.Reflection.DefaultMemberAttribute;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TypeFilter = System.Reflection.TypeFilter;
using MemberFilter = System.Reflection.MemberFilter;

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
    /// <exception cref="ArgumentException">types is multidimensional. -or- modifiers is multidimensional. -or- types and modifiers do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo GetConstructor(BindingFlags bindingAttr, OriginBinder binder, CallingConventions callConvention, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetConstructor(bindingAttr, binder, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. -or- A null reference (Nothing in Visual Basic), to use the System.Type.DefaultBinder.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. -or- <see cref="OriginType.EmptyTypes"/>.</param>
    /// <param name="modifiers">An array of <see cref="OriginParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>A <see cref="OriginConstructorInfo"/> object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException">types is multidimensional. -or- modifiers is multidimensional. -or- types and modifiers do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public OriginConstructorInfo GetConstructor(BindingFlags bindingAttr, OriginBinder binder, Type[] types, OriginParameterModifier[] modifiers)
    {
        return Origin.GetConstructor(bindingAttr, binder, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements.");
    }

    /// <summary>
    /// Searches for a public instance constructor whose parameters match the types in the specified array.
    /// </summary>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="OriginType.EmptyTypes"/>.</param>
    /// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException">types is multidimensional.</exception>
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

    //
    // Summary:
    //     Returns an array of System.Type objects that represent the type arguments of
    //     a closed generic type or the type parameters of a generic type definition.
    //
    // Returns:
    //     An array of System.Type objects that represent the type arguments of a generic
    //     type. Returns an empty array if the current type is not a generic type.
    //
    // Exceptions:
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must provide
    //     an implementation.
    public virtual Type[] GetGenericArguments()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns an array of System.Type objects that represent the constraints on the
    //     current generic type parameter.
    //
    // Returns:
    //     An array of System.Type objects that represent the constraints on the current
    //     generic type parameter.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     The current System.Type object is not a generic type parameter. That is, the
    //     System.Type.IsGenericParameter property returns false.
    public virtual Type[] GetGenericParameterConstraints()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a System.Type object that represents a generic type definition from which
    //     the current generic type can be constructed.
    //
    // Returns:
    //     A System.Type object representing a generic type from which the current type
    //     can be constructed.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     The current type is not a generic type. That is, System.Type.IsGenericType returns
    //     false.
    //
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must provide
    //     an implementation.
    public virtual Type GetGenericTypeDefinition()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     The hash code for this instance.
    public override int GetHashCode()
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the interface with the specified name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the interface to get. For generic interfaces,
    //     this is the mangled name.
    //
    // Returns:
    //     An object representing the interface with the specified name, implemented or
    //     inherited by the current System.Type, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     The current System.Type represents a type that implements the same generic interface
    //     with different type arguments.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public Type? GetInterface(string name)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the specified interface, specifying
    //     whether to do a case-insensitive search for the interface name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the interface to get. For generic interfaces,
    //     this is the mangled name.
    //
    //   ignoreCase:
    //     true to ignore the case of that part of name that specifies the simple interface
    //     name (the part that specifies the namespace must be correctly cased). -or- false
    //     to perform a case-sensitive search for all parts of name.
    //
    // Returns:
    //     An object representing the interface with the specified name, implemented or
    //     inherited by the current System.Type, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     The current System.Type represents a type that implements the same generic interface
    //     with different type arguments.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public abstract Type? GetInterface(string name, bool ignoreCase);

    //
    // Summary:
    //     Returns an interface mapping for the specified interface type.
    //
    // Parameters:
    //   interfaceType:
    //     The interface type to retrieve a mapping for.
    //
    // Returns:
    //     An object that represents the interface mapping for interfaceType.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     interfaceType is not implemented by the current type. -or- The interfaceType
    //     argument does not refer to an interface. -or- The current instance or interfaceType
    //     argument is an open generic type; that is, the System.Type.ContainsGenericParameters
    //     property returns true. -or- interfaceType is a generic interface, and the current
    //     type is an array type.
    //
    //   T:System.ArgumentNullException:
    //     interfaceType is null.
    //
    //   T:System.InvalidOperationException:
    //     The current System.Type represents a generic type parameter; that is, System.Type.IsGenericParameter
    //     is true.
    //
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must provide
    //     an implementation.
    public virtual InterfaceMapping GetInterfaceMap([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] Type interfaceType)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, gets all the interfaces implemented or inherited
    //     by the current System.Type.
    //
    // Returns:
    //     An array of System.Type objects representing all the interfaces implemented or
    //     inherited by the current System.Type. -or- An empty array of type System.Type,
    //     if no interfaces are implemented or inherited by the current System.Type.
    //
    // Exceptions:
    //   T:System.Reflection.TargetInvocationException:
    //     A static initializer is invoked and throws an exception.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public abstract Type[] GetInterfaces();

    //
    // Summary:
    //     Searches for the public members with the specified name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public members to get.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing the public members
    //     with the specified name, if found; otherwise, an empty array.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
    public MemberInfo[] GetMember(string name)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified members, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the members to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return an empty array.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing the public members
    //     with the specified name, if found; otherwise, an empty array.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified members of the specified member type, using the specified
    //     binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the members to get.
    //
    //   type:
    //     The value to search for.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return an empty array.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing the public members
    //     with the specified name, if found; otherwise, an empty array.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    //
    //   T:System.NotSupportedException:
    //     A derived class must provide an implementation.
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the System.Reflection.MemberInfo on the current System.Type that
    //     matches the specified System.Reflection.MemberInfo.
    //
    // Parameters:
    //   member:
    //     The System.Reflection.MemberInfo to find on the current System.Type.
    //
    // Returns:
    //     An object representing the member on the current System.Type that matches the
    //     specified member.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     member is null.
    //
    //   T:System.ArgumentException:
    //     member does not match a member on the current System.Type.
    public virtual MemberInfo GetMemberWithSameMetadataDefinitionAs(MemberInfo member)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns all the public members of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing all the public
    //     members of the current System.Type. -or- An empty array of type System.Reflection.MemberInfo,
    //     if the current System.Type does not have public members.
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
    public MemberInfo[] GetMembers()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the members defined for the
    //     current System.Type, using the specified binding constraints.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return an empty array.
    //
    // Returns:
    //     An array of System.Reflection.MemberInfo objects representing all members defined
    //     for the current System.Type that match the specified binding constraints. -or-
    //     An empty array if no members are defined for the current System.Type, or if none
    //     of the defined members match the binding constraints.
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
    public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

    //
    // Summary:
    //     Searches for the public method with the specified name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    // Returns:
    //     An object that represents the public method with the specified name, if found;
    //     otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name.
    //
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo? GetMethod(string name)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method whose parameters match the specified generic
    //     parameter count, argument types and modifiers, using the specified binding constraints
    //     and the specified calling convention.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   genericParameterCount:
    //     The number of generic type parameters of the method.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   callConvention:
    //     The object that specifies the set of rules to use regarding the order and layout
    //     of arguments, how the return value is passed, what registers are used for arguments,
    //     and how the stack is cleaned up.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified generic parameter
    //     count, argument types, modifiers, binding constraints and calling convention,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in the types array
    //     is null.
    //
    //   T:System.ArgumentException:
    //     genericParameterCount is negative.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method whose parameters match the specified generic
    //     parameter count, argument types and modifiers, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   genericParameterCount:
    //     The number of generic type parameters of the method.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified generic parameter
    //     count, argument types, modifiers and binding constraints, if found; otherwise,
    //     null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in the types array
    //     is null.
    //
    //   T:System.ArgumentException:
    //     genericParameterCount is negative.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder? binder, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public method whose parameters match the specified
    //     generic parameter count and argument types.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   genericParameterCount:
    //     The number of generic type parameters of the method.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    // Returns:
    //     An object representing the public method whose parameters match the specified
    //     generic parameter count and argument types, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in the types array
    //     is null.
    //
    //   T:System.ArgumentException:
    //     genericParameterCount is negative.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo? GetMethod(string name, int genericParameterCount, Type[] types)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public method whose parameters match the specified
    //     generic parameter count, argument types and modifiers.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   genericParameterCount:
    //     The number of generic type parameters of the method.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the public method that matches the specified generic parameter
    //     count, argument types and modifiers, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in the types array
    //     is null.
    //
    //   T:System.ArgumentException:
    //     genericParameterCount is negative.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo? GetMethod(string name, int genericParameterCount, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    // Returns:
    //     An object representing the method that matches the specified requirements, if
    //     found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method whose parameters match the specified argument
    //     types, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- Default to return null.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    // Returns:
    //     An object representing the method that matches the specified requirements, if
    //     found; otherwise, null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr, Type[] types)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method whose parameters match the specified argument
    //     types and modifiers, using the specified binding constraints and the specified
    //     calling convention.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   callConvention:
    //     The object that specifies the set of rules to use regarding the order and layout
    //     of arguments, how the return value is passed, what registers are used for arguments,
    //     and how the stack is cleaned up.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified requirements, if
    //     found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified method whose parameters match the specified argument
    //     types and modifiers, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified requirements, if
    //     found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo? GetMethod(string name, BindingFlags bindingAttr, Binder? binder, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public method whose parameters match the specified
    //     argument types.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    // Returns:
    //     An object representing the public method whose parameters match the specified
    //     argument types, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and specified parameters.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo? GetMethod(string name, Type[] types)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public method whose parameters match the specified
    //     argument types and modifiers.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public method to get.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of System.Type objects
    //     (as provided by the System.Type.EmptyTypes field) to get a method that takes
    //     no parameters.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. To be only used
    //     when calling through COM interop, and only parameters that are passed by reference
    //     are handled. The default binder does not process this parameter.
    //
    // Returns:
    //     An object representing the public method that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and specified parameters.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo? GetMethod(string name, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the specified method whose parameters
    //     match the specified generic parameter count, argument types and modifiers, using
    //     the specified binding constraints and the specified calling convention.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   genericParameterCount:
    //     The number of generic type parameters of the method.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   callConvention:
    //     The object that specifies the set of rules to use regarding the order and layout
    //     of arguments, how the return value is passed, what registers are used for arguments,
    //     and what process cleans up the stack.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a method that takes no parameters.
    //     -or- null. If types is null, arguments are not matched.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. The default binder
    //     does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified generic parameter
    //     count, argument types, modifiers, binding constraints and calling convention,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.NotSupportedException:
    //     The method needs to be overriden and called in a derived class.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    protected virtual MethodInfo? GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[]? types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the specified method whose parameters
    //     match the specified argument types and modifiers, using the specified binding
    //     constraints and the specified calling convention.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the method to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   callConvention:
    //     The object that specifies the set of rules to use regarding the order and layout
    //     of arguments, how the return value is passed, what registers are used for arguments,
    //     and what process cleans up the stack.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the method to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a method that takes no parameters.
    //     -or- null. If types is null, arguments are not matched.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. The default binder
    //     does not process this parameter.
    //
    // Returns:
    //     An object representing the method that matches the specified requirements, if
    //     found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional. -or- types and
    //     modifiers do not have the same length.
    //
    //   T:System.NotSupportedException:
    //     The current type is a System.Reflection.Emit.TypeBuilder or System.Reflection.Emit.GenericTypeParameterBuilder.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    protected abstract MethodInfo? GetMethodImpl(string name, BindingFlags bindingAttr, Binder? binder, CallingConventions callConvention, Type[]? types, ParameterModifier[]? modifiers);

    //
    // Summary:
    //     Returns all the public methods of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects representing all the public
    //     methods defined for the current System.Type. -or- An empty array of type System.Reflection.MethodInfo,
    //     if no public methods are defined for the current System.Type.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo[] GetMethods()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the methods defined for the
    //     current System.Type, using the specified binding constraints.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return an empty array.
    //
    // Returns:
    //     An array of System.Reflection.MethodInfo objects representing all methods defined
    //     for the current System.Type that match the specified binding constraints. -or-
    //     An empty array of type System.Reflection.MethodInfo, if no methods are defined
    //     for the current System.Type, or if none of the defined methods match the binding
    //     constraints.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

    //
    // Summary:
    //     Searches for the public nested type with the specified name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the nested type to get.
    //
    // Returns:
    //     An object representing the public nested type with the specified name, if found;
    //     otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
    public Type? GetNestedType(string name)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the specified nested type, using
    //     the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the nested type to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    // Returns:
    //     An object representing the nested type that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
    public abstract Type? GetNestedType(string name, BindingFlags bindingAttr);

    //
    // Summary:
    //     Returns the public types nested in the current System.Type.
    //
    // Returns:
    //     An array of System.Type objects representing the public types nested in the current
    //     System.Type (the search is not recursive), or an empty array of type System.Type
    //     if no public types are nested in the current System.Type.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
    public Type[] GetNestedTypes()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the types nested in the current
    //     System.Type, using the specified binding constraints.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    // Returns:
    //     An array of System.Type objects representing all the types nested in the current
    //     System.Type that match the specified binding constraints (the search is not recursive),
    //     or an empty array of type System.Type, if no nested types are found that match
    //     the binding constraints.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
    public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

    //
    // Summary:
    //     Returns all the public properties of the current System.Type.
    //
    // Returns:
    //     An array of System.Reflection.PropertyInfo objects representing all public properties
    //     of the current System.Type. -or- An empty array of type System.Reflection.PropertyInfo,
    //     if the current System.Type does not have public properties.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo[] GetProperties()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the properties of the current
    //     System.Type, using the specified binding constraints.
    //
    // Parameters:
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return an empty array.
    //
    // Returns:
    //     An array of objects representing all properties of the current System.Type that
    //     match the specified binding constraints. -or- An empty array of type System.Reflection.PropertyInfo,
    //     if the current System.Type does not have properties, or if none of the properties
    //     match the binding constraints.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    //
    // Summary:
    //     Searches for the public property with the specified name.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public property to get.
    //
    // Returns:
    //     An object representing the public property with the specified name, if found;
    //     otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name.
    //
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo? GetProperty(string name)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified property, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the property to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    // Returns:
    //     An object representing the property that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified property whose parameters match the specified argument
    //     types and modifiers, using the specified binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the property to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   returnType:
    //     The return type of the property.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the indexed property to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a property that is not indexed.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. The default binder
    //     does not process this parameter.
    //
    // Returns:
    //     An object representing the property that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional. -or- types and
    //     modifiers do not have the same length.
    //
    //   T:System.NullReferenceException:
    //     An element of types is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    public PropertyInfo? GetProperty(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the public property with the specified name and return type.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public property to get.
    //
    //   returnType:
    //     The return type of the property.
    //
    // Returns:
    //     An object representing the public property with the specified name, if found;
    //     otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name.
    //
    //   T:System.ArgumentNullException:
    //     name is null, or returnType is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo? GetProperty(string name, Type? returnType)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public property whose parameters match the specified
    //     argument types.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public property to get.
    //
    //   returnType:
    //     The return type of the property.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the indexed property to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a property that is not indexed.
    //
    // Returns:
    //     An object representing the public property whose parameters match the specified
    //     argument types, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     argument types.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional.
    //
    //   T:System.NullReferenceException:
    //     An element of types is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo? GetProperty(string name, Type? returnType, Type[] types)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public property whose parameters match the specified
    //     argument types and modifiers.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public property to get.
    //
    //   returnType:
    //     The return type of the property.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the indexed property to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a property that is not indexed.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. The default binder
    //     does not process this parameter.
    //
    // Returns:
    //     An object representing the public property that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     argument types and modifiers.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional. -or- types and
    //     modifiers do not have the same length.
    //
    //   T:System.NullReferenceException:
    //     An element of types is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo? GetProperty(string name, Type? returnType, Type[] types, ParameterModifier[]? modifiers)
    {
        throw null;
    }

    //
    // Summary:
    //     Searches for the specified public property whose parameters match the specified
    //     argument types.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the public property to get.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the indexed property to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a property that is not indexed.
    //
    // Returns:
    //     An object representing the public property whose parameters match the specified
    //     argument types, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     argument types.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional.
    //
    //   T:System.NullReferenceException:
    //     An element of types is null.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public PropertyInfo? GetProperty(string name, Type[] types)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, searches for the specified property whose
    //     parameters match the specified argument types and modifiers, using the specified
    //     binding constraints.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the property to get.
    //
    //   bindingAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. -or- System.Reflection.BindingFlags.Default to return null.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded member, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder.
    //
    //   returnType:
    //     The return type of the property.
    //
    //   types:
    //     An array of System.Type objects representing the number, order, and type of the
    //     parameters for the indexed property to get. -or- An empty array of the type System.Type
    //     (that is, Type[] types = new Type[0]) to get a property that is not indexed.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the types array. The default binder
    //     does not process this parameter.
    //
    // Returns:
    //     An object representing the property that matches the specified requirements,
    //     if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one property is found with the specified name and matching the specified
    //     binding constraints.
    //
    //   T:System.ArgumentNullException:
    //     name is null. -or- types is null. -or- One of the elements in types is null.
    //
    //   T:System.ArgumentException:
    //     types is multidimensional. -or- modifiers is multidimensional. -or- types and
    //     modifiers do not have the same length.
    //
    //   T:System.NotSupportedException:
    //     The current type is a System.Reflection.Emit.TypeBuilder, System.Reflection.Emit.EnumBuilder,
    //     or System.Reflection.Emit.GenericTypeParameterBuilder.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
    protected abstract PropertyInfo? GetPropertyImpl(string name, BindingFlags bindingAttr, Binder? binder, Type? returnType, Type[]? types, ParameterModifier[]? modifiers);

    //
    // Summary:
    //     Gets the current System.Type.
    //
    // Returns:
    //     The current System.Type.
    //
    // Exceptions:
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    public new Type GetType()
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the System.Type with the specified name, performing a case-sensitive search.
    //
    // Parameters:
    //   typeName:
    //     The assembly-qualified name of the type to get. See System.Type.AssemblyQualifiedName.
    //     If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     it is sufficient to supply the type name qualified by its namespace.
    //
    // Returns:
    //     The type with the specified name, if found; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.ArgumentException:
    //     typeName represents a generic type that has a pointer type, a ByRef type, or
    //     System.Void as one of its type arguments. -or- typeName represents a generic
    //     type that has an incorrect number of type arguments. -or- typeName represents
    //     a generic type, and one of its type arguments does not satisfy the constraints
    //     for the corresponding type parameter.
    //
    //   T:System.TypeLoadException:
    //     typeName represents an array of System.TypedReference.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded. Note:
    //     In .NET for Windows Store apps or the Portable Class Library, catch the base
    //     class exception, System.IO.IOException, instead.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- Version 2.0 or later
    //     of the common language runtime is currently loaded, and the assembly was compiled
    //     with a later version.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the System.Type with the specified name, performing a case-sensitive search
    //     and specifying whether to throw an exception if the type is not found.
    //
    // Parameters:
    //   typeName:
    //     The assembly-qualified name of the type to get. See System.Type.AssemblyQualifiedName.
    //     If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     it is sufficient to supply the type name qualified by its namespace.
    //
    //   throwOnError:
    //     true to throw an exception if the type cannot be found; false to return null.
    //     Specifying false also suppresses some other exception conditions, but not all
    //     of them. See the Exceptions section.
    //
    // Returns:
    //     The type with the specified name. If the type is not found, the throwOnError
    //     parameter specifies whether null is returned or an exception is thrown. In some
    //     cases, an exception is thrown regardless of the value of throwOnError. See the
    //     Exceptions section.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.TypeLoadException:
    //     throwOnError is true and the type is not found. -or- throwOnError is true and
    //     typeName contains invalid characters, such as an embedded tab. -or- throwOnError
    //     is true and typeName is an empty string. -or- throwOnError is true and typeName
    //     represents an array type with an invalid size. -or- typeName represents an array
    //     of System.TypedReference.
    //
    //   T:System.ArgumentException:
    //     throwOnError is true and typeName contains invalid syntax. For example, "MyType[,*,]".
    //     -or- typeName represents a generic type that has a pointer type, a ByRef type,
    //     or System.Void as one of its type arguments. -or- typeName represents a generic
    //     type that has an incorrect number of type arguments. -or- typeName represents
    //     a generic type, and one of its type arguments does not satisfy the constraints
    //     for the corresponding type parameter.
    //
    //   T:System.IO.FileNotFoundException:
    //     throwOnError is true and the assembly or one of its dependencies was not found.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded. Note:
    //     In .NET for Windows Store apps or the Portable Class Library, catch the base
    //     class exception, System.IO.IOException, instead.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- Version 2.0 or later
    //     of the common language runtime is currently loaded, and the assembly was compiled
    //     with a later version.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the System.Type with the specified name, specifying whether to throw an
    //     exception if the type is not found and whether to perform a case-sensitive search.
    //
    // Parameters:
    //   typeName:
    //     The assembly-qualified name of the type to get. See System.Type.AssemblyQualifiedName.
    //     If the type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     it is sufficient to supply the type name qualified by its namespace.
    //
    //   throwOnError:
    //     true to throw an exception if the type cannot be found; false to return null.
    //     Specifying false also suppresses some other exception conditions, but not all
    //     of them. See the Exceptions section.
    //
    //   ignoreCase:
    //     true to perform a case-insensitive search for typeName, false to perform a case-sensitive
    //     search for typeName.
    //
    // Returns:
    //     The type with the specified name. If the type is not found, the throwOnError
    //     parameter specifies whether null is returned or an exception is thrown. In some
    //     cases, an exception is thrown regardless of the value of throwOnError. See the
    //     Exceptions section.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.TypeLoadException:
    //     throwOnError is true and the type is not found. -or- throwOnError is true and
    //     typeName contains invalid characters, such as an embedded tab. -or- throwOnError
    //     is true and typeName is an empty string. -or- throwOnError is true and typeName
    //     represents an array type with an invalid size. -or- typeName represents an array
    //     of System.TypedReference.
    //
    //   T:System.ArgumentException:
    //     throwOnError is true and typeName contains invalid syntax. For example, "MyType[,*,]".
    //     -or- typeName represents a generic type that has a pointer type, a ByRef type,
    //     or System.Void as one of its type arguments. -or- typeName represents a generic
    //     type that has an incorrect number of type arguments. -or- typeName represents
    //     a generic type, and one of its type arguments does not satisfy the constraints
    //     for the corresponding type parameter.
    //
    //   T:System.IO.FileNotFoundException:
    //     throwOnError is true and the assembly or one of its dependencies was not found.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- Version 2.0 or later
    //     of the common language runtime is currently loaded, and the assembly was compiled
    //     with a later version.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type with the specified name, optionally providing custom methods to
    //     resolve the assembly and the type.
    //
    // Parameters:
    //   typeName:
    //     The name of the type to get. If the typeResolver parameter is provided, the type
    //     name can be any string that typeResolver is capable of resolving. If the assemblyResolver
    //     parameter is provided or if standard type resolution is used, typeName must be
    //     an assembly-qualified name (see System.Type.AssemblyQualifiedName), unless the
    //     type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     in which case it is sufficient to supply the type name qualified by its namespace.
    //
    //   assemblyResolver:
    //     A method that locates and returns the assembly that is specified in typeName.
    //     The assembly name is passed to assemblyResolver as an System.Reflection.AssemblyName
    //     object. If typeName does not contain the name of an assembly, assemblyResolver
    //     is not called. If assemblyResolver is not supplied, standard assembly resolution
    //     is performed. Caution Do not pass methods from unknown or untrusted callers.
    //     Doing so could result in elevation of privilege for malicious code. Use only
    //     methods that you provide or that you are familiar with.
    //
    //   typeResolver:
    //     A method that locates and returns the type that is specified by typeName from
    //     the assembly that is returned by assemblyResolver or by standard assembly resolution.
    //     If no assembly is provided, the typeResolver method can provide one. The method
    //     also takes a parameter that specifies whether to perform a case-insensitive search;
    //     false is passed to that parameter. Caution Do not pass methods from unknown or
    //     untrusted callers.
    //
    // Returns:
    //     The type with the specified name, or null if the type is not found.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.ArgumentException:
    //     An error occurs when typeName is parsed into a type name and an assembly name
    //     (for example, when the simple type name includes an unescaped special character).
    //     -or- typeName represents a generic type that has a pointer type, a ByRef type,
    //     or System.Void as one of its type arguments. -or- typeName represents a generic
    //     type that has an incorrect number of type arguments. -or- typeName represents
    //     a generic type, and one of its type arguments does not satisfy the constraints
    //     for the corresponding type parameter.
    //
    //   T:System.TypeLoadException:
    //     typeName represents an array of System.TypedReference.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded. -or-
    //     typeName contains an invalid assembly name. -or- typeName is a valid assembly
    //     name without a type name.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- The assembly was compiled
    //     with a later version of the common language runtime than the version that is
    //     currently loaded.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName, Func<AssemblyName, Assembly?>? assemblyResolver, Func<Assembly?, string, bool, Type?>? typeResolver)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type with the specified name, specifying whether to throw an exception
    //     if the type is not found, and optionally providing custom methods to resolve
    //     the assembly and the type.
    //
    // Parameters:
    //   typeName:
    //     The name of the type to get. If the typeResolver parameter is provided, the type
    //     name can be any string that typeResolver is capable of resolving. If the assemblyResolver
    //     parameter is provided or if standard type resolution is used, typeName must be
    //     an assembly-qualified name (see System.Type.AssemblyQualifiedName), unless the
    //     type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     in which case it is sufficient to supply the type name qualified by its namespace.
    //
    //   assemblyResolver:
    //     A method that locates and returns the assembly that is specified in typeName.
    //     The assembly name is passed to assemblyResolver as an System.Reflection.AssemblyName
    //     object. If typeName does not contain the name of an assembly, assemblyResolver
    //     is not called. If assemblyResolver is not supplied, standard assembly resolution
    //     is performed. Caution Do not pass methods from unknown or untrusted callers.
    //     Doing so could result in elevation of privilege for malicious code. Use only
    //     methods that you provide or that you are familiar with.
    //
    //   typeResolver:
    //     A method that locates and returns the type that is specified by typeName from
    //     the assembly that is returned by assemblyResolver or by standard assembly resolution.
    //     If no assembly is provided, the method can provide one. The method also takes
    //     a parameter that specifies whether to perform a case-insensitive search; false
    //     is passed to that parameter. Caution Do not pass methods from unknown or untrusted
    //     callers.
    //
    //   throwOnError:
    //     true to throw an exception if the type cannot be found; false to return null.
    //     Specifying false also suppresses some other exception conditions, but not all
    //     of them. See the Exceptions section.
    //
    // Returns:
    //     The type with the specified name. If the type is not found, the throwOnError
    //     parameter specifies whether null is returned or an exception is thrown. In some
    //     cases, an exception is thrown regardless of the value of throwOnError. See the
    //     Exceptions section.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.TypeLoadException:
    //     throwOnError is true and the type is not found. -or- throwOnError is true and
    //     typeName contains invalid characters, such as an embedded tab. -or- throwOnError
    //     is true and typeName is an empty string. -or- throwOnError is true and typeName
    //     represents an array type with an invalid size. -or- typeName represents an array
    //     of System.TypedReference.
    //
    //   T:System.ArgumentException:
    //     An error occurs when typeName is parsed into a type name and an assembly name
    //     (for example, when the simple type name includes an unescaped special character).
    //     -or- throwOnError is true and typeName contains invalid syntax (for example,
    //     "MyType[,*,]"). -or- typeName represents a generic type that has a pointer type,
    //     a ByRef type, or System.Void as one of its type arguments. -or- typeName represents
    //     a generic type that has an incorrect number of type arguments. -or- typeName
    //     represents a generic type, and one of its type arguments does not satisfy the
    //     constraints for the corresponding type parameter.
    //
    //   T:System.IO.FileNotFoundException:
    //     throwOnError is true and the assembly or one of its dependencies was not found.
    //     -or- typeName contains an invalid assembly name. -or- typeName is a valid assembly
    //     name without a type name.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- The assembly was compiled
    //     with a later version of the common language runtime than the version that is
    //     currently loaded.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName, Func<AssemblyName, Assembly?>? assemblyResolver, Func<Assembly?, string, bool, Type?>? typeResolver, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type with the specified name, specifying whether to perform a case-sensitive
    //     search and whether to throw an exception if the type is not found, and optionally
    //     providing custom methods to resolve the assembly and the type.
    //
    // Parameters:
    //   typeName:
    //     The name of the type to get. If the typeResolver parameter is provided, the type
    //     name can be any string that typeResolver is capable of resolving. If the assemblyResolver
    //     parameter is provided or if standard type resolution is used, typeName must be
    //     an assembly-qualified name (see System.Type.AssemblyQualifiedName), unless the
    //     type is in the currently executing assembly or in mscorlib.dll/System.Private.CoreLib.dll,
    //     in which case it is sufficient to supply the type name qualified by its namespace.
    //
    //   assemblyResolver:
    //     A method that locates and returns the assembly that is specified in typeName.
    //     The assembly name is passed to assemblyResolver as an System.Reflection.AssemblyName
    //     object. If typeName does not contain the name of an assembly, assemblyResolver
    //     is not called. If assemblyResolver is not supplied, standard assembly resolution
    //     is performed. Caution Do not pass methods from unknown or untrusted callers.
    //     Doing so could result in elevation of privilege for malicious code. Use only
    //     methods that you provide or that you are familiar with.
    //
    //   typeResolver:
    //     A method that locates and returns the type that is specified by typeName from
    //     the assembly that is returned by assemblyResolver or by standard assembly resolution.
    //     If no assembly is provided, the method can provide one. The method also takes
    //     a parameter that specifies whether to perform a case-insensitive search; the
    //     value of ignoreCase is passed to that parameter. Caution Do not pass methods
    //     from unknown or untrusted callers.
    //
    //   throwOnError:
    //     true to throw an exception if the type cannot be found; false to return null.
    //     Specifying false also suppresses some other exception conditions, but not all
    //     of them. See the Exceptions section.
    //
    //   ignoreCase:
    //     true to perform a case-insensitive search for typeName, false to perform a case-sensitive
    //     search for typeName.
    //
    // Returns:
    //     The type with the specified name. If the type is not found, the throwOnError
    //     parameter specifies whether null is returned or an exception is thrown. In some
    //     cases, an exception is thrown regardless of the value of throwOnError. See the
    //     Exceptions section.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.TypeLoadException:
    //     throwOnError is true and the type is not found. -or- throwOnError is true and
    //     typeName contains invalid characters, such as an embedded tab. -or- throwOnError
    //     is true and typeName is an empty string. -or- throwOnError is true and typeName
    //     represents an array type with an invalid size. -or- typeName represents an array
    //     of System.TypedReference.
    //
    //   T:System.ArgumentException:
    //     An error occurs when typeName is parsed into a type name and an assembly name
    //     (for example, when the simple type name includes an unescaped special character).
    //     -or- throwOnError is true and typeName contains invalid syntax (for example,
    //     "MyType[,*,]"). -or- typeName represents a generic type that has a pointer type,
    //     a ByRef type, or System.Void as one of its type arguments. -or- typeName represents
    //     a generic type that has an incorrect number of type arguments. -or- typeName
    //     represents a generic type, and one of its type arguments does not satisfy the
    //     constraints for the corresponding type parameter.
    //
    //   T:System.IO.FileNotFoundException:
    //     throwOnError is true and the assembly or one of its dependencies was not found.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded. -or-
    //     typeName contains an invalid assembly name. -or- typeName is a valid assembly
    //     name without a type name.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- The assembly was compiled
    //     with a later version of the common language runtime than the version that is
    //     currently loaded.
    [RequiresUnreferencedCode("The type might be removed")]
    public static Type? GetType(string typeName, Func<AssemblyName, Assembly?>? assemblyResolver, Func<Assembly?, string, bool, Type?>? typeResolver, bool throwOnError, bool ignoreCase)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the types of the objects in the specified array.
    //
    // Parameters:
    //   args:
    //     An array of objects whose types to determine.
    //
    // Returns:
    //     An array of System.Type objects representing the types of the corresponding elements
    //     in args.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     args is null. -or- One or more of the elements in args is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     The class initializers are invoked and at least one throws an exception.
    public static Type[] GetTypeArray(object[] args)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the underlying type code of the specified System.Type.
    //
    // Parameters:
    //   type:
    //     The type whose underlying type code to get.
    //
    // Returns:
    //     The code of the underlying type, or System.TypeCode.Empty if type is null.
    public static TypeCode GetTypeCode(Type? type)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns the underlying type code of this System.Type instance.
    //
    // Returns:
    //     The type code of the underlying type.
    protected virtual TypeCode GetTypeCodeImpl()
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified class identifier (CLSID).
    //
    // Parameters:
    //   clsid:
    //     The CLSID of the type to get.
    //
    // Returns:
    //     System.__ComObject regardless of whether the CLSID is valid.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromCLSID(Guid clsid)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified class identifier (CLSID), specifying
    //     whether to throw an exception if an error occurs while loading the type.
    //
    // Parameters:
    //   clsid:
    //     The CLSID of the type to get.
    //
    //   throwOnError:
    //     true to throw any exception that occurs. -or- false to ignore any exception that
    //     occurs.
    //
    // Returns:
    //     System.__ComObject regardless of whether the CLSID is valid.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified class identifier (CLSID) from the
    //     specified server.
    //
    // Parameters:
    //   clsid:
    //     The CLSID of the type to get.
    //
    //   server:
    //     The server from which to load the type. If the server name is null, this method
    //     automatically reverts to the local machine.
    //
    // Returns:
    //     System.__ComObject regardless of whether the CLSID is valid.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromCLSID(Guid clsid, string? server)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified class identifier (CLSID) from the
    //     specified server, specifying whether to throw an exception if an error occurs
    //     while loading the type.
    //
    // Parameters:
    //   clsid:
    //     The CLSID of the type to get.
    //
    //   server:
    //     The server from which to load the type. If the server name is null, this method
    //     automatically reverts to the local machine.
    //
    //   throwOnError:
    //     true to throw any exception that occurs. -or- false to ignore any exception that
    //     occurs.
    //
    // Returns:
    //     System.__ComObject regardless of whether the CLSID is valid.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromCLSID(Guid clsid, string? server, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type referenced by the specified type handle.
    //
    // Parameters:
    //   handle:
    //     The object that refers to the type.
    //
    // Returns:
    //     The type referenced by the specified System.RuntimeTypeHandle, or null if the
    //     System.RuntimeTypeHandle.Value property of handle is null.
    //
    // Exceptions:
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified program identifier (ProgID), returning
    //     null if an error is encountered while loading the System.Type.
    //
    // Parameters:
    //   progID:
    //     The ProgID of the type to get.
    //
    // Returns:
    //     The type associated with the specified ProgID, if progID is a valid entry in
    //     the registry and a type is associated with it; otherwise, null.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     progID is null.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromProgID(string progID)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified program identifier (ProgID), specifying
    //     whether to throw an exception if an error occurs while loading the type.
    //
    // Parameters:
    //   progID:
    //     The ProgID of the type to get.
    //
    //   throwOnError:
    //     true to throw any exception that occurs. -or- false to ignore any exception that
    //     occurs.
    //
    // Returns:
    //     The type associated with the specified program identifier (ProgID), if progID
    //     is a valid entry in the registry and a type is associated with it; otherwise,
    //     null.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     progID is null.
    //
    //   T:System.Runtime.InteropServices.COMException:
    //     The specified ProgID is not registered.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromProgID(string progID, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified program identifier (progID) from
    //     the specified server, returning null if an error is encountered while loading
    //     the type.
    //
    // Parameters:
    //   progID:
    //     The progID of the type to get.
    //
    //   server:
    //     The server from which to load the type. If the server name is null, this method
    //     automatically reverts to the local machine.
    //
    // Returns:
    //     The type associated with the specified program identifier (progID), if progID
    //     is a valid entry in the registry and a type is associated with it; otherwise,
    //     null.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     prodID is null.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromProgID(string progID, string? server)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the type associated with the specified program identifier (progID) from
    //     the specified server, specifying whether to throw an exception if an error occurs
    //     while loading the type.
    //
    // Parameters:
    //   progID:
    //     The progID of the System.Type to get.
    //
    //   server:
    //     The server from which to load the type. If the server name is null, this method
    //     automatically reverts to the local machine.
    //
    //   throwOnError:
    //     true to throw any exception that occurs. -or- false to ignore any exception that
    //     occurs.
    //
    // Returns:
    //     The type associated with the specified program identifier (progID), if progID
    //     is a valid entry in the registry and a type is associated with it; otherwise,
    //     null.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     progID is null.
    //
    //   T:System.Runtime.InteropServices.COMException:
    //     The specified progID is not registered.
    [SupportedOSPlatform("windows")]
    public static Type? GetTypeFromProgID(string progID, string? server, bool throwOnError)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the handle for the System.Type of a specified object.
    //
    // Parameters:
    //   o:
    //     The object for which to get the type handle.
    //
    // Returns:
    //     The handle for the System.Type of the specified System.Object.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     o is null.
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.HasElementType
    //     property and determines whether the current System.Type encompasses or refers
    //     to another type; that is, whether the current System.Type is an array, a pointer,
    //     or is passed by reference.
    //
    // Returns:
    //     true if the System.Type is an array, a pointer, or is passed by reference; otherwise,
    //     false.
    protected abstract bool HasElementTypeImpl();

    //
    // Summary:
    //     Invokes the specified member, using the specified binding constraints and matching
    //     the specified argument list.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the constructor, method, property, or field
    //     member to invoke. -or- An empty string ("") to invoke the default member. -or-
    //     For IDispatch members, a string representing the DispID, for example "[DispID=3]".
    //
    //   invokeAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. The access can be one of the BindingFlags such as Public, NonPublic,
    //     Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified.
    //     If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance
    //     | BindingFlags.Static are used.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder. Note that explicitly defining a System.Reflection.Binder
    //     object may be required for successfully invoking method overloads with variable
    //     arguments.
    //
    //   target:
    //     The object on which to invoke the specified member.
    //
    //   args:
    //     An array containing the arguments to pass to the member to invoke.
    //
    // Returns:
    //     An object representing the return value of the invoked member.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     invokeAttr does not contain CreateInstance and name is null.
    //
    //   T:System.ArgumentException:
    //     invokeAttr is not a valid System.Reflection.BindingFlags attribute. -or- invokeAttr
    //     does not contain one of the following binding flags: InvokeMethod, CreateInstance,
    //     GetField, SetField, GetProperty, or SetProperty. -or- invokeAttr contains CreateInstance
    //     combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.
    //     -or- invokeAttr contains both GetField and SetField. -or- invokeAttr contains
    //     both GetProperty and SetProperty. -or- invokeAttr contains InvokeMethod combined
    //     with SetField or SetProperty. -or- invokeAttr contains SetField and args has
    //     more than one element. -or- This method is called on a COM object and one of
    //     the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty,
    //     BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.
    //     -or- One of the named parameter arrays contains a string that is null.
    //
    //   T:System.MethodAccessException:
    //     The specified member is a class initializer.
    //
    //   T:System.MissingFieldException:
    //     The field or property cannot be found.
    //
    //   T:System.MissingMethodException:
    //     No method can be found that matches the arguments in args. -or- The current System.Type
    //     object represents a type that contains open type parameters, that is, System.Type.ContainsGenericParameters
    //     returns true.
    //
    //   T:System.Reflection.TargetException:
    //     The specified member cannot be invoked on target.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method matches the binding criteria.
    //
    //   T:System.NotSupportedException:
    //     The .NET Compact Framework does not currently support this method.
    //
    //   T:System.InvalidOperationException:
    //     The method represented by name has one or more unspecified generic type parameters.
    //     That is, the method's System.Reflection.MethodBase.ContainsGenericParameters
    //     property returns true.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object? InvokeMember(string name, BindingFlags invokeAttr, OriginBinder? binder, object? target, object?[]? args)
    {
    }

    //
    // Summary:
    //     Invokes the specified member, using the specified binding constraints and matching
    //     the specified argument list and culture.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the constructor, method, property, or field
    //     member to invoke. -or- An empty string ("") to invoke the default member. -or-
    //     For IDispatch members, a string representing the DispID, for example "[DispID=3]".
    //
    //   invokeAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. The access can be one of the BindingFlags such as Public, NonPublic,
    //     Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified.
    //     If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance
    //     | BindingFlags.Static are used.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder. Note that explicitly defining a System.Reflection.Binder
    //     object may be required for successfully invoking method overloads with variable
    //     arguments.
    //
    //   target:
    //     The object on which to invoke the specified member.
    //
    //   args:
    //     An array containing the arguments to pass to the member to invoke.
    //
    //   culture:
    //     The object representing the globalization locale to use, which may be necessary
    //     for locale-specific conversions, such as converting a numeric System.String to
    //     a System.Double. -or- A null reference (Nothing in Visual Basic) to use the current
    //     thread's System.Globalization.CultureInfo.
    //
    // Returns:
    //     An object representing the return value of the invoked member.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     invokeAttr does not contain CreateInstance and name is null.
    //
    //   T:System.ArgumentException:
    //     invokeAttr is not a valid System.Reflection.BindingFlags attribute. -or- invokeAttr
    //     does not contain one of the following binding flags: InvokeMethod, CreateInstance,
    //     GetField, SetField, GetProperty, or SetProperty. -or- invokeAttr contains CreateInstance
    //     combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.
    //     -or- invokeAttr contains both GetField and SetField. -or- invokeAttr contains
    //     both GetProperty and SetProperty. -or- invokeAttr contains InvokeMethod combined
    //     with SetField or SetProperty. -or- invokeAttr contains SetField and args has
    //     more than one element. -or- This method is called on a COM object and one of
    //     the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty,
    //     BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.
    //     -or- One of the named parameter arrays contains a string that is null.
    //
    //   T:System.MethodAccessException:
    //     The specified member is a class initializer.
    //
    //   T:System.MissingFieldException:
    //     The field or property cannot be found.
    //
    //   T:System.MissingMethodException:
    //     No method can be found that matches the arguments in args. -or- The current System.Type
    //     object represents a type that contains open type parameters, that is, System.Type.ContainsGenericParameters
    //     returns true.
    //
    //   T:System.Reflection.TargetException:
    //     The specified member cannot be invoked on target.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method matches the binding criteria.
    //
    //   T:System.InvalidOperationException:
    //     The method represented by name has one or more unspecified generic type parameters.
    //     That is, the method's System.Reflection.MethodBase.ContainsGenericParameters
    //     property returns true.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object? InvokeMember(string name, BindingFlags invokeAttr, OriginBinder? binder, object? target, object?[]? args, CultureInfo? culture)
    {
    }

    //
    // Summary:
    //     When overridden in a derived class, invokes the specified member, using the specified
    //     binding constraints and matching the specified argument list, modifiers and culture.
    //
    // Parameters:
    //   name:
    //     The string containing the name of the constructor, method, property, or field
    //     member to invoke. -or- An empty string ("") to invoke the default member. -or-
    //     For IDispatch members, a string representing the DispID, for example "[DispID=3]".
    //
    //   invokeAttr:
    //     A bitwise combination of the enumeration values that specify how the search is
    //     conducted. The access can be one of the BindingFlags such as Public, NonPublic,
    //     Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified.
    //     If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance
    //     | BindingFlags.Static are used.
    //
    //   binder:
    //     An object that defines a set of properties and enables binding, which can involve
    //     selection of an overloaded method, coercion of argument types, and invocation
    //     of a member through reflection. -or- A null reference (Nothing in Visual Basic),
    //     to use the System.Type.DefaultBinder. Note that explicitly defining a System.Reflection.Binder
    //     object may be required for successfully invoking method overloads with variable
    //     arguments.
    //
    //   target:
    //     The object on which to invoke the specified member.
    //
    //   args:
    //     An array containing the arguments to pass to the member to invoke.
    //
    //   modifiers:
    //     An array of System.Reflection.ParameterModifier objects representing the attributes
    //     associated with the corresponding element in the args array. A parameter's associated
    //     attributes are stored in the member's signature. The default binder processes
    //     this parameter only when calling a COM component.
    //
    //   culture:
    //     The System.Globalization.CultureInfo object representing the globalization locale
    //     to use, which may be necessary for locale-specific conversions, such as converting
    //     a numeric String to a Double. -or- A null reference (Nothing in Visual Basic)
    //     to use the current thread's System.Globalization.CultureInfo.
    //
    //   namedParameters:
    //     An array containing the names of the parameters to which the values in the args
    //     array are passed.
    //
    // Returns:
    //     An object representing the return value of the invoked member.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     invokeAttr does not contain CreateInstance and name is null.
    //
    //   T:System.ArgumentException:
    //     args and modifiers do not have the same length. -or- invokeAttr is not a valid
    //     System.Reflection.BindingFlags attribute. -or- invokeAttr does not contain one
    //     of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField,
    //     GetProperty, or SetProperty. -or- invokeAttr contains CreateInstance combined
    //     with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- invokeAttr
    //     contains both GetField and SetField. -or- invokeAttr contains both GetProperty
    //     and SetProperty. -or- invokeAttr contains InvokeMethod combined with SetField
    //     or SetProperty. -or- invokeAttr contains SetField and args has more than one
    //     element. -or- The named parameter array is larger than the argument array. -or-
    //     This method is called on a COM object and one of the following binding flags
    //     was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty,
    //     BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty. -or- One of
    //     the named parameter arrays contains a string that is null.
    //
    //   T:System.MethodAccessException:
    //     The specified member is a class initializer.
    //
    //   T:System.MissingFieldException:
    //     The field or property cannot be found.
    //
    //   T:System.MissingMethodException:
    //     No method can be found that matches the arguments in args. -or- No member can
    //     be found that has the argument names supplied in namedParameters. -or- The current
    //     System.Type object represents a type that contains open type parameters, that
    //     is, System.Type.ContainsGenericParameters returns true.
    //
    //   T:System.Reflection.TargetException:
    //     The specified member cannot be invoked on target.
    //
    //   T:System.Reflection.AmbiguousMatchException:
    //     More than one method matches the binding criteria.
    //
    //   T:System.InvalidOperationException:
    //     The method represented by name has one or more unspecified generic type parameters.
    //     That is, the method's System.Reflection.MethodBase.ContainsGenericParameters
    //     property returns true.
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public object? InvokeMember(string name, BindingFlags invokeAttr, OriginBinder? binder, object? target, object?[]? args, OriginParameterModifier[]? modifiers, CultureInfo? culture, string[]? namedParameters)
    {
    }

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.IsArray property
    //     and determines whether the System.Type is an array.
    //
    // Returns:
    //     true if the System.Type is an array; otherwise, false.
    protected abstract bool IsArrayImpl();

    //
    // Summary:
    //     Determines whether an instance of a specified type c can be assigned to a variable
    //     of the current type.
    //
    // Parameters:
    //   c:
    //     The type to compare with the current type.
    //
    // Returns:
    //     true if any of the following conditions is true: - c and the current instance
    //     represent the same type. - c is derived either directly or indirectly from the
    //     current instance. c is derived directly from the current instance if it inherits
    //     from the current instance; c is derived indirectly from the current instance
    //     if it inherits from a succession of one or more classes that inherit from the
    //     current instance. - The current instance is an interface that c implements. -
    //     c is a generic type parameter, and the current instance represents one of the
    //     constraints of c. - c represents a value type, and the current instance represents
    //     Nullable<c> (Nullable(Of c) in Visual Basic). false if none of these conditions
    //     are true, or if c is null.
    public virtual bool IsAssignableFrom([NotNullWhen(true)] Type? c)
    {
        throw null;
    }

    //
    // Summary:
    //     Determines whether the current type can be assigned to a variable of the specified
    //     targetType.
    //
    // Parameters:
    //   targetType:
    //     The type to compare with the current type.
    //
    // Returns:
    //     true if any of the following conditions is true: - The current instance and targetType
    //     represent the same type. - The current type is derived either directly or indirectly
    //     from targetType. The current type is derived directly from targetType if it inherits
    //     from targetType; the current type is derived indirectly from targetType if it
    //     inherits from a succession of one or more classes that inherit from targetType.
    //     - targetType is an interface that the current type implements. - The current
    //     type is a generic type parameter, and targetType represents one of the constraints
    //     of the current type. - The current type represents a value type, and targetType
    //     represents Nullable<c> (Nullable(Of c) in Visual Basic). false if none of these
    //     conditions are true, or if targetType is null.
    public bool IsAssignableTo([NotNullWhen(true)] Type? targetType)
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.IsByRef property
    //     and determines whether the System.Type is passed by reference.
    //
    // Returns:
    //     true if the System.Type is passed by reference; otherwise, false.
    protected abstract bool IsByRefImpl();

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.IsCOMObject property
    //     and determines whether the System.Type is a COM object.
    //
    // Returns:
    //     true if the System.Type is a COM object; otherwise, false.
    protected abstract bool IsCOMObjectImpl();

    //
    // Summary:
    //     Implements the System.Type.IsContextful property and determines whether the System.Type
    //     can be hosted in a context.
    //
    // Returns:
    //     true if the System.Type can be hosted in a context; otherwise, false.
    protected virtual bool IsContextfulImpl()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a value that indicates whether the specified value exists in the current
    //     enumeration type.
    //
    // Parameters:
    //   value:
    //     The value to be tested.
    //
    // Returns:
    //     true if the specified value is a member of the current enumeration type; otherwise,
    //     false.
    //
    // Exceptions:
    //   T:System.ArgumentException:
    //     The current type is not an enumeration.
    //
    //   T:System.ArgumentNullException:
    //     value is null.
    //
    //   T:System.InvalidOperationException:
    //     value is of a type that cannot be the underlying type of an enumeration.
    public virtual bool IsEnumDefined(object value)
    {
        throw null;
    }

    //
    // Summary:
    //     Determines whether two COM types have the same identity and are eligible for
    //     type equivalence.
    //
    // Parameters:
    //   other:
    //     The COM type that is tested for equivalence with the current type.
    //
    // Returns:
    //     true if the COM types are equivalent; otherwise, false. This method also returns
    //     false if one type is in an assembly that is loaded for execution, and the other
    //     is in an assembly that is loaded into the reflection-only context.
    public virtual bool IsEquivalentTo([NotNullWhen(true)] Type? other)
    {
        throw null;
    }

    //
    // Summary:
    //     Determines whether the specified object is an instance of the current System.Type.
    //
    // Parameters:
    //   o:
    //     The object to compare with the current type.
    //
    // Returns:
    //     true if the current Type is in the inheritance hierarchy of the object represented
    //     by o, or if the current Type is an interface that o implements. false if neither
    //     of these conditions is the case, if o is null, or if the current Type is an open
    //     generic type (that is, System.Type.ContainsGenericParameters returns true).
    public virtual bool IsInstanceOfType([NotNullWhen(true)] object? o)
    {
        throw null;
    }

    //
    // Summary:
    //     Implements the System.Type.IsMarshalByRef property and determines whether the
    //     System.Type is marshaled by reference.
    //
    // Returns:
    //     true if the System.Type is marshaled by reference; otherwise, false.
    protected virtual bool IsMarshalByRefImpl()
    {
        throw null;
    }

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.IsPointer property
    //     and determines whether the System.Type is a pointer.
    //
    // Returns:
    //     true if the System.Type is a pointer; otherwise, false.
    protected abstract bool IsPointerImpl();

    //
    // Summary:
    //     When overridden in a derived class, implements the System.Type.IsPrimitive property
    //     and determines whether the System.Type is one of the primitive types.
    //
    // Returns:
    //     true if the System.Type is one of the primitive types; otherwise, false.
    protected abstract bool IsPrimitiveImpl();

    //
    // Summary:
    //     Determines whether the current System.Type derives from the specified System.Type.
    //
    // Parameters:
    //   c:
    //     The type to compare with the current type.
    //
    // Returns:
    //     true if the current Type derives from c; otherwise, false. This method also returns
    //     false if c and the current Type are equal.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     c is null.
    public virtual bool IsSubclassOf(Type c)
    {
        throw null;
    }

    //
    // Summary:
    //     Implements the System.Type.IsValueType property and determines whether the System.Type
    //     is a value type; that is, not a class or an interface.
    //
    // Returns:
    //     true if the System.Type is a value type; otherwise, false.
    protected virtual bool IsValueTypeImpl()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a System.Type object representing a one-dimensional array of the current
    //     type, with a lower bound of zero.
    //
    // Returns:
    //     A System.Type object representing a one-dimensional array of the current type,
    //     with a lower bound of zero.
    //
    // Exceptions:
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must provide
    //     an implementation.
    //
    //   T:System.TypeLoadException:
    //     The current type is System.TypedReference. -or- The current type is a ByRef type.
    //     That is, System.Type.IsByRef returns true.
    public virtual Type MakeArrayType()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a System.Type object representing an array of the current type, with
    //     the specified number of dimensions.
    //
    // Parameters:
    //   rank:
    //     The number of dimensions for the array. This number must be less than or equal
    //     to 32.
    //
    // Returns:
    //     An object representing an array of the current type, with the specified number
    //     of dimensions.
    //
    // Exceptions:
    //   T:System.IndexOutOfRangeException:
    //     rank is invalid. For example, 0 or negative.
    //
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    //
    //   T:System.TypeLoadException:
    //     The current type is System.TypedReference. -or- The current type is a ByRef type.
    //     That is, System.Type.IsByRef returns true. -or- rank is greater than 32.
    public virtual Type MakeArrayType(int rank)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a System.Type object that represents the current type when passed as
    //     a ref parameter (ByRef parameter in Visual Basic).
    //
    // Returns:
    //     A System.Type object that represents the current type when passed as a ref parameter
    //     (ByRef parameter in Visual Basic).
    //
    // Exceptions:
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    //
    //   T:System.TypeLoadException:
    //     The current type is System.TypedReference. -or- The current type is a ByRef type.
    //     That is, System.Type.IsByRef returns true.
    public virtual Type MakeByRefType()
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a signature type object that can be passed into the Type[] array parameter
    //     of a Overload:System.Type.GetMethod method to represent a generic parameter reference.
    //
    // Parameters:
    //   position:
    //     The typed parameter position.
    //
    // Returns:
    //     A signature type object that can be passed into the Type[] array parameter of
    //     a Overload:System.Type.GetMethod method to represent a generic parameter reference.
    //
    // Exceptions:
    //   T:System.ArgumentOutOfRangeException:
    //     position is negative.
    public static Type MakeGenericMethodParameter(int position)
    {
        throw null;
    }

    //
    // Summary:
    //     Creates a generic signature type, which allows third party reimplementations
    //     of Reflection to fully support the use of signature types in querying type members.
    //
    // Parameters:
    //   genericTypeDefinition:
    //     The generic type definition.
    //
    //   typeArguments:
    //     An array of type arguments.
    //
    // Returns:
    //     A generic signature type.
    public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
    {
        throw null;
    }

    //
    // Summary:
    //     Substitutes the elements of an array of types for the type parameters of the
    //     current generic type definition and returns a System.Type object representing
    //     the resulting constructed type.
    //
    // Parameters:
    //   typeArguments:
    //     An array of types to be substituted for the type parameters of the current generic
    //     type.
    //
    // Returns:
    //     A System.Type representing the constructed type formed by substituting the elements
    //     of typeArguments for the type parameters of the current generic type.
    //
    // Exceptions:
    //   T:System.InvalidOperationException:
    //     The current type does not represent a generic type definition. That is, System.Type.IsGenericTypeDefinition
    //     returns false.
    //
    //   T:System.ArgumentNullException:
    //     typeArguments is null. -or- Any element of typeArguments is null.
    //
    //   T:System.ArgumentException:
    //     The number of elements in typeArguments is not the same as the number of type
    //     parameters in the current generic type definition. -or- Any element of typeArguments
    //     does not satisfy the constraints specified for the corresponding type parameter
    //     of the current generic type. -or- typeArguments contains an element that is a
    //     pointer type (System.Type.IsPointer returns true), a by-ref type (System.Type.IsByRef
    //     returns true), or System.Void.
    //
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class. Derived classes must provide
    //     an implementation.
    [RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
    public virtual Type MakeGenericType(params Type[] typeArguments)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a System.Type object that represents a pointer to the current type.
    //
    // Returns:
    //     A System.Type object that represents a pointer to the current type.
    //
    // Exceptions:
    //   T:System.NotSupportedException:
    //     The invoked method is not supported in the base class.
    //
    //   T:System.TypeLoadException:
    //     The current type is System.TypedReference. -or- The current type is a ByRef type.
    //     That is, System.Type.IsByRef returns true.
    public virtual Type MakePointerType()
    {
        throw null;
    }

    //
    // Summary:
    //     Indicates whether two System.Type objects are equal.
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
    [NullableContext(2)]
    public static bool operator ==(Type? left, Type? right)
    {
        throw null;
    }

    //
    // Summary:
    //     Indicates whether two System.Type objects are not equal.
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
    [NullableContext(2)]
    public static bool operator !=(Type? left, Type? right)
    {
        throw null;
    }

    //
    // Summary:
    //     Gets the System.Type with the specified name, specifying whether to perform a
    //     case-sensitive search and whether to throw an exception if the type is not found.
    //     The type is loaded for reflection only, not for execution.
    //
    // Parameters:
    //   typeName:
    //     The assembly-qualified name of the System.Type to get.
    //
    //   throwIfNotFound:
    //     true to throw a System.TypeLoadException if the type cannot be found; false to
    //     return null if the type cannot be found. Specifying false also suppresses some
    //     other exception conditions, but not all of them. See the Exceptions section.
    //
    //   ignoreCase:
    //     true to perform a case-insensitive search for typeName; false to perform a case-sensitive
    //     search for typeName.
    //
    // Returns:
    //     The type with the specified name, if found; otherwise, null. If the type is not
    //     found, the throwIfNotFound parameter specifies whether null is returned or an
    //     exception is thrown. In some cases, an exception is thrown regardless of the
    //     value of throwIfNotFound. See the Exceptions section.
    //
    // Exceptions:
    //   T:System.ArgumentNullException:
    //     typeName is null.
    //
    //   T:System.Reflection.TargetInvocationException:
    //     A class initializer is invoked and throws an exception.
    //
    //   T:System.TypeLoadException:
    //     throwIfNotFound is true and the type is not found. -or- throwIfNotFound is true
    //     and typeName contains invalid characters, such as an embedded tab. -or- throwIfNotFound
    //     is true and typeName is an empty string. -or- throwIfNotFound is true and typeName
    //     represents an array type with an invalid size. -or- typeName represents an array
    //     of System.TypedReference objects.
    //
    //   T:System.ArgumentException:
    //     typeName does not include the assembly name. -or- throwIfNotFound is true and
    //     typeName contains invalid syntax; for example, "MyType[,*,]". -or- typeName represents
    //     a generic type that has a pointer type, a ByRef type, or System.Void as one of
    //     its type arguments. -or- typeName represents a generic type that has an incorrect
    //     number of type arguments. -or- typeName represents a generic type, and one of
    //     its type arguments does not satisfy the constraints for the corresponding type
    //     parameter.
    //
    //   T:System.IO.FileNotFoundException:
    //     throwIfNotFound is true and the assembly or one of its dependencies was not found.
    //
    //   T:System.IO.FileLoadException:
    //     The assembly or one of its dependencies was found, but could not be loaded.
    //
    //   T:System.BadImageFormatException:
    //     The assembly or one of its dependencies is not valid. -or- The assembly was compiled
    //     with a later version of the common language runtime than the version that is
    //     currently loaded.
    //
    //   T:System.PlatformNotSupportedException:
    //     .NET Core and .NET 5+ only: In all cases.
    [Obsolete("ReflectionOnly loading is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0018", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
    public static Type? ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
    {
        throw null;
    }

    //
    // Summary:
    //     Returns a String representing the name of the current Type.
    //
    // Returns:
    //     A System.String representing the name of the current System.Type.
    public override string ToString()
    {
        throw null;
    }
}
