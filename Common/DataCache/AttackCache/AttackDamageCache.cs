using SangoCommon.ComBatCode;
using SangoCommon.DataCache.PositionCache;
using SangoCommon.ElementCode;
using System;

namespace SangoCommon.DataCache.AttackCache
{
    public class AttackDamageCache
    {
        public FightTypeCode FightTypeCode { get; set; }
        public string AttackerAccount { get; set; }
        public string DamagerAccount { get; set; }
        public SkillCode SkillCode { get; set; }
        public ElementReactionCode ElementReactionCode { get; set; }
        public Vector3Cache AttackerPosition { get; set; }
        public Vector3Cache DamagerPosition { get; set; }
        public DateTime DateTime { get; set; }
    }
}
