using Common.DataCache.AttackCache;
using FantasyOfSango.Base;
using System.Collections.Generic;

namespace FantasyOfSango.Cache
{
    public class OnlineAttackCache : BaseCache
    {
        public static OnlineAttackCache Instance = null;

        private List<AttackDamageCache> OnlineAttackDamageCacheList = new List<AttackDamageCache>();

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
        }

        public void AddOnlineAttackDamageCache(AttackDamageCache attackDamageCache)
        {
            OnlineAttackDamageCacheList.Add(attackDamageCache);
        }
    }
}
