using System;

namespace FacetedSearch.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsImplementationOf<T>(this Type type)
        {
            return IsImplementationOf(type, typeof (T));
        }

        public static bool IsImplementationOf(this Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.FullName) != null;
        }
    }
}