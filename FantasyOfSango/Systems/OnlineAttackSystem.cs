using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using SangoCommon.Classs;
using SangoCommon.Enums;

namespace FantasyOfSango.Systems
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

        public AttackResult GetAttackResult(AttackDamage attackDamageCache)
        {
            AttackResult attackResultCache;
            if (attackDamageCache.SkillCode == SkillCode.Attack)
            {
                attackResultCache = SetAttackDamage(attackDamageCache);
            }
            else if (attackDamageCache.SkillCode == SkillCode.ElementAttack)
            {
                attackResultCache = SetElementAttackDamage(attackDamageCache);
            }
            else if (attackDamageCache.SkillCode == SkillCode.ElementBurst)
            {
                attackResultCache = SetElementBurstDamage(attackDamageCache);
            }
            else
            {
                attackResultCache = null;
            }
            return attackResultCache;
        }

        private AttackResult SetAttackDamage(AttackDamage attackDamageCache)
        {
            AvaterInfo attackerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.AttackerAccount);
            AvaterInfo damagerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.DamagerAccount);
            int attackerAttack = attackerPlayerCache.AttributeInfoList[attackerIndex].Attack;
            int damagerDefence = damagerPlayerCache.AttributeInfoList[damagerIndex].Defence;
            int attackDamage = attackerAttack - damagerDefence;
            damagerPlayerCache.AttributeInfoList[damagerIndex].HP -= attackDamage;
            AttackResult attackResultCache = new AttackResult
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

        private AttackResult SetElementAttackDamage(AttackDamage attackDamageCache)
        {
            AvaterInfo attackerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.AttackerAccount);
            AvaterInfo damagerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.DamagerAccount);
            //Exam if that damageRequest from Kokomi, she can give a healer
            if (attackerPlayerCache.AttributeInfoList[attackerIndex].Avater == AvaterCode.SangonomiyaKokomi)
            {
                int kokomiHealer = attackerPlayerCache.AttributeInfoList[attackerIndex].Attack * 2;
                damagerPlayerCache.AttributeInfoList[damagerIndex].HP += kokomiHealer;
                AttackResult attackResultCache = new AttackResult
                {
                    AttackerAccount = attackDamageCache.AttackerAccount,
                    DamagerAccount = attackDamageCache.DamagerAccount,
                    DamageNumber = -kokomiHealer,
                    AttackerPlayerCache = attackerPlayerCache,
                    DamagerPlayerCache = damagerPlayerCache
                };
                OnlineAccountCache.Instance.UpdateOnlinePlayerCache(attackDamageCache.AttackerAccount, attackerPlayerCache, attackDamageCache.DamagerAccount, damagerPlayerCache);
                return attackResultCache;
            }
            return null;
        }

        private AttackResult SetElementBurstDamage(AttackDamage attackDamageCache)
        {

            return null;
        }
    }
}
