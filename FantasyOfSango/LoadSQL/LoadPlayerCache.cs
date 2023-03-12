using Common.DataCache.PlayerDataCache;
using PlayerData;
using System.Collections.Generic;

namespace FantasyOfSango.LoadSQL
{
    public class LoadPlayerCache
    {
        public static PlayerCache LoadPlayer(string account, Player player)
        {
            List<AttributeInfoCache> tempAttributeInfoList = new List<AttributeInfoCache>();
            List<WeaponInfoCache> tempWeaponInfoList = new List<WeaponInfoCache>();
            List<ArtifactInfoCache> tempArtifactInfoList = new List<ArtifactInfoCache>();

            foreach (var item in player.AttributeInfoList)
            {
                AttributeInfoCache temp = new AttributeInfoCache
                {
                    Avater = item.Avater,
                    HP = item.HP,
                    HPFull = item.HPFull,
                    MP = item.MP,
                    MPFull = item.MP,
                    Attack = item.Attack,
                    Defence = item.Defence,
                    ElementType = item.ElementType,
                    ElementGauge = item.ElementGauge,
                };
                tempAttributeInfoList.Add(temp);
            }

            foreach (var item in player.WeaponInfoList)
            {
                WeaponInfoCache temp = new WeaponInfoCache
                {
                    WeaponName = item.WeaponName,
                    WeaponType = item.WeaponType,
                    Rarity = item.Rarity,
                    Skill = item.Skill,
                    PhysicAttack = item.PhysicAttack,
                    Level = item.Level,
                    Experience = item.Experience,
                };
                tempWeaponInfoList.Add(temp);
            }

            foreach (var item in player.ArtifactInfoList)
            {
                ArtifactInfoCache temp = new ArtifactInfoCache
                {
                    ArtifactName = item.ArtifactName,
                    Attack = item.Attack,
                };
                tempArtifactInfoList.Add(temp);
            }

            PlayerCache playerCache = new PlayerCache
            {
                Account = account,
                Nickname = player.Nickname,
                AttributeInfoList = tempAttributeInfoList,
                WeaponInfoList = tempWeaponInfoList,
                ArtifactInfoList = tempArtifactInfoList
            };
            return playerCache;
        }
    }
}
