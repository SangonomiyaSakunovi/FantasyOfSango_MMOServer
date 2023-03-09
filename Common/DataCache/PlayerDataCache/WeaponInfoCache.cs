using Common.ComBatCode;
using Common.StateCode;
using Common.WeaponCode;
using System;

namespace Common.DataCache.PlayerDataCache
{
    [Serializable]
    public class WeaponInfoCache
    {
        public WeaponNameCode WeaponName { get; set; }
        public WeaponTypeCode WeaponType { get; set; }
        public RarityCode Rarity { get; set; }
        public SkillCode Skill { get; set; }
        public int PhysicAttack { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
    }
}
