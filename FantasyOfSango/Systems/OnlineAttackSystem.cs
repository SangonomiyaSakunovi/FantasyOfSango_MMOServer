using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using SangoCommon.AI;
using SangoCommon.Classs;
using SangoCommon.Enums;
using System;
using System.Diagnostics;

//Developer : SangonomiyaSakunovi
//Discription:

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

        public AttackResult GetAttackResult(AttackDamage attackDamage)
        {
            AttackResult attackResult;
            if (attackDamage.FightTypeCode == FightTypeCode.PVP)
            {
                if (attackDamage.SkillCode == SkillCode.Attack)
                {
                    attackResult = SetAttackDamage(attackDamage);
                }
                else if (attackDamage.SkillCode == SkillCode.ElementAttack)
                {
                    attackResult = SetElementAttackDamage(attackDamage);
                }
                else if (attackDamage.SkillCode == SkillCode.ElementBurst)
                {
                    attackResult = SetElementBurstDamage(attackDamage);
                }
                else
                {
                    attackResult = null;
                }
            }
            if(attackDamage.FightTypeCode == FightTypeCode.PVE)
            {
                SangoServer.Log.Info("PVE mode");
                if(attackDamage.SkillCode == SkillCode.ElementAttack)
                {
                    attackResult = SetElementAttackDamagePVE(attackDamage);
                    return attackResult;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        private AttackResult SetAttackDamage(AttackDamage attackDamage)
        {
            AvaterInfo attackerAvaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(attackDamage.AttackerAccount);
            AvaterInfo damagerAvaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(attackDamage.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamage.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamage.DamagerAccount);
            int attackerAttack = attackerAvaterInfo.AttributeInfoList[attackerIndex].Attack;
            int damagerDefence = damagerAvaterInfo.AttributeInfoList[damagerIndex].Defence;
            int attackDamageNum = attackerAttack - damagerDefence;
            damagerAvaterInfo.AttributeInfoList[damagerIndex].HP -= attackDamageNum;
            AttackResult attackResult = new AttackResult
            {
                AttackerAccount = attackDamage.AttackerAccount,
                DamagerAccount = attackDamage.DamagerAccount,
                DamageNumber = attackDamageNum,
                AttackerAvaterInfo = attackerAvaterInfo,
                DamagerAvaterInfo = damagerAvaterInfo
            };
            OnlineAccountCache.Instance.UpdateOnlineAvaterInfo(attackDamage.AttackerAccount, attackerAvaterInfo, attackDamage.DamagerAccount, damagerAvaterInfo);
            return attackResult;
        }

        private AttackResult SetElementAttackDamage(AttackDamage attackDamage)
        {
            AvaterInfo attackerAvaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(attackDamage.AttackerAccount);
            AvaterInfo damagerAvaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(attackDamage.DamagerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamage.AttackerAccount);
            int damagerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamage.DamagerAccount);
            //Exam if that damageRequest from Kokomi, she can give a healer
            if (attackerAvaterInfo.AttributeInfoList[attackerIndex].Avater == AvaterCode.SangonomiyaKokomi)
            {
                int kokomiHealer = attackerAvaterInfo.AttributeInfoList[attackerIndex].Attack * 2;
                damagerAvaterInfo.AttributeInfoList[damagerIndex].HP += kokomiHealer;
                AttackResult attackResultCache = new AttackResult
                {
                    AttackerAccount = attackDamage.AttackerAccount,
                    DamagerAccount = attackDamage.DamagerAccount,
                    DamageNumber = -kokomiHealer,
                    AttackerAvaterInfo = attackerAvaterInfo,
                    DamagerAvaterInfo = damagerAvaterInfo
                };
                OnlineAccountCache.Instance.UpdateOnlineAvaterInfo(attackDamage.AttackerAccount, attackerAvaterInfo, attackDamage.DamagerAccount, damagerAvaterInfo);
                return attackResultCache;
            }
            return null;
        }

        private AttackResult SetElementBurstDamage(AttackDamage attackDamage)
        {
            return null;
        }

        private AttackResult SetElementAttackDamagePVE(AttackDamage attackDamage)
        {
            AvaterInfo attackerAvaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(attackDamage.AttackerAccount);
            int attackerIndex = OnlineAccountCache.Instance.GetOnlineAvaterIndex(attackDamage.AttackerAccount);

            NPCCode code = (NPCCode)Enum.Parse(typeof(NPCCode), attackDamage.DamagerAccount);
            AIBase ai = OnlineNPCCache.Instance.GetNPCAI(code);

            ai.SetDamaged(attackerAvaterInfo.AttributeInfoList[attackerIndex].Avater, SkillCode.ElementAttack, attackDamage.AttackerVector3Position);
            //Exam if that damageRequest from Kokomi, she can give a damage the same as healer amount
            if (attackerAvaterInfo.AttributeInfoList[attackerIndex].Avater == AvaterCode.SangonomiyaKokomi)
            {
                int kokomiHealer = attackerAvaterInfo.AttributeInfoList[attackerIndex].Attack * 2;
                ai.GetNPCGameObjectInfo().NPCAttributeInfo.HP -= kokomiHealer;
                AttackResult attackResultCache = new AttackResult
                {
                    AttackerAccount = attackDamage.AttackerAccount,
                    DamagerAccount = attackDamage.DamagerAccount,
                    DamageNumber = kokomiHealer,
                    AttackerAvaterInfo = attackerAvaterInfo,
                };
                //OnlineAccountCache.Instance.UpdateOnlineAvaterInfo(attackDamage.AttackerAccount, attackerAvaterInfo, attackDamage.DamagerAccount, damagerAvaterInfo);
                //TODO: 更新玩家状态
                return attackResultCache;
            }
            return null;
        }
    }
}
