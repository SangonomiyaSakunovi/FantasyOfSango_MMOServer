using PlayerData;
using PlayerMigration;
using System.Linq;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class PlayerLookUp
    {
        //Exam if the UserInfo is Valid
        public static Player PlayerDataLookUp(string account, string password)
        {
            using (PlayerContext context = new PlayerContext())
            {
                UserInfo storedUserInfo = context.UserInfos.SingleOrDefault(x => x.Account == account);
                if (storedUserInfo != null)
                {
                    if (storedUserInfo.Account == account && storedUserInfo.Password == password)
                    {
                        Player playerNew = new Player();

                        var player = context.Players
                            .Where(x => x.UserInfo == storedUserInfo)
                            .ToList();
                        playerNew.Nickname = player[0].Nickname;

                        var attributeinfoList = context.AttributeInfos
                            .Where(z => z.PlayerId == player[0].Id)
                            .ToList();
                        playerNew.AttributeInfoList = attributeinfoList;

                        var weaponinfoList = context.WeaponInfos
                            .Where(y => y.PlayerId == player[0].Id)
                            .ToList();
                        playerNew.WeaponInfoList = weaponinfoList;

                        var artifactinfoList = context.ArtifactInfos
                            .Where(z => z.PlayerId == player[0].Id)
                            .ToList();
                        playerNew.ArtifactInfoList = artifactinfoList;
                        return playerNew;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
