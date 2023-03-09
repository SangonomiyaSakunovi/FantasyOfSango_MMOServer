using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerData
{
    public class Player
    {
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Nickname { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<AttributeInfo> AttributeInfoList { get; set; }
        public List<WeaponInfo> WeaponInfoList { get; set; }
        public List<ArtifactInfo> ArtifactInfoList { get; set; }
    }
}
