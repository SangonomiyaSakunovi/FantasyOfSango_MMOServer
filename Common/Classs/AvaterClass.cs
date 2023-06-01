using SangoCommon.Enums;
using System;

//Developer : SangonomiyaSakunovi
//Discription:

namespace SangoCommon.Classs
{
    [Serializable]
    public class AvaterAttributeInfo
    {
        public AvaterCode Avater { get; set; }
        public int HP { get; set; }
        public int HPFull { get; set; }
        public int MP { get; set; }
        public int MPFull { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementGauge { get; set; }
        public string WeaponId { get; set; }
        public int WeaponExp { get; set; }
    }
}
