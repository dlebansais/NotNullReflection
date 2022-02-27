# NotNullReflection

Encapsulation of `System.Reflection` with no null references whatsoever.

[![Build status](https://ci.appveyor.com/api/projects/status/w7j88ujjovca8ipn?svg=true)](https://ci.appveyor.com/project/dlebansais/notnullreflection) [![CodeFactor](https://www.codefactor.io/repository/github/dlebansais/easly-language/badge)](https://www.codefactor.io/repository/github/dlebansais/easly-language)

## Supported types

+ `System.Reflection.Assembly`
+ `System.Reflection.AssemblyName`
+ `System.Reflection.ConstructorInfo`
+ `System.Reflection.EventInfo`
+ `System.Reflection.FieldInfo`
+ `System.Reflection.ManifestResourceInfo`
+ `System.Reflection.MemberInfo`
+ `System.Reflection.MethodBase`
+ `System.Reflection.MethodInfo`
+ `System.Reflection.Module`
+ `System.Reflection.PropertyInfo`
+ `System.Type`

## Supported frameworks

+ .Net Standard 2.1
+ .Net Core 3.1
+ .Net Framework 4.8
+ .Net 5.0
+ .Net 6.0

x64 platform only, nullable enabled, C# 10.

## How to use

Add a reference to this package in your project, and insert in your source files:

```
using NotNullReflection;
```

In case of conflict with `using System;` you can remove it and replace it with specific aliases for the types you need. For instance, to use both `NotNullReflection.Type` and `InvalidOperationException` insert:

```
using NotNullReflection;
using InvalidOperationException = System.InvalidOperationException;
```
 
Or you can create an alias on `NotNullReflection.Type` instead, depending on what is most convenient for you.

## Origin type

You can access the origin `System.Type` object with the `Origin` member:

```
using NotNullReflection;

Type StringType = Type.FromTypeof<string>();
System.Type SystemStringType = StringType.Origin;
```

## Tools

A few tools have been added to the `NotNullReflection` assembly.

### Type

+ You can get a `NotNullReflection.Type` from `typeof(...)` with the `FromTypeof<T>()` static method (see the previous section).
+ You can get the type of an object with the `FromGetType(object obj)` static method.
+ You can check if a `Type` and `typeof(...)` represent the same type with `IsTypeof<T>()`. For the specific case of `void` that cannot be used as a generic argument, use `IsTypeofVoid()`.

### Property

You can check whether a string is the name of a property and get the corresponding `PropertyInfo` object at the same time:

```
using NotNullReflection;

Type StringType = Type.FromTypeof<string>();
if (StringType.IsProperty("Length", out PropertyInfo Property)) { ... }
```

### Field, event

The same methods exist for fields and events: `IsField(string, out FieldInfo)` and `IsEvent(string, out EventInfo)`. 

### Assembly

You can check whether a string is the name of a type in an assembly, and get the corresponding `Type` object at the same time, with `IsType(string, out Type)`.


## null values

When a method of `System.Type` expects a parameter to mean something special when `null`, use one of these default objects:

+ `NotNullReflection.Assembly.DefaultBinder`.
+ `NotNullReflection.Type.DefaultAssemblyResolver`.
+ `NotNullReflection.Type.DefaultTypeResolver`.
+ `NotNullReflection.Type.Void`.
+ `NotNullReflection.Type.Missing`.
+ An empty array.

Check the method documentation for details.

### Null as a return value

The `NotNullReflection` assembly never returns `null`. It will throw an exception instead, usually `NullReferenceException` (but check each method's or property's documentation).
