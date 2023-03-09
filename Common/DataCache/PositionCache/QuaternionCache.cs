using System;

namespace Common.DataCache.PositionCache
{
    [Serializable]
    public class QuaternionCache
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
    }
}
