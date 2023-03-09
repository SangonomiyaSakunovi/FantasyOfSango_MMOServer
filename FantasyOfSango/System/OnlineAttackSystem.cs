using Common.ComBatCode;
using Common.DataCache.AttackCache;
using Common.DataCache.PlayerDataCache;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;

namespace FantasyOfSango.System
{
    public class OnlineAttackSystem : BaseSystem
    {
        public static OnlineAttackSystem Instance = null;

        private int PerAttackExperience = 1;

        public override void InitSystem()
        {
            base.InitSystem();
            Instance = this;
        }

        public AttackResultCache GetAttackResult(AttackDamageCache attackDamageCache)
        {
            AttackResultCache attackResultCache;
            if (attackDamageCache.SkillCode == SkillCode.Attack)
            {
                attackResultCache = SetAttackDamage(attackDamageCache);
            }
            else
            {
                attackResultCache = null;
            }
            return attackResultCache;
        }

        private AttackResultCache SetAttackDamage(AttackDamageCache attackDamageCache)
        {
            PlayerCache attackerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.AttackerAccount);
            PlayerCache damagerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.DamagerAccount);
            int attackerAttack = attackerPlayerCache.WeaponInfoList[0].PhysicAttack;
            int attackDamage = attackerAttack;
            damagerPlayerCache.AttributeInfoList[0].HP -= attackDamage;
            AttackResultCache attackResultCache = new AttackResultCache
            {
                AttackerAccount = attackDamageCache.AttackerAccount,
                DamagerAccount = attackDamageCache.DamagerAccount,
                DamageNumber = attackDamage,
                AttackerPlayerCache = attackerPlayerCache,
                DamagerPlayerCache = damagerPlayerCache
            };
            OnlineAccountCache.Instance.UpdateOnlinePlayerCache(attackDamageCache.AttackerAccount, attackerPlayerCache, attackDamageCache.DamagerAccount, damagerPlayerCache);
            return attackResultCache;
        }

    }
}
