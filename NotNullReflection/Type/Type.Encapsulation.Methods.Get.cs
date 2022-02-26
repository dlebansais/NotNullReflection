namespace NotNullReflection;

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using AmbiguousMatchException = System.Reflection.AmbiguousMatchException;
using ArgumentException = System.ArgumentException;
using Array = System.Array;
using Binder = System.Reflection.Binder;
using BindingFlags = System.Reflection.BindingFlags;
using CallingConventions = System.Reflection.CallingConventions;
using DefaultMemberAttribute = System.Reflection.DefaultMemberAttribute;
using InterfaceMapping = System.Reflection.InterfaceMapping;
using InvalidOperationException = System.NullReferenceException;
using MemberTypes = System.Reflection.MemberTypes;
using NotSupportedException = System.NotSupportedException;
using NullReferenceException = System.NullReferenceException;
using OriginFieldInfo = System.Reflection.FieldInfo;
using OriginType = System.Type;
using ParameterModifier = System.Reflection.ParameterModifier;
using TargetInvocationException = System.Reflection.TargetInvocationException;

/// <summary>
/// Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.
/// </summary>
public partial class Type
{
#if NET6_0_OR_GREATER
    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. -or- <see cref="OriginType.EmptyTypes"/>.</param>
    /// <returns>A <see cref="ConstructorInfo"/> object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Type[] types)
    {
        return ConstructorInfo.CreateNew(Origin.GetConstructor(bindingAttr, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("No constructor found that matches the specified requirements."));
    }
#endif

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters..</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
#endif
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
        return ConstructorInfo.CreateNew(Origin.GetConstructor(bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements."));
    }

    /// <summary>
    /// Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection. -or- A null reference (Nothing in Visual Basic), to use the System.Type.DefaultBinder.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. -or- <see cref="OriginType.EmptyTypes"/>.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>A <see cref="ConstructorInfo"/> object representing the constructor that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
#endif
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
        return ConstructorInfo.CreateNew(Origin.GetConstructor(bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("No constructor found that matches the specified requirements."));
    }

    /// <summary>
    /// Searches for a public instance constructor whose parameters match the types in the specified array.
    /// </summary>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the constructor to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="System.Array.Empty{Type}()"/>) to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="OriginType.EmptyTypes"/>.</param>
    /// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">No constructor found that matches the specified requirements.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
    public ConstructorInfo GetConstructor(Type[] types)
    {
        return ConstructorInfo.CreateNew(Origin.GetConstructor(GetOriginList(types).ToArray()) ?? throw new NullReferenceException("No constructor found that matches the specified requirements."));
    }

    /// <summary>
    /// Returns all the public constructors defined for the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="ConstructorInfo"/> objects representing all the public instance constructors defined for the current <see cref="Type"/>, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="Type"/>, or if the current <see cref="Type"/> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="ConstructorInfo"/> is returned.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
    public ConstructorInfo[] GetConstructors()
    {
        return ConstructorInfo.GetList(Origin.GetConstructors()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the constructors defined for the current <see cref="Type"/>, using the specified <see cref="BindingFlags"/>.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="ConstructorInfo"/> objects representing all constructors defined for the current <see cref="Type"/> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="ConstructorInfo"/> if no constructors are defined for the current <see cref="Type"/>, if none of the defined constructors match the binding constraints, or if the current <see cref="Type"/> represents a type parameter in the definition of a generic type or generic method.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
#endif
    public ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
        return ConstructorInfo.GetList(Origin.GetConstructors(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the members defined for the current <see cref="Type"/> whose <see cref="DefaultMemberAttribute"/> is set.
    /// </summary>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing all default members of the current <see cref="Type"/>. -or- An empty array of type <see cref="MemberInfo"/>, if the current <see cref="Type"/> does not have default members.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
#endif
    public MemberInfo[] GetDefaultMembers()
    {
        return GetList(Origin.GetDefaultMembers()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="Type"/> of the object encompassed or referred to by the current array, pointer or reference type.
    /// </summary>
    /// <returns>The <see cref="Type"/> of the object encompassed or referred to by the current array, pointer, or reference type, or throws an exception if the current <see cref="Type"/> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
    /// <exception cref="NullReferenceException">Type doesn't have an element type.</exception>
    public Type GetElementType()
    {
        return CreateNew(Origin.GetElementType() ?? throw new NullReferenceException("Type doesn't have an element type."));
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
        return CreateNew(Origin.GetEnumUnderlyingType());
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
    /// Returns the <see cref="EventInfo"/> object representing the specified public event.
    /// </summary>
    /// <param name="name">The string containing the name of an event that is declared or inherited by the current <see cref="Type"/>.</param>
    /// <returns>The object representing the specified public event that is declared or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Event not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
#endif
    public EventInfo GetEvent(string name)
    {
        return EventInfo.CreateNew(Origin.GetEvent(name) ?? throw new NullReferenceException("Event not found."));
    }

    /// <summary>
    /// When overridden in a derived class, returns the <see cref="EventInfo"/> object representing the specified event, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of an event which is declared or inherited by the current <see cref="Type"/>.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>The object representing the specified event that is declared or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Event not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
#endif
    public EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
        return EventInfo.CreateNew(Origin.GetEvent(name, bindingAttr) ?? throw new NullReferenceException("Event not found."));
    }

    /// <summary>
    /// Returns all the public events that are declared or inherited by the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="EventInfo"/> objects representing all the public events which are declared or inherited by the current <see cref="Type"/>. -or- An empty array of type <see cref="EventInfo"/>, if the current <see cref="Type"/> does not have public events.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)]
#endif
    public EventInfo[] GetEvents()
    {
        return EventInfo.GetList(Origin.GetEvents()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="EventInfo"/> objects representing all events that are declared or inherited by the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="EventInfo"/>, if the current <see cref="Type"/> does not have events, or if none of the events match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)]
#endif
    public EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
        return EventInfo.GetList(Origin.GetEvents(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the public field with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the data field to get.</param>
    /// <returns>An object representing the public field with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NotSupportedException">This <see cref="Type"/> object is a <see cref="TypeBuilder"/> whose <see cref="TypeBuilder.CreateType"/> method has not yet been called.</exception>
    /// <exception cref="NullReferenceException">Field not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
#endif
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
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
#endif
    public OriginFieldInfo GetField(string name, BindingFlags bindingAttr)
    {
        return Origin.GetField(name, bindingAttr) ?? throw new NullReferenceException("Field not found.");
    }

    /// <summary>
    /// Returns all the public fields of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="OriginFieldInfo"/> objects representing all the public fields defined for the current <see cref="Type"/>. -or- An empty array of type <see cref="OriginFieldInfo"/>, if no public fields are defined for the current <see cref="Type"/>.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)]
#endif
    public OriginFieldInfo[] GetFields()
    {
        return Origin.GetFields();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the fields defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="OriginFieldInfo"/> objects representing all fields defined for the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="OriginFieldInfo"/>, if no fields are defined for the current <see cref="Type"/>, or if none of the defined fields match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)]
#endif
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
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> object is not a generic type parameter. That is, the <see cref="Type.IsGenericParameter"/> property returns <see langword="false"/>.</exception>
    public Type[] GetGenericParameterConstraints()
    {
        return GetList(Origin.GetGenericParameterConstraints()).ToArray();
    }

    /// <summary>
    /// Returns a <see cref="Type"/> object that represents a generic type definition from which the current generic type can be constructed.
    /// </summary>
    /// <returns>A <see cref="Type"/> object representing a generic type from which the current type can be constructed.</returns>
    /// <exception cref="InvalidOperationException">The current type is not a generic type. That is, <see cref="Type.IsGenericType"/> returns <see langword="false"/>.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    public Type GetGenericTypeDefinition()
    {
        return CreateNew(Origin.GetGenericTypeDefinition());
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
#if NET6_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
#endif
    public Type GetInterface(string name)
    {
        return CreateNew(Origin.GetInterface(name) ?? throw new NullReferenceException("Interface not found."));
    }

    /// <summary>
    /// When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.
    /// </summary>
    /// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
    /// <param name="ignoreCase"><see langword="true"/> to ignore the case of that part of name that specifies the simple interface name (the part that specifies the namespace must be correctly cased). -or- <see langword="false"/> to perform a case-sensitive search for all parts of name.</param>
    /// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="Type"/>, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">The current <see cref="Type"/> represents a type that implements the same generic interface with different type arguments.</exception>
    /// <exception cref="NullReferenceException">Interface not found.</exception>
#if NET6_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    [return: DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
#endif
    public Type GetInterface(string name, bool ignoreCase)
    {
        return CreateNew(Origin.GetInterface(name, ignoreCase) ?? throw new NullReferenceException("Interface not found."));
    }

    /// <summary>
    /// Returns an interface mapping for the specified interface type.
    /// </summary>
    /// <param name="interfaceType">The interface type to retrieve a mapping for.</param>
    /// <returns>An object that represents the interface mapping for <paramref name="interfaceType"/>.</returns>
    /// <exception cref="ArgumentException"><paramref name="interfaceType"/> is not implemented by the current type. -or- The <paramref name="interfaceType"/> argument does not refer to an interface. -or- The current instance of <paramref name="interfaceType"/> argument is an open generic type; that is, the <see cref="Type.ContainsGenericParameters"/> property returns <see langword="true"/>. -or- <paramref name="interfaceType"/> is a generic interface, and the current type is an array type.</exception>
    /// <exception cref="InvalidOperationException">The current <see cref="Type"/> represents a generic type parameter; that is, <see cref="Type.IsGenericParameter"/> is <see langword="true"/>.</exception>
    /// <exception cref="NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
    public InterfaceMapping GetInterfaceMap(
#if NET5_0_OR_GREATER
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
#endif
        Type interfaceType)
    {
        return Origin.GetInterfaceMap(interfaceType.Origin);
    }

    /// <summary>
    /// When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects representing all the interfaces implemented or inherited by the current <see cref="Type"/>. -or- An empty array of type <see cref="Type"/>, if no interfaces are implemented or inherited by the current <see cref="Type"/>.</returns>
    /// <exception cref="TargetInvocationException">A static initializer is invoked and throws an exception.</exception>
#if NET6_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
#endif
    public Type[] GetInterfaces()
    {
        return GetList(Origin.GetInterfaces()).ToArray();
    }

    /// <summary>
    /// Searches for the public members with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public members to get.</param>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
#endif
    public MemberInfo[] GetMember(string name)
    {
        return GetList(Origin.GetMember(name)).ToArray();
    }

    /// <summary>
    /// Searches for the specified members, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the members to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
#endif
    public MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
        return GetList(Origin.GetMember(name, bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the specified members of the specified member type, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the members to get.</param>
    /// <param name="type">The value to search for.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
    /// <exception cref="NotSupportedException">A derived class must provide an implementation.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
#endif
    public MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
        return GetList(Origin.GetMember(name, type, bindingAttr)).ToArray();
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Searches for the <see cref="MemberInfo"/> on the current <see cref="Type"/> that matches the specified <see cref="MemberInfo"/>.
    /// </summary>
    /// <param name="member">The <see cref="MemberInfo"/> to find on the current <see cref="Type"/>.</param>
    /// <returns>An object representing the member on the current <see cref="Type"/> that matches the specified member.</returns>
    /// <exception cref="ArgumentException">member does not match a member on the current <see cref="Type"/>.</exception>
    public MemberInfo GetMemberWithSameMetadataDefinitionAs(MemberInfo member)
    {
        return CreateNew(Origin.GetMemberWithSameMetadataDefinitionAs(member.Origin));
    }
#endif

    /// <summary>
    /// Returns all the public members of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing all the public members of the current <see cref="Type"/>. -or- An empty array of type <see cref="MemberInfo"/>, if the current <see cref="Type"/> does not have public members.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)2731)]
#endif
    public MemberInfo[] GetMembers()
    {
        return GetList(Origin.GetMembers()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the members defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="MemberInfo"/> objects representing all members defined for the current System.Type that match the specified binding constraints. -or- An empty array if no members are defined for the current <see cref="Type"/>, or if none of the defined members match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)8191)]
#endif
    public MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
        return GetList(Origin.GetMembers(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the public method with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <returns>An object that represents the public method with the specified name, if found; otherwise, null.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
#endif
    public MethodInfo GetMethod(string name)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name) ?? throw new NullReferenceException("Method not found."));
    }

#if NET5_0_OR_GREATER
    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified generic parameter count, argument types, modifiers, binding constraints and calling convention, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, genericParameterCount, bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified generic parameter count, argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified generic parameter count, argument types, modifiers and binding constraints, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, genericParameterCount, bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
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
    public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, genericParameterCount, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified generic parameter count, argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="genericParameterCount">The number of generic type parameters of the method.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public method that matches the specified generic parameter count, argument types and modifiers, if found; otherwise, throws an exception.</returns>
    /// <exception cref="ArgumentException"><paramref name="genericParameterCount"/> is negative.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
    public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, genericParameterCount, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
    }
#endif

    /// <summary>
    /// Searches for the specified method, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
#endif
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, bindingAttr) ?? throw new NullReferenceException("Method not found."));
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Method not found.</exception>
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Type[] types)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, bindingAttr, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found."));
    }
#endif

    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
#endif
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, bindingAttr, binder != DefaultBinder ? binder : null, callConvention, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the method to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
#endif
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, bindingAttr, binder != DefaultBinder ? binder : null, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
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
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
#endif
    public MethodInfo GetMethod(string name, Type[] types)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Searches for the specified public method whose parameters match the specified argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public method to get.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the method to get. -or- An empty array of <see cref="Type"/> objects (as provided by the System.Type.EmptyTypes field) to get a method that takes no parameters.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public method that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one method is found with the specified name and specified parameters.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional.</exception>
    /// <exception cref="NullReferenceException">Method not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
#endif
    public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
    {
        return MethodInfo.CreateNew(Origin.GetMethod(name, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Method not found."));
    }

    /// <summary>
    /// Returns all the public methods of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="MethodInfo"/> objects representing all the public methods defined for the current <see cref="Type"/>. -or- An empty array of type <see cref="MethodInfo"/>, if no public methods are defined for the current <see cref="Type"/>.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)]
#endif
    public MethodInfo[] GetMethods()
    {
        return MethodInfo.GetList(Origin.GetMethods()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the methods defined for the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of <see cref="MethodInfo"/> objects representing all methods defined for the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="MethodInfo"/>, if no methods are defined for the current <see cref="Type"/>, or if none of the defined methods match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)]
#endif
    public MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
        return MethodInfo.GetList(Origin.GetMethods(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the public nested type with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the nested type to get.</param>
    /// <returns>An object representing the public nested type with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Nested type not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
#endif
    public Type GetNestedType(string name)
    {
        return CreateNew(Origin.GetNestedType(name) ?? throw new NullReferenceException("Nested type not found."));
    }

    /// <summary>
    /// When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the nested type to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the nested type that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="NullReferenceException">Nested type not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
#endif
    public Type GetNestedType(string name, BindingFlags bindingAttr)
    {
        return CreateNew(Origin.GetNestedType(name, bindingAttr) ?? throw new NullReferenceException("Nested type not found."));
    }

    /// <summary>
    /// Returns the public types nested in the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="Type"/> objects representing the public types nested in the current <see cref="Type"/> (the search is not recursive), or an empty array of type <see cref="Type"/> if no public types are nested in the current <see cref="Type"/>.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes)]
#endif
    public Type[] GetNestedTypes()
    {
        return GetList(Origin.GetNestedTypes()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the types nested in the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An array of <see cref="Type"/> objects representing all the types nested in the current <see cref="Type"/> that match the specified binding constraints (the search is not recursive), or an empty array of type <see cref="Type"/>, if no nested types are found that match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicNestedTypes | DynamicallyAccessedMemberTypes.NonPublicNestedTypes)]
#endif
    public Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
        return GetList(Origin.GetNestedTypes(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Returns all the public properties of the current <see cref="Type"/>.
    /// </summary>
    /// <returns>An array of <see cref="PropertyInfo"/> objects representing all public properties of the current <see cref="Type"/>. -or- An empty array of type <see cref="PropertyInfo"/>, if the current <see cref="Type"/> does not have public properties.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo[] GetProperties()
    {
        return PropertyInfo.GetList(Origin.GetProperties()).ToArray();
    }

    /// <summary>
    /// When overridden in a derived class, searches for the properties of the current <see cref="Type"/>, using the specified binding constraints.
    /// </summary>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted. -or- <see cref="BindingFlags.Default"/> to return an empty array.</param>
    /// <returns>An array of objects representing all properties of the current <see cref="Type"/> that match the specified binding constraints. -or- An empty array of type <see cref="PropertyInfo"/>, if the current <see cref="Type"/> does not have properties, or if none of the properties match the binding constraints.</returns>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
#endif
    public PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
        return PropertyInfo.GetList(Origin.GetProperties(bindingAttr)).ToArray();
    }

    /// <summary>
    /// Searches for the public property with the specified name.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <returns>An object representing the public property with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo GetProperty(string name)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name) ?? throw new NullReferenceException("Property not found."));
    }

    /// <summary>
    /// Searches for the specified property, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the property to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <returns>An object representing the property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, bindingAttr) ?? throw new NullReferenceException("Property not found."));
    }

    /// <summary>
    /// Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.
    /// </summary>
    /// <param name="name">The string containing the name of the property to get.</param>
    /// <param name="bindingAttr">A bitwise combination of the enumeration values that specify how the search is conducted.</param>
    /// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, bindingAttr, binder != DefaultBinder ? binder : null, returnType.Origin, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Property not found."));
    }

    /// <summary>
    /// Searches for the public property with the specified name and return type.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <returns>An object representing the public property with the specified name, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, Type returnType)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, returnType.Origin) ?? throw new NullReferenceException("Property not found."));
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
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, returnType.Origin, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Property not found."));
    }

    /// <summary>
    /// Searches for the specified public property whose parameters match the specified argument types and modifiers.
    /// </summary>
    /// <param name="name">The string containing the name of the public property to get.</param>
    /// <param name="returnType">The return type of the property.</param>
    /// <param name="types">An array of <see cref="Type"/> objects representing the number, order, and type of the parameters for the indexed property to get. -or- An empty array of the type <see cref="Type"/> (that is, <see cref="Type"/>[] types = <see cref="Array.Empty{Type}()"/>) to get a property that is not indexed.</param>
    /// <param name="modifiers">An array of <see cref="ParameterModifier"/> objects representing the attributes associated with the corresponding element in the types array. The default binder does not process this parameter.</param>
    /// <returns>An object representing the public property that matches the specified requirements, if found; otherwise, throws an exception.</returns>
    /// <exception cref="AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers.</exception>
    /// <exception cref="ArgumentException"><paramref name="types"/> is multidimensional. -or- <paramref name="modifiers"/> is multidimensional. -or- <paramref name="types"/> and <paramref name="modifiers"/> do not have the same length.</exception>
    /// <exception cref="NullReferenceException">Property not found.</exception>
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, returnType.Origin, GetOriginList(types).ToArray(), modifiers) ?? throw new NullReferenceException("Property not found."));
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
#if NET5_0_OR_GREATER
    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
#endif
    public PropertyInfo GetProperty(string name, Type[] types)
    {
        return PropertyInfo.CreateNew(Origin.GetProperty(name, GetOriginList(types).ToArray()) ?? throw new NullReferenceException("Property not found."));
    }
}
