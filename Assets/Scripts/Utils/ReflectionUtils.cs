using System;
using UnityEngine.Assertions;

public static class ReflectionUtils
{
    public static void FindAndSetPrivateField(Type targetType, string fieldName, object target, object value)
    {
        var fieldInfo = targetType.GetField(fieldName, 
            System.Reflection.BindingFlags.Instance | 
            System.Reflection.BindingFlags.NonPublic);
        
        Assert.IsNotNull(fieldInfo, $"Field {fieldName} has not been found in type {targetType}");
        
        fieldInfo.SetValue(target, value);
    }
    
    public static Type GetTypeFromInheritanceHierarchy(Type type, Type targetType)
    {
        Assert.IsFalse(type == typeof(object),
            $"Type {targetType} has not been found in inheritance hierarchy");
        
        if (type == targetType) return type;
        
        return GetTypeFromInheritanceHierarchy(type.BaseType, targetType);
    }

    public static T GetValueFromPrivateField<T>(Type targetType, string fieldName, object target)
    {
        var fieldInfo = targetType.GetField(fieldName, 
            System.Reflection.BindingFlags.Instance | 
            System.Reflection.BindingFlags.NonPublic);
        
        Assert.IsFalse(fieldInfo == null,
            $"Field {fieldName} has not been found in type {targetType}");

        object value = fieldInfo.GetValue(target);
        
        return value is T ? (T)value : default(T);
    }
}
