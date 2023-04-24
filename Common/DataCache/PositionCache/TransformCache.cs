using static SangoCommon.Struct.CommonStruct;
namespace SangoCommon.DataCache.PositionCache
{
    public class TransformCache
    {
        public string Account { get; set; }
        public Vector3Position Vector3Position { get; set; }
        public QuaternionRotation QuaternionRotation { get; set; }
    }
}
