using System;

namespace Common.DataCache.PlayerDataCache
{
    [Serializable]
    public class AttributeInfoCache
    {
        public int HP { get; set; }
        public int HPFull { get; set; }
        public int MP { get; set; }
        public int MPFull { get; set; }        
    }
}
