using SangoCommon.ComBatCode;
using SangoCommon.StateCode;
using SangoCommon.WeaponCode;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerData
{
    public class WeaponInfo
    {
        //Value
        public int Id { get; set; }
        public WeaponNameCode WeaponName { get; set; }
        public WeaponTypeCode WeaponType { get; set; }
        public RarityCode Rarity { get; set; }
        public SkillCode Skill { get; set; }
        public int PhysicAttack { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
