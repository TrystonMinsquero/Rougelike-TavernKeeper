using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


[Serializable]
public abstract class Enumeration : IComparable
{
    private readonly int _value;
    private readonly string _displayName;

    protected Enumeration()
    {
    }

    protected Enumeration(int value, string displayName)
    {
        _value = value;
        _displayName = displayName;
    }

    public int Value
    {
        get { return _value; }
    }

    public string DisplayName
    {
        get { return _displayName; }
    }

    public override string ToString()
    {
        return DisplayName;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();
            var locatedValue = info.GetValue(instance) as T;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration;

        if (otherValue == null)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = _value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
    {
        var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }
    
    public static T FromDisplayName<T>(T type, string displayName) where T : Enumeration, new()
    {
        var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }
    
    private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ApplicationException(message);
        }

        return matchingItem;
    }

    public int CompareTo(object other)
    {
        return Value.CompareTo(((Enumeration)other).Value);
    }
    
}

public static class EnumerationExtensions
{
    public static IEnumerable<Enumeration> GetAll(this Enumeration enumerable)
    {
        var type = enumerable.GetType();
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = Activator.CreateInstance(type, true);
            var locatedValue = info.GetValue(instance) as Enumeration;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public static IEnumerable<Enumeration> GetAll(this Type enumerableType)
    {
        var fields = enumerableType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = Activator.CreateInstance(enumerableType, true);
            var locatedValue = info.GetValue(instance) as Enumeration;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public static Enumeration FromDisplayName(this Enumeration enumeration, string displayName)
    {
        var matchingItem = enumeration.GetAll().FirstOrDefault(item => item.DisplayName == displayName);
        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", displayName, "Display Name", enumeration.GetType());
            throw new ApplicationException(message);
        }

        return matchingItem;
    }
}