using PlayerData;

namespace PlayerScript
{
    public class PlayerAttributeAdd
    {
        public static AttributeInfo PackAttibuteInfo(int hp, int hpFull, int mp, int mpFull)
        {
            var attributeInfoNew = new AttributeInfo
            {
                HP = hp,
                HPFull = hpFull,
                MP = mp,
                MPFull = mpFull,                
            };
            return attributeInfoNew;
        }
    }
}
