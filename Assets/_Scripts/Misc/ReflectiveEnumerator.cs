using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Misc
{
    public static class ReflectiveEnumerator
    {
        static ReflectiveEnumerator() { }

        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in 
                     Assembly.GetAssembly(typeof(T)).GetTypes()
                         .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            return objects;
        }
        
        public static IEnumerable<Type> GetEnumerableOfType(Type parentType)
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in 
                     Assembly.GetAssembly(parentType).GetTypes()
                         .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(parentType)))
            {
                objects.Add(type);
            }
            return objects;
        }
    }
}