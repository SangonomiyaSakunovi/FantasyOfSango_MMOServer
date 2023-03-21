using SangoCommon.ArtifactCode;
using SangoCommon.ComBatCode;
using SangoCommon.StateCode;
using SangoCommon.WeaponCode;
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
