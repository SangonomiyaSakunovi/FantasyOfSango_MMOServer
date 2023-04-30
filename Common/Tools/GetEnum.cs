using System;

//Developer : SangonomiyaSakunovi
//Discription:

namespace SangoCommon.Tools
{
    public class GetEnum
    {
        /// <summary>
        /// Get Enums, you need specify the EnumType and String, the return is [Enums]
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Enum GetEnumByString(Type enumType, string value)
        {
            return Enum.Parse(enumType, value) as Enum;
        }
    }
}
