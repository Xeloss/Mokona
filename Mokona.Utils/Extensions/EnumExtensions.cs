namespace Mokona.Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Dynamic;
    using System.Linq;

    public static class EnumExtensions
    {
        public static List<KeyValuePair<TEnum, string>> ToList<TEnum>() 
            where TEnum : struct, IConvertible
        {
            var anEnumType = typeof(TEnum);
            if (!anEnumType.IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var result = new List<KeyValuePair<TEnum, string>>();
            var values = Enum.GetValues(anEnumType).Cast<TEnum>();

            foreach (var value in values)
            {
                result.Add(new KeyValuePair<TEnum, string>(value, value.DisplayValue()));
            }

            return result;
        }

        public static dynamic ToObject<TEnum>()
            where TEnum : struct, IConvertible
        {
            var anEnumType = typeof(TEnum);
            if (!anEnumType.IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            dynamic result = new ExpandoObject();
            var values = Enum.GetValues(anEnumType).Cast<TEnum>();

            foreach (var value in values)
            {
                ((IDictionary<string, object>)result).Add(value.ToString(), value.ToString());
            }

            return result;
        }

        public static string DisplayValue<T>(this T value) where T : struct, IConvertible
        {
            return GetDisplayValue(ref value);
        }

        private static string GetDisplayValue<T>(ref T value) where T : struct, IConvertible
        {
            var displayAttributeType = typeof(DisplayAttribute);

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return string.Empty;

            var attr = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();
            if (attr == null)
                return field.Name;

            return attr.GetName();
        }
    }
}
