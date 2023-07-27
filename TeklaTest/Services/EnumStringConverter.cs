using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Tekla.Extension.Services
{
    /// <summary>
    /// Generic verison of Enum converter what does all reflection operations
    /// on the creation of class and cashes the values. May work slower because of boxing.
    /// </summary>
    /// <typeparam name="TEnum">Enum for which converter is created.</typeparam>
    public class EnumStringConverter<TEnum> where TEnum : Enum
    {
        /// <summary>
        /// Cashed storage of given upon creation Enum attributes.
        /// </summary>
        private static string[] _enumCorrespondenceArray;

        /// <summary>
        /// Convert string to Enum value.
        /// </summary>
        /// <param name="str">String value which API works with</param>
        /// <returns>May return -1 if string is not supported by given Enum.</returns>
        public TEnum GetEnumFromString(string str)
        {
            return (TEnum)(object)Array.IndexOf(_enumCorrespondenceArray, str);
        }

        /// <summary>
        /// Convert given Enum value to string.
        /// Only works with Enums what type is specified upon creation of the class.
        /// </summary>
        /// <param name="eValue">Value of a given Enum</param>
        /// <returns>String value API working with</returns>
        public string GetStringValueFromEnum(TEnum eValue)
        {
            return _enumCorrespondenceArray[(int)(object)eValue];
        }

        /// <summary>
        /// Constuctor for creation of Converter class. Each Enum should get its own converter object.
        /// </summary>
        public EnumStringConverter()
        {
            _enumCorrespondenceArray = GetEnumCorrespondenceArray<TEnum>();
        }

        /// <summary>
        /// Uses reflection to get Enum Description attributes array.
        /// </summary>
        /// <typeparam name="TEnum">Generic version of Enum</typeparam>
        /// <returns>Sting array</returns>
        private string[] GetEnumCorrespondenceArray<TEnum>() where TEnum : Enum
        {
            TEnum[] enumValues = Enum.GetValues(typeof(TEnum))
                                           .Cast<TEnum>()
                                           .ToArray();

            return enumValues.Select(val => GetDescription(val)).ToArray();
        }

        /// <summary>
        /// Gets Description attribute value from Enum.
        /// </summary>
        /// <typeparam name="TEnum">Generic version of Enum</typeparam>
        /// <param name="enumValue"></param>
        private string GetDescription(Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                               .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute?.Description ?? enumValue.ToString();
        }
    }
}
