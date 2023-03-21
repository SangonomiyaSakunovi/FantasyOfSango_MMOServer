using SangoCommon.Constant;
using SangoCommon.ElementCode;

namespace SangoCommon.DataCache.ElementCache
{
    public class ElementApplicationCache
    {
        public ElementTypeCode Type { get; set; }
        public float Gauge { get; set; }
        public float DecayRate { get; set; }
        public float RemainTime { get; set; }

        public ElementApplicationCache(ElementTypeCode element, float gauge)
        {
            Type = element;
            Gauge = gauge;
            RemainTime = ElementConstant.ElementRemainTimeBase + ElementConstant.ElementRemainTimePlus * Gauge;
            DecayRate = ElementConstant.ElementRemainCount * Gauge / RemainTime;
        }
    }
}
