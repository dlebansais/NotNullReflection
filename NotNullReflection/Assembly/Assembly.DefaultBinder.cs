namespace NotNullReflection;

using System;
using System.Globalization;
using System.Reflection;
using OriginType = System.Type;

/// <summary>
/// Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.
/// </summary>
public partial class Assembly
{
    private class UnusedBinder : Binder
    {
        public UnusedBinder() { }
        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo? culture) { throw new NotSupportedException(); }
        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object?[] args, ParameterModifier[]? modifiers, CultureInfo? culture, string[]? names, out object? state) { throw new NotSupportedException(); }
        public override object ChangeType(object value, OriginType type, CultureInfo? culture) { throw new NotSupportedException(); }
        public override void ReorderArgumentArray(ref object?[] args, object state) { throw new NotSupportedException(); }
        public override MethodBase? SelectMethod(BindingFlags bindingAttr, MethodBase[] match, OriginType[] types, ParameterModifier[]? modifiers) { throw new NotSupportedException(); }
        public override PropertyInfo? SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, OriginType? returnType, OriginType[]? indexes, ParameterModifier[]? modifiers) { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Gets the default binder object.
    /// </summary>
    public static Binder DefaultBinder { get; } = new UnusedBinder();
}
