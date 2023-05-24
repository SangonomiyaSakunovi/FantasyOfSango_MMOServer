using FantasyOfSango.Bases;
using FantasyOfSango.FSMs;
using FantasyOfSango.Systems;
using SangoCommon.AI;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Structs;
using SangoCommon.Tools;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Caches
{
    public class OnlineNPCCache : BaseCache
    {
        public static OnlineNPCCache Instance = null;

        private Dictionary<AOISceneGrid, List<NPCCode>> AOINPCGameObjectDict = new Dictionary<AOISceneGrid, List<NPCCode>>();
        private Dictionary<NPCCode, NPCGameObject> NPCGameObjectDict = new Dictionary<NPCCode, NPCGameObject>();
        private Dictionary<NPCCode, FSMSystem> NPCFSMSystemDict = new Dictionary<NPCCode, FSMSystem>();
        private Dictionary<NPCCode, AIBase> NPCAISystemDict = new Dictionary<NPCCode, AIBase>();

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
        }

        public void AddOrUpdateAOINPCGameObjectDict(NPCCode npcCode, AOISceneGrid aoiSceneGrid, string id = "Test")
        {
            lock (id)
            {
                if (AOINPCGameObjectDict.ContainsKey(aoiSceneGrid))
                {
                    AOINPCGameObjectDict[aoiSceneGrid].Add(npcCode);
                }
                else
                {
                    AOINPCGameObjectDict.Add(aoiSceneGrid, new List<NPCCode> { npcCode });
                }
            }
        }

        public List<NPCCode> GetSurroundAOINPCGameObject(AOISceneGrid aoiSceneGrid, string id = "Test")
        {
            lock (id)
            {
                List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(aoiSceneGrid);
                List<NPCCode> aoiNPCList = new List<NPCCode>();
                List<NPCCode> tempList1 = DictTools.GetDictValue<AOISceneGrid, List<NPCCode>>(AOINPCGameObjectDict, aoiSceneGrid);

                if (tempList1 != null)
                {
                    for (int i = 0; i < tempList1.Count; i++)
                    {
                        aoiNPCList.Add(tempList1[i]);
                    }
                }
                for (int j = 0; j < surroundAOIGridList.Count; j++)
                {
                    if (AOINPCGameObjectDict.ContainsKey(surroundAOIGridList[j]))
                    {
                        List<NPCCode> tempList2 = DictTools.GetDictValue<AOISceneGrid, List<NPCCode>>(AOINPCGameObjectDict, surroundAOIGridList[j]);
                        if (tempList2 != null)
                        {
                            for (int k = 0; k < tempList2.Count; k++)
                            {
                                aoiNPCList.Add(tempList2[k]);
                            }
                        }
                    }
                }
                return aoiNPCList;
            }
        }

        public void AddNPCAISystem<T>(NPCCode npcCode, AIBase ai) where T : AIBase
        {
            if (ai is T subAI)
                NPCAISystemDict.Add(npcCode, subAI);
        }

        public AIBase GetNPCAI(NPCCode npcCode)
        {
            return DictTools.GetDictValue<NPCCode, AIBase>(NPCAISystemDict, npcCode);
        }

        public void AddNPCGameObject(NPCCode npcCode, NPCGameObject npcGameObject)
        {
            NPCGameObjectDict.Add(npcCode, npcGameObject);
        }

        public NPCGameObject GetNPCGameObject(NPCCode npcCode)
        {
            return DictTools.GetDictValue<NPCCode, NPCGameObject>(NPCGameObjectDict, npcCode);
        }

        public void AddNPCFSMSystem(NPCCode npcCode, FSMSystem fSMSystem)
        {
            NPCFSMSystemDict.Add(npcCode, fSMSystem);
        }

        public FSMSystem GetNPCFSMSystem(NPCCode npcCode)
        {
            return DictTools.GetDictValue<NPCCode, FSMSystem>(NPCFSMSystemDict, npcCode);
        }

        public IEnumerable<AIBase> GetNPCAIs() => NPCAISystemDict.Values;
    }
}
