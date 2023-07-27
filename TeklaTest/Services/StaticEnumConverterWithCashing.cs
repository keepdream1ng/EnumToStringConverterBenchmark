using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tekla.Extension.Services
{
    /// <summary>
    /// Static version of enum converter with cashing.
    /// </summary>
    public static class StaticEnumConverterWithCashing
    {
        /// <summary>
        /// Cash for storing string array depending on Enum type.
        /// </summary>
        private static Dictionary<Type, string[]> _enumCorrespondenceArrayCash = new Dictionary<Type, string[]>();

        /// <summary>
        /// Convert string to Enum value.
        /// </summary>
        /// <param name="str">String value which API works with</param>
        /// <returns>May return -1 if string is not supported by given Enum.</returns>
        public static TEnum GetEnumFromString<TEnum>(string str) where TEnum : Enum
        {
            string[] EnumCorrespondenceArray;
            if (_enumCorrespondenceArrayCash.ContainsKey(typeof(TEnum)))
            {
                EnumCorrespondenceArray = _enumCorrespondenceArrayCash[typeof(TEnum)];
            } else
            {
                EnumCorrespondenceArray = GetEnumCorrespondenceArray<TEnum>();
                _enumCorrespondenceArrayCash.Add(typeof(TEnum), EnumCorrespondenceArray);
            }

            return (TEnum)(object)Array.IndexOf(EnumCorrespondenceArray, str);
        }

        /// <summary>
        /// Convert given Enum value to string.
        /// Only works with Enums what type is specified upon creation of the class.
        /// </summary>
        /// <param name="eValue">Value of a given Enum</param>
        /// <returns>String value API working with</returns>
        public static string GetStringValueFromEnum(Enum eValue)
        {
            return _enumCorrespondenceArrayCash[eValue.GetType()][(int)(object)eValue];
        }
        /// <summary>
        /// Uses reflection to get Enum Description attributes array.
        /// </summary>
        /// <typeparam name="TEnum">Generic version of Enum</typeparam>
        /// <returns>Sting array</returns>
        private static string[] GetEnumCorrespondenceArray<TEnum>() where TEnum : Enum
        {
            TEnum[] enumValues = Enum.GetValues(typeof(TEnum))
                                           .Cast<TEnum>()
                                           .ToArray();

            return enumValues.Select(val => GetDescription(val)).ToArray();
        }

        /// <summary>
        /// Gets Description attribute value from Enum.
        /// </summary>
        /// <param name="enumValue"></param>
        private static string GetDescription(Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                               .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute?.Description ?? enumValue.ToString();
        }
    }
}
