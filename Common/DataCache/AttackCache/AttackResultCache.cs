using Common.DataCache.PlayerDataCache;

namespace Common.DataCache.AttackCache
{
    public class AttackResultCache
    {
        public string AttackerAccount { get; set; }
        public string DamagerAccount { get; set; }
        public int DamageNumber { get; set; }
        public PlayerCache AttackerPlayerCache { get; set; }
        public PlayerCache DamagerPlayerCache { get; set; }
    }
}
