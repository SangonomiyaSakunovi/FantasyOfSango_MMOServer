using SangoCommon.Classs;
using FantasyOfSango.Bases;
using System.Collections.Generic;

namespace FantasyOfSango.Caches
{
    public class OnlineAttackCache : BaseCache
    {
        public static OnlineAttackCache Instance = null;

        private List<AttackDamage> OnlineAttackDamageCacheList = new List<AttackDamage>();

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
        }

        public void AddOnlineAttackDamageCache(AttackDamage attackDamageCache)
        {
            OnlineAttackDamageCacheList.Add(attackDamageCache);
        }
    }
}
