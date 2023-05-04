﻿using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.FSMs;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Structs;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription: 

namespace FantasyOfSango.Systems
{
    public class NPCSystem : BaseSystem
    {
        public static NPCSystem Instance;

        public override void InitSystem()
        {
            base.InitSystem();
            Instance = this;
            InitNPCGameObject();
        }

        private void InitNPCGameObject()
        {
            InitEnemyGameObject();
        }

        private void InitEnemyGameObject()
        {
            NPCGameObject testHilichurlGameObject = new NPCGameObject
            {
                NPCCode = NPCCode.Hilichurl_Island_A_01,
                Vector3Position = new Vector3Position(-69.08f, 10.39f, 138.39f),
                QuaternionRotation = new QuaternionRotation(0, 0, 0, 0),
                NPCAttributeInfo = new NPCAttributeInfo
                {
                    HP = 10,
                    HPFull = 10,
                    Attack = 1,
                    Defence = 1,
                    ElementType = ElementTypeCode.Null,
                    ElementGauge = 0
                },
                AOISceneGrid = new AOISceneGrid(SceneCode.Island, (int)(-69.08f + 700) / 100, (int)(138.39f + 400) / 100)
            };
            OnlineNPCCache.Instance.AddNPCGameObject(NPCCode.Hilichurl_Island_A_01, testHilichurlGameObject);
            OnlineNPCCache.Instance.AddOrUpdateAOINPCGameObjectDict(NPCCode.Hilichurl_Island_A_01, testHilichurlGameObject.AOISceneGrid);
        }

        private void InitEnemyFSMSystem()
        {
            FSMSystem testHilichurlFSMSystem = new FSMSystem(NPCCode.Hilichurl_Island_A_01);
            OnlineNPCCache.Instance.AddNPCFSMSystem(NPCCode.Hilichurl_Island_A_01, testHilichurlFSMSystem);
        }

        public ClientPeer FindOnlineClientPeerAsTarget(NPCCode npcCode, float patrolToChaseDis)
        {
            NPCGameObject npc = OnlineNPCCache.Instance.GetNPCGameObject(npcCode);
            AOISceneGrid aoiSceneGrid = npc.AOISceneGrid;
            Vector3Position npcPos = npc.Vector3Position;
            List<string> accountString = OnlineAccountCache.Instance.GetCurrentAOIAccount(aoiSceneGrid);
            for (int i = 0; i < accountString.Count; i++)
            {
                ClientPeer tempPeer = OnlineAccountCache.Instance.GetOnlinePlayerPeer(accountString[i]);
                float tempDis = Vector3Position.Distance(npcPos, tempPeer.TransformOnline.Vector3Position);
                if (tempDis < patrolToChaseDis)
                {
                    return tempPeer;
                }
            }
            return null;
        }

        public bool IsLostOnlineClientPeerTarget(NPCCode npcCode, ClientPeer targetPeer, float chaseToPatrolDis)
        {
            Vector3Position npcPos = OnlineNPCCache.Instance.GetNPCGameObject(npcCode).Vector3Position;
            float tempDis = Vector3Position.Distance(npcPos, targetPeer.TransformOnline.Vector3Position);
            if (tempDis > chaseToPatrolDis)
            {
                return true;
            }
            return false;
        }

        public bool IsApproachOnlineClientPeerTarget(NPCCode npcCode, ClientPeer targetPeer, float chaseToAttackDis)
        {
            Vector3Position npcPos = OnlineNPCCache.Instance.GetNPCGameObject(npcCode).Vector3Position;
            float tempDis = Vector3Position.Distance(npcPos, targetPeer.TransformOnline.Vector3Position);
            if (tempDis < chaseToAttackDis)
            {
                return true;
            }
            return false;
        }

        public bool IsAwayOnlienClientPeerTarget(NPCCode npcCode, ClientPeer targetPeer, float attackToChaseDis)
        {
            Vector3Position npcPos = OnlineNPCCache.Instance.GetNPCGameObject(npcCode).Vector3Position;
            float tempDis = Vector3Position.Distance(npcPos, targetPeer.TransformOnline.Vector3Position);
            if (tempDis > attackToChaseDis)
            {
                return true;
            }
            return false;
        }
    }
}