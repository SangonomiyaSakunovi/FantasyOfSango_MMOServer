using SangoCommon.ComBatCode;
using SangoCommon.ElementCode;
using System;
using static SangoCommon.Struct.CommonStruct;

namespace SangoCommon.DataCache.AttackCache
{
    public class AttackDamageCache
    {
        public FightTypeCode FightTypeCode { get; set; }
        public string AttackerAccount { get; set; }
        public string DamagerAccount { get; set; }
        public SkillCode SkillCode { get; set; }
        public ElementReactionCode ElementReactionCode { get; set; }
        public Vector3Position AttackerVector3Position { get; set; }
        public Vector3Position DamagerVector3Position { get; set; }
        public DateTime DateTime { get; set; }
    }
}
