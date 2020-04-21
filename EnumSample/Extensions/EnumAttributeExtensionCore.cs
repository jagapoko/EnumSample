using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumSample.Extensions
{
    public static class EnumAttributeExtensionCore
    {
        private static class EnumAttributeCache<TAttribute> where TAttribute : Attribute
        {
            private static ConcurrentDictionary<Enum, TAttribute> body = new ConcurrentDictionary<Enum, TAttribute>();

            internal static TAttribute GetOrAdd(Enum enumKey, Func<Enum, TAttribute> valueFactory) => body.GetOrAdd(enumKey, valueFactory);
        }

        public static TAttribute GetAttribute<TAttribute>(this Enum enumKey) where TAttribute : Attribute
        {
            return EnumAttributeCache<TAttribute>.GetOrAdd(enumKey, _ => enumKey.GetAttributeCore<TAttribute>());
        }

        public static TAttribute GetAttributeCore<TAttribute>(this Enum enumKey) where TAttribute : Attribute
        {
            var fieldInfo = enumKey.GetType().GetField(enumKey.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>();
            if (!(attributes?.Any() ?? false)) return null;

            return attributes.First();

        }
    }
}
