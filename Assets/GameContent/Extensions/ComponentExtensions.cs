using System;
using UnityEngine;

public static class ComponentExtensions
{
    public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
    {
        if (!ReferenceEquals(component, null))
        {
            action?.Invoke(component);
        }
    }
}