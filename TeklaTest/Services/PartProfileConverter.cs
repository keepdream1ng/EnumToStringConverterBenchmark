using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekla.Extension.Services
{
    /// <summary>
    /// Template attribute PROFILE_TYPE string values converter to specific Enum as per https://support.tekla.com/doc/tekla-structures/2023/profile_type.
    /// </summary>
    public static class PartProfileConverter
    {
        /// <summary>
        /// Storage for hardcoded enum string values.
        /// </summary>
        private static string[] _enumCorrespondenceArray = new string[]
        {
        "B",
        "I",
        "L",
        "U",
        "RU",
        "RO",
        "M",
        "C",
        "T",
        "Z"
        };

        /// <summary>
        /// Converts string to ProfileType.
        /// </summary>
        /// <param name="str">String value API is working with.</param>
        /// <returns>May return -1 if the string is not supported in the ProfileType Enum</returns>
        public static ProfileType GetProfileTypeFromString(string str)
        {
            return (ProfileType)Array.IndexOf(_enumCorrespondenceArray, str);
        }

        /// <summary>
        /// Converts ProfileType back to string.
        /// </summary>
        /// <param name="profileType"></param>
        /// <returns>String corresponding to Enum</returns>
        public static string GetStringValueFromProfileType(ProfileType profileType)
        {
            return _enumCorrespondenceArray[(int)profileType];
        }
    }
}
