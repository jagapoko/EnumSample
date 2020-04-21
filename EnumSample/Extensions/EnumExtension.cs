using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EnumSample.Extensions
{
    public static class EnumExtension
    {
        #region Price属性
        /// <summary>
        /// Price属性
        /// </summary>
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class PriceAttribute : Attribute
        {
            public string Price { get; private set; }

            public PriceAttribute(string Price)
            {
                this.Price = Price;
            }
        }
        /// <summary>
        /// Price属性の取得
        /// </summary>
        public static string GetPrice(this Enum value)
            => value.GetAttribute<PriceAttribute>()?.Price
                ?? "\\0";
        #endregion


        #region Color属性
        /// <summary>
        /// Color属性
        /// </summary>
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class ColorAttribute : Attribute
        {
            public Color Color { get; private set; }

            public ColorAttribute(KnownColor Color)
            {
                this.Color = System.Drawing.Color.FromKnownColor(Color);
            }
        }
        /// <summary>
        /// Color属性の取得
        /// </summary>
        public static Color GetColor(this Enum value)
            => value.GetAttribute<ColorAttribute>()?.Color
                ?? Color.Empty;
        #endregion



        #region ResourceName属性
        /// <summary>
        /// ResourceName属性
        /// </summary>
        [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
        public sealed class ResourceNameAttribute : Attribute
        {
            public String ResourceName { get; private set; }

            public ResourceNameAttribute(String ResourceName)
            {
                this.ResourceName = ResourceName;
            }
        }
        /// <summary>
        /// ResourceName属性の取得
        /// </summary>
        public static String GetResourceName(this Enum value)
            => value.GetAttribute<ResourceNameAttribute>()?.ResourceName
                ?? "";
        #endregion


    }
}
