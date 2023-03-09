using Common.ComBatCode;
using Common.StateCode;
using Common.WeaponCode;
using PlayerData;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class WeaponAdd
    {
        public static WeaponInfo PackWeaponInfo(WeaponNameCode weaponName,
            WeaponTypeCode weaponTypeCode, RarityCode rarity, SkillCode skill, int physicAttack,
            int level, int experience)
        {
            var weaponInfoNew = new WeaponInfo
            {
                WeaponName = weaponName,
                WeaponType = weaponTypeCode,
                Rarity = rarity,
                Skill = skill,
                PhysicAttack = physicAttack,
                Level = level,
                Experience = experience,
            };
            return weaponInfoNew;
        }
    }
}
