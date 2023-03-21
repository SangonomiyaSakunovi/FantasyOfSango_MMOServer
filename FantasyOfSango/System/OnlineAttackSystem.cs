using SangoCommon.ComBatCode;
using SangoCommon.DataCache.AttackCache;
using SangoCommon.DataCache.PlayerDataCache;
using SangoCommon.GameObjectCode;
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

        private AttackResultCache SetAttackDamage(AttackDamageCache attackDamageCache)
        {
            PlayerCache attackerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.AttackerAccount);
            PlayerCache damagerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.DamagerAccount);
            int attackerAttack = attackerPlayerCache.AttributeInfoList[attackerIndex].Attack;
            int damagerDefence = damagerPlayerCache.AttributeInfoList[damagerIndex].Defence;
            int attackDamage = attackerAttack - damagerDefence;
            damagerPlayerCache.AttributeInfoList[damagerIndex].HP -= attackDamage;
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

        private AttackResultCache SetElementAttackDamage(AttackDamageCache attackDamageCache)
        {
            PlayerCache attackerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.AttackerAccount);
            PlayerCache damagerPlayerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(attackDamageCache.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamageCache.DamagerAccount);
            //Exam if that damageRequest from Kokomi, she can give a healer
            if (attackerPlayerCache.AttributeInfoList[attackerIndex].Avater == AvaterCode.SangonomiyaKokomi)
            {
                int kokomiHealer = attackerPlayerCache.AttributeInfoList[attackerIndex].Attack * 2;
                damagerPlayerCache.AttributeInfoList[damagerIndex].HP += kokomiHealer;
                AttackResultCache attackResultCache = new AttackResultCache
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

        private AttackResultCache SetElementBurstDamage(AttackDamageCache attackDamageCache)
        {

            return null;
        }
    }
}
