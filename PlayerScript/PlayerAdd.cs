using Common.ArtifactCode;
using Common.ComBatCode;
using Common.ElementCode;
using Common.GameObjectCode;
using Common.StateCode;
using Common.WeaponCode;
using PlayerData;
using PlayerMigration;
using System.Collections.Generic;
using System.Linq;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class PlayerAdd
    {
        public static int AddUserInfo(string nickname, string account, string password)
        {
            using (PlayerContext context = new PlayerContext())
            {
                int changeLineNum = 0;
                bool isHasUserInfo = false;
                //Exam if there already has this username
                UserInfo storedUserInfo = context.UserInfos.SingleOrDefault(x => x.Account == account);
                if (storedUserInfo != null)
                {
                    isHasUserInfo = true;
                }
                if (isHasUserInfo)
                {
                    return changeLineNum;
                }
                else
                {
                    //Add UserInfo into DbSet                
                    var userInfoNew = new UserInfo
                    {
                        Account = account,
                        Password = password
                    };
                    var attributeInfoNew1 = PlayerAttributeAdd.PackAttibuteInfo(AvaterCode.SangonomiyaKokomi, 100, 100, 100, 100, 2, 0, ElementTypeCode.Hydro, 2);
                    var attributeInfoNew2 = PlayerAttributeAdd.PackAttibuteInfo(AvaterCode.Yoimiya, 90, 100, 50, 100, 2, 0, ElementTypeCode.Pyro, 2);
                    var attributeInfoNew3 = PlayerAttributeAdd.PackAttibuteInfo(AvaterCode.Ganyu, 80, 100, 40, 100, 2, 0, ElementTypeCode.Cryo, 2);
                    var attributeInfoNew4 = PlayerAttributeAdd.PackAttibuteInfo(AvaterCode.Aether, 70, 100, 30, 100, 2, 0, ElementTypeCode.Electro, 2);
                    var weaponInfoNew1 = WeaponAdd.PackWeaponInfo(WeaponNameCode.Default, WeaponTypeCode.Default,
                        RarityCode.Default, SkillCode.Default, 2, 0, 0);
                    var weaponInfoNew2 = WeaponAdd.PackWeaponInfo(WeaponNameCode.Default, WeaponTypeCode.Default,
                        RarityCode.Default, SkillCode.Default, 2, 0, 0);
                    var weaponInfoNew3 = WeaponAdd.PackWeaponInfo(WeaponNameCode.Default, WeaponTypeCode.Default,
                        RarityCode.Default, SkillCode.Default, 2, 0, 0);
                    var weaponInfoNew4 = WeaponAdd.PackWeaponInfo(WeaponNameCode.Default, WeaponTypeCode.Default,
                        RarityCode.Default, SkillCode.Default, 2, 0, 0);
                    var artifactInfoNew1 = PlayerArtifactAdd.PackArtifactInfo(ArtifactNameCode.Default, 0);
                    var artifactInfoNew2 = PlayerArtifactAdd.PackArtifactInfo(ArtifactNameCode.Default, 0);
                    var artifactInfoNew3 = PlayerArtifactAdd.PackArtifactInfo(ArtifactNameCode.Default, 0);
                    var artifactInfoNew4 = PlayerArtifactAdd.PackArtifactInfo(ArtifactNameCode.Default, 0);
                    var PlayerNew = new Player
                    {
                        Nickname = nickname,
                        UserInfo = userInfoNew,
                        AttributeInfoList = new List<AttributeInfo> { attributeInfoNew1, attributeInfoNew2, attributeInfoNew3, attributeInfoNew4 },
                        WeaponInfoList = new List<WeaponInfo> { weaponInfoNew1, weaponInfoNew2, weaponInfoNew3, weaponInfoNew4 },
                        ArtifactInfoList = new List<ArtifactInfo> { artifactInfoNew1, artifactInfoNew2, artifactInfoNew3, artifactInfoNew4 },
                    };
                    context.Players.Add(PlayerNew);
                    return changeLineNum = context.SaveChanges();
                }
            }
        }
    }
}
