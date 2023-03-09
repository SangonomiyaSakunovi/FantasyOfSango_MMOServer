using System.Collections.Generic;

namespace Common.DataCache.PlayerDataCache
{
    public class PlayerCache
    {
        public string Account { get; set; }
        public string Nickname { get; set; }
        public List<AttributeInfoCache> AttributeInfoList { get; set; }
        public List<WeaponInfoCache> WeaponInfoList { get; set; }
        public List<ArtifactInfoCache> ArtifactInfoList { get; set; }
    }
}
