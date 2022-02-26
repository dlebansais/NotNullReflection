namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using AmbiguousMatchException = System.Reflection.AmbiguousMatchException;
using ArgumentException = System.ArgumentException;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using FieldAttributes = System.Reflection.FieldAttributes;
using IndexOutOfRangeException = System.IndexOutOfRangeException;
using InvalidOperationException = System.NullReferenceException;
using MemberFilter = System.Reflection.MemberFilter;
using MemberTypes = System.Reflection.MemberTypes;
using MethodAccessException = System.MethodAccessException;
using MethodAttributes = System.Reflection.MethodAttributes;
using MethodImplAttributes = System.Reflection.MethodImplAttributes;
using MissingFieldException = System.MissingFieldException;
using MissingMethodException = System.MissingMethodException;
using NotSupportedException = System.NotSupportedException;
using OriginType = System.Type;
using ParameterModifier = System.Reflection.ParameterModifier;
using TargetException = System.Reflection.TargetException;
using TargetInvocationException = System.Reflection.TargetInvocationException;
using TypedReference = System.TypedReference;
using TypeFilter = System.Reflection.TypeFilter;
using TypeLoadException = System.TypeLoadException;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
    /// <summary>
    /// Determines if the underlying system type of the current <see cref="Type"/> object is the same as the underlying system type of the specified <see cref="object"/>.
    /// </summary>
    /// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="Type"/>. For the comparison to succeed, <paramref name="o"/> must be able to be cast or converted to an object of type <see cref="Type"/>.</param>
    /// <returns><see langword="true"/> if the underlying system type of <paramref name="o"/> is the same as the underlying system type of the current S<see cref="Type"/>; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <paramref name="o"/> cannot be cast or converted to a <see cref="Type"/> object.</returns>
    public override bool Equals(object o)
    {
        return o is Type AsType && Origin.Equals(AsType.Origin);
    }

    /// <summary>
    /// Determines if the underlying system type of the current <see cref="Type"/> is the same as the underlying system type of the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="Type"/>.</param>
    /// <returns><see langword="true"/> if the underlying system type of <paramref name="o"/> is the same as the underlying system type of the current <see cref="Type"/>; otherwise, <see langword="false"/>.</returns>
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
#if NET6_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
#endif
    public Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
    {
        return GetList(Origin.FindInterfaces(filter, filterCriteria)).ToArray();
    }

    /// <summary>
    /// Returns a filtered array of <see cref="MemberInfo"/> objects of the specified member type.
    /// </summary>
    /// <param name="memberType">A bitwise combination of the enumeration values that indicates the type of member to search for.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="filter">The delegate that does the comparisons, returning <see langword="true"/> if the member currently being inspected matches the <paramref name="filterCriteria"/> and <see langword="false"/> otherwise.</param>
    /// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of <see cref="MemberInfo"/> objects. The fields of <see cref="FieldAttributes"/>, <see cref="MethodAttributes"/>, and <see cref="MethodImplAttributes"/> can be used in conjunction with the <see cref="OriginType.FilterAttribute"/> delegate supplied by this class.</param>
    /// <returns>A filtered array of <see cref="MemberInfo"/> objects of the specified member type. -or- An empty array if the current <see cref="Type"/> does not have members of type <paramref name="memberType"/> that match the filter criteria.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
    public MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
    {
        return GetList(Origin.FindMembers(memberType, bindingAttr, filter, filterCriteria)).ToArray();
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
    /// Invokes the specified member, using the specified binding constraints and matching the specified argument list.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="Binder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns <see langword="true"/>.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="MethodBase.ContainsGenericParameters"/> property returns <see langword="true"/>.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args) ?? Void;
    }

    /// <summary>
    /// Invokes the specified member, using the specified binding constraints and matching the specified argument list and culture.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="Binder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="string"/> to a <see cref="double"/>. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns <see langword="true"/>.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="MethodBase.ContainsGenericParameters"/> property returns <see langword="true"/>.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args, culture) ?? Void;
    }

    /// <summary>
    /// When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.
    /// </summary>
    /// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke. -or- An empty string <see cref="string.Empty"/> to invoke the default member. -or- For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
    /// <param name="invokeAttr">A bitwise combination of the enumeration values that specify how the search is conducted. The access can be one of the <see cref="BindingFlags"/> such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see cref="BindingFlags.Public"/> | <see cref="BindingFlags.Instance"/> | <see cref="BindingFlags.Static"/> are used.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. Note that explicitly defining a <see cref="Binder"/> object may be required for successfully invoking method overloads with variable arguments.</param>
    /// <param name="target">The object on which to invoke the specified member.</param>
    /// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the args array. A parameter's associated attributes are stored in the member's signature. The default binder processes this parameter only when calling a COM component.</param>
    /// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="string"/> to a <see cref="double"/>. Use <see cref="CultureInfo.CurrentCulture"/> if the default is desired.</param>
    /// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args"/> array are passed.</param>
    /// <returns>An object representing the return value of the invoked member.</returns>
    /// <exception cref="ArgumentException"><paramref name="invokeAttr"/> is not a valid <see cref="BindingFlags"/> attribute. -or- <paramref name="invokeAttr"/> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr"/> contains both GetField and SetField. -or- <paramref name="invokeAttr"/> contains both GetProperty and SetProperty. -or- <paramref name="invokeAttr"/> contains InvokeMethod combined with SetField or SetProperty. -or- <paramref name="invokeAttr"/> contains SetField and args has more than one element. -or- This method is called on a COM object and one of the following binding flags was not passed in: <see cref="BindingFlags.InvokeMethod"/>, <see cref="BindingFlags.GetProperty"/>, <see cref="BindingFlags.SetProperty"/>, <see cref="BindingFlags.PutDispProperty"/>, or <see cref="BindingFlags.PutRefDispProperty"/>.</exception>
    /// <exception cref="MethodAccessException">The specified member is a class initializer.</exception>
    /// <exception cref="MissingFieldException">The field or property cannot be found.</exception>
    /// <exception cref="MissingMethodException">No method can be found that matches the arguments in <paramref name="args"/>. -or- The current <see cref="Type"/> object represents a type that contains open type parameters, that is, <see cref="Type.ContainsGenericParameters"/> returns <see langword="true"/>.</exception>
    /// <exception cref="TargetException">The specified member cannot be invoked on target.</exception>
    /// <exception cref="AmbiguousMatchException">More than one method matches the binding criteria.</exception>
    /// <exception cref="InvalidOperationException">The method represented by name has one or more unspecified generic type parameters. That is, the method's <see cref="MethodBase.ContainsGenericParameters"/> property returns <see langword="true"/>.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
        return Origin.InvokeMember(name, invokeAttr, binder != DefaultBinder ? binder : null, target, args, modifiers, culture, namedParameters) ?? Void;
    }

    /// <summary>
    /// Determines whether an instance of a specified type <paramref name="c"/> can be assigned to a variable of the current type.
    /// </summary>
    /// <param name="c">The type to compare with the current type.</param>
    /// <returns><see langword="true"/> if any of the following conditions is true: - <paramref name="c"/> and the current instance represent the same type. - <paramref name="c"/> is derived either directly or indirectly from the current instance. <paramref name="c"/> is derived directly from the current instance if it inherits from the current instance; <paramref name="c"/> is derived indirectly from the current instance if it inherits from a succession of one or more classes that inherit from the current instance. - The current instance is an interface that <paramref name="c"/> implements. - <paramref name="c"/> is a generic type parameter, and the current instance represents one of the constraints of <paramref name="c"/>. - <paramref name="c"/> represents a value type, and the current instance represents <see cref="System.Nullable{c}"/>. <see langword="false"/> if none of these conditions are true.</returns>
    public bool IsAssignableFrom(Type c)
    {
        return Origin.IsAssignableFrom(c.Origin);
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Determines whether the current type can be assigned to a variable of the specified <paramref name="targetType"/>.
    /// </summary>
    /// <param name="targetType">The type to compare with the current type.</param>
    /// <returns><see langword="true"/> if any of the following conditions is true: - The current instance and <paramref name="targetType"/> represent the same type. - The current type is derived either directly or indirectly from <paramref name="targetType"/>. The current type is derived directly from <paramref name="targetType"/> if it inherits from <paramref name="targetType"/>; the current type is derived indirectly from <paramref name="targetType"/> if it inherits from a succession of one or more classes that inherit from <paramref name="targetType"/>. - <paramref name="targetType"/> is an interface that the current type implements. - The current type is a generic type parameter, and <paramref name="targetType"/> represents one of the constraints of the current type. - The current type represents a value type, and <paramref name="targetType"/> represents <see cref="System.Nullable{c}"/>. <see langword="false"/> if none of these conditions are true.</returns>
    public bool IsAssignableTo(Type targetType)
    {
        return Origin.IsAssignableTo(targetType.Origin);
    }
#endif

    /// <summary>
    /// Returns a value that indicates whether the specified value exists in the current enumeration type.
    /// </summary>
    /// <param name="value">The value to be tested.</param>
    /// <returns><see langword="true"/> if the specified value is a member of the current enumeration type; otherwise, <see langword="false"/>.</returns>
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
    /// <returns><see langword="true"/> if the COM types are equivalent; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if one type is in an assembly that is loaded for execution, and the other is in an assembly that is loaded into the reflection-only context.</returns>
    public bool IsEquivalentTo(Type other)
    {
        return Origin.IsEquivalentTo(other.Origin);
    }

    /// <summary>
    /// Determines whether the specified object is an instance of the current <see cref="Type"/>.
    /// </summary>
    /// <param name="o">The object to compare with the current type.</param>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> is in the inheritance hierarchy of the object represented by <paramref name="o"/>, or if the current <see cref="Type"/> is an interface that <paramref name="o"/> implements. <see langword="false"/> if neither of these conditions is the case or if the current <see cref="Type"/> is an open generic type (that is, <see cref="Type.ContainsGenericParameters"/> returns <see langword="true"/>).</returns>
    public bool IsInstanceOfType(object o)
    {
        return Origin.IsInstanceOfType(o);
    }

    /// <summary>
    /// Determines whether the current <see cref="Type"/> derives from the specified <see cref="Type"/>.
    /// </summary>
    /// <param name="c">The type to compare with the current type.</param>
    /// <returns><see langword="true"/> if the current <see cref="Type"/> derives from <paramref name="c"/>; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <paramref name="c"/> and the current <see cref="Type"/> are equal.</returns>
    public bool IsSubclassOf(Type c)
    {
        return Origin.IsSubclassOf(c.Origin);
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object representing a one-dimensional array of the current type, with a lower bound of zero.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns <see langword="true"/>.</exception>
    public Type MakeArrayType()
    {
        return CreateNew(Origin.MakeArrayType());
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object representing an array of the current type, with the specified number of dimensions.
    /// </summary>
    /// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
    /// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
    /// <exception cref="IndexOutOfRangeException"><paramref name="rank"/> is invalid. For example, 0 or negative.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns <see langword="true"/>. -or- <paramref name="rank"/> is greater than 32.</exception>
    public Type MakeArrayType(int rank)
    {
        return CreateNew(Origin.MakeArrayType(rank));
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents the current type when passed as a ref parameter.
    /// </summary>
    /// <returns>A <see cref="Type"/> object that represents the current type when passed as a ref parameter.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns <see langword="true"/>.</exception>
    public Type MakeByRefType()
    {
        return CreateNew(Origin.MakeByRefType());
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Returns a signature type object that can be passed into the <see cref="Type"/>[] array parameter of a Overload:System.Type.GetMethod method to represent a generic parameter reference.
    /// </summary>
    /// <param name="position">The typed parameter position.</param>
    /// <returns>A signature type object that can be passed into the <see cref="Type"/>[] array parameter of a Overload:System.Type.GetMethod method to represent a generic parameter reference.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/> is negative.</exception>
    public static Type MakeGenericMethodParameter(int position)
    {
        return CreateNew(OriginType.MakeGenericMethodParameter(position));
    }

    /// <summary>
    /// Creates a generic signature type, which allows third party reimplementations of Reflection to fully support the use of signature types in querying type members.
    /// </summary>
    /// <param name="genericTypeDefinition">The generic type definition.</param>
    /// <param name="typeArguments">An array of type arguments.</param>
    /// <returns>A generic signature type.</returns>
    public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
    {
        return CreateNew(OriginType.MakeGenericSignatureType(genericTypeDefinition.Origin, GetOriginList(typeArguments).ToArray()));
    }
#endif

    /// <summary>
    /// Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="Type"/> object representing the resulting constructed type.
    /// </summary>
    /// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
    /// <returns>A <see cref="Type"/> representing the constructed type formed by substituting the elements of <paramref name="typeArguments"/> for the type parameters of the current generic type.</returns>
    /// <exception cref="InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="IsGenericTypeDefinition"/> returns <see langword="false"/>.</exception>
    /// <exception cref="ArgumentException">The number of elements in <paramref name="typeArguments"/> is not the same as the number of type parameters in the current generic type definition. -or- Any element of <paramref name="typeArguments"/> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. -or- <paramref name="typeArguments"/> contains an element that is a pointer type (<see cref="IsPointer"/> returns <see langword="true"/>), a by-ref type (<see cref="IsByRef"/> returns <see langword="true"/>), or <see cref="System.Void"/>.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
#if NET5_0_OR_GREATER
    [RequiresUnreferencedCode("If some of the generic arguments are annotated (either with DynamicallyAccessedMembersAttribute, or generic constraints), trimming can't validate that the requirements of those annotations are met.")]
#endif
    public Type MakeGenericType(params Type[] typeArguments)
    {
        return CreateNew(Origin.MakeGenericType(GetOriginList(typeArguments).ToArray()));
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents a pointer to the current type.
    /// </summary>
    /// <returns>A <see cref="Type"/> object that represents a pointer to the current type.</returns>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class.</exception>
    /// <exception cref="TypeLoadException">The current type is <see cref="TypedReference"/>. -or- The current type is a ByRef type. That is, <see cref="IsByRef"/> returns <see langword="true"/>.</exception>
    public Type MakePointerType()
    {
        return CreateNew(Origin.MakePointerType());
    }

    /// <summary>
    /// Indicates whether two <see cref="Type"/> objects are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Type left, Type right)
    {
        return left.Origin == right.Origin;
    }

    /// <summary>
    /// Indicates whether two <see cref="Type"/> objects are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
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
