using Common.ComBatCode;
using Common.DataCache.PositionCache;
using System;

namespace Common.DataCache.AttackCache
{
    public class AttackDamageCache
    {
        public string AttackerAccount { get; set; }
        public string DamagerAccount { get; set; }
        public SkillCode SkillCode { get; set; }
        public Vector3Cache AttackerPosition { get; set; }
        public Vector3Cache DamagerPosition { get; set; }
        public DateTime DateTime { get; set; }
    }
}
