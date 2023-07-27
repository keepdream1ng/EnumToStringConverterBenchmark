using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tekla.Extension.Services
{
    /// <summary>
    /// Static version of enum converter without any cashing.
    /// </summary>
    public static class StaticEnumConverter
    {
        /// <summary>
        /// Convert string to Enum value.
        /// </summary>
        /// <param name="str">String value which API works with</param>
        /// <returns>May return -1 if string is not supported by given Enum.</returns>
        public static TEnum GetEnumFromString<TEnum>(string str) where TEnum : Enum
        {
            TEnum[] enumValues = Enum.GetValues(typeof(TEnum))
                                           .Cast<TEnum>()
                                           .ToArray();

            for (int i = 0; i < enumValues.Length; i++)
            {
                if (str == GetDescription(enumValues[i]))
                {
                    return (TEnum)(object)i;
                }
            }
            return (TEnum)(object)-1;
        }

        /// <summary>
        /// Convert given Enum value to string.
        /// Only works with Enums what type is specified upon creation of the class.
        /// </summary>
        /// <param name="eValue">Value of a given Enum</param>
        /// <returns>String value API working with</returns>
        public static string GetStringValueFromEnum(Enum eValue)
        {
            return GetDescription(eValue);
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
