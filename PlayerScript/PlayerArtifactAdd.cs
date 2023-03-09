using Common.ArtifactCode;
using Common.ComBatCode;
using Common.StateCode;
using Common.WeaponCode;
using PlayerData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerScript
{
    public class PlayerArtifactAdd
    {
        public static ArtifactInfo PackArtifactInfo(ArtifactNameCode artifactName, int attack)
        {
            var artifactInfoNew = new ArtifactInfo
            {
                ArtifactName = artifactName,
                Attack = attack,
            };
            return artifactInfoNew;
        }
    }
}
