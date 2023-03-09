using Common.ElementCode;
using PlayerData;

namespace PlayerScript
{
    public class PlayerAttributeAdd
    {
        public static AttributeInfo PackAttibuteInfo(int hp, int hpFull, int mp, int mpFull, int attack, int defence, ElementTypeCode type, int gauge)
        {
            var attributeInfoNew = new AttributeInfo
            {
                HP = hp,
                HPFull = hpFull,
                MP = mp,
                MPFull = mpFull,  
                Attack = attack,
                Defence = defence,
                ElementType = type,
                ElementGauge = gauge
            };
            return attributeInfoNew;
        }
    }
}
