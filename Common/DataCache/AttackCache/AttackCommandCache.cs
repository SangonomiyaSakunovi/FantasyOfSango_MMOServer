using SangoCommon.ComBatCode;
using static SangoCommon.Struct.CommonStruct;

namespace SangoCommon.DataCache.AttackCache
{
    public class AttackCommandCache
    {
        public string Account { get; set; }
        public SkillCode SkillCode { get; set; }
        public Vector3Position Vector3Position { get; set; }
        public QuaternionRotation QuaternionRotation { get; set; }
    }
}
