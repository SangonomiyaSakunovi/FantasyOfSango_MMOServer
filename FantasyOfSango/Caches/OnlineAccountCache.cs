using FantasyOfSango.Bases;
using FantasyOfSango.Systems;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
using System.Collections.Generic;
using SangoCommon.Structs;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Caches
{
    public class OnlineAccountCache : BaseCache
    {
        public static OnlineAccountCache Instance = null;

        private Dictionary<AOISceneGrid, List<string>> AOIAccountDict = new Dictionary<AOISceneGrid, List<string>>();
        private Dictionary<string, ClientPeer> OnlineAccountDict = new Dictionary<string, ClientPeer>();
        private Dictionary<ClientPeer, TransformOnline> OnlinePlayerTransformDict = new Dictionary<ClientPeer, TransformOnline>();

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
        }

        public List<string> GetSurroundAOIAccount(AOISceneGrid aoiSceneGrid, string account = "Test")
        {
            lock (account)
            {
                List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(aoiSceneGrid);
                List<string> aoiAccountList = new List<string>();
                List<string> tempList1 = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, aoiSceneGrid);

                if (tempList1 != null)
                {
                    for (int i = 0; i < tempList1.Count; i++)
                    {
                        aoiAccountList.Add(tempList1[i]);
                    }
                }
                for (int j = 0; j < surroundAOIGridList.Count; j++)
                {
                    if (AOIAccountDict.ContainsKey(surroundAOIGridList[j]))
                    {
                        List<string> tempList2 = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, surroundAOIGridList[j]);
                        if (tempList2 != null)
                        {
                            for (int k = 0; k < tempList2.Count; k++)
                            {
                                aoiAccountList.Add(tempList2[k]);
                            }
                        }
                    }
                }
                return aoiAccountList;
            }
        }

        public TransformOnline GetAccountTransfrom(string account)
        {
            ClientPeer peer = DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account);
            TransformOnline transform = null;
            if (peer != null)
            {
                transform = DictTools.GetDictValue<ClientPeer, TransformOnline>(OnlinePlayerTransformDict, peer);
            }
            return transform;
        }

        public void InitOnlineAccountAOIInfo(string account, SceneCode sceneCode, float x, float z)
        {
            AOISceneGrid aoiSceneGrid = AOISystem.Instance.SetAOIGrid(sceneCode, x, z);
            if (OnlineAccountDict.ContainsKey(account))
            {
                SetOnlineAccountAOISceneGrid(account, aoiSceneGrid);
                AddOrUpdateAOIAccountDict(account, aoiSceneGrid);
            }
        }

        public void UpdateOnlineAccountAOIInfo(string account, SceneCode sceneCode, float x, float z)
        {
            lock (account)
            {
                AOISceneGrid aoiSceneGridCurrent = DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).AOISceneGrid;
                AOISceneGrid aoiSceneGridTemp = AOISystem.Instance.SetAOIGrid(sceneCode, x, z);
                if (aoiSceneGridTemp != aoiSceneGridCurrent)
                {
                    SetOnlineAccountAOISceneGrid(account, aoiSceneGridTemp);
                    AddOrUpdateAOIAccountDict(account, aoiSceneGridTemp);
                    RemoveAOIAccountDict(account, aoiSceneGridCurrent);
                }
            }
        }

        private void AddOrUpdateAOIAccountDict(string account, AOISceneGrid aoiSceneGrid)
        {
            lock (account)
            {
                if (AOIAccountDict.ContainsKey(aoiSceneGrid))
                {
                    AOIAccountDict[aoiSceneGrid].Add(account);
                }
                else
                {
                    AOIAccountDict.Add(aoiSceneGrid, new List<string> { account });
                }
            }
        }

        private void RemoveAOIAccountDict(string account, AOISceneGrid aoiSceneGrid)
        {
            lock (account)
            {
                if (AOIAccountDict.ContainsKey(aoiSceneGrid))
                {
                    if (AOIAccountDict[aoiSceneGrid].Contains(account))
                    {
                        AOIAccountDict[aoiSceneGrid].Remove(account);
                    }
                }
            }
        }

        private void SetOnlineAccountAOISceneGrid(string account, AOISceneGrid aoiSceneGrid)
        {
            lock (account)
            {
                DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).SetAOIGrid(aoiSceneGrid);
            }
        }

        public bool IsAccountOnline(string account)
        {
            lock (account)
            {
                return OnlineAccountDict.ContainsKey(account);
            }
        }

        public void AddOnlineAccount(ClientPeer clientPeer, string account, AvaterInfo avaterInfo)
        {
            lock (clientPeer)
            {
                clientPeer.SetPlayerCache(avaterInfo);
                OnlineAccountDict.Add(account, clientPeer);
            }
        }

        public AvaterInfo GetOnlineAvaterInfo(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).PlayerCache;
            }
        }

        public void RemoveOnlineAccount(ClientPeer clientPeer)
        {
            lock (clientPeer)
            {
                if (AOIAccountDict.ContainsKey(OnlineAccountDict[clientPeer.Account].AOISceneGrid))
                {
                    AOIAccountDict[OnlineAccountDict[clientPeer.Account].AOISceneGrid].Remove(clientPeer.Account);
                }
                OnlineAccountDict.Remove(clientPeer.Account);
                OnlinePlayerTransformDict.Remove(clientPeer);
            }
        }

        public List<string> GetOnlinePlayerAccount()
        {
            List<string> onlineAccountList = new List<string>();
            foreach (var item in OnlineAccountDict.Keys)
            {
                lock (item)
                {
                    onlineAccountList.Add(item);
                }
            }
            return onlineAccountList;
        }

        public List<string> GetOtherOnlinePlayerAccount(string localAccount)
        {
            List<string> onlineAccountList = new List<string>();
            foreach (var item in OnlineAccountDict.Keys)
            {
                lock (item)
                {
                    if (item != localAccount)
                    {
                        onlineAccountList.Add(item);
                    }
                }
            }
            return onlineAccountList;
        }

        public List<ClientPeer> GetOnlinePlayerPeer()
        {
            List<ClientPeer> onlinePeerList = new List<ClientPeer>();
            foreach (var item in OnlineAccountDict.Values)
            {
                lock (item)
                {
                    onlinePeerList.Add(item);
                }
            }
            return onlinePeerList;
        }

        public List<ClientPeer> GetOtherOnlinePlayerPeer(ClientPeer localPeer)
        {
            List<ClientPeer> onlinePeerList = new List<ClientPeer>();
            foreach (var item in OnlineAccountDict.Values)
            {
                lock (item)
                {
                    if (item != localPeer)
                    {
                        onlinePeerList.Add(item);
                    }
                }
            }
            return onlinePeerList;
        }

        public void SetOnlinePlayerTransform(ClientPeer clientPeer, TransformOnline playerTransform)
        {
            lock (clientPeer)
            {
                if (OnlinePlayerTransformDict.ContainsKey(clientPeer))
                {
                    OnlinePlayerTransformDict[clientPeer] = playerTransform;
                }
                else
                {
                    OnlinePlayerTransformDict.Add(clientPeer, playerTransform);
                }
            }
        }

        public void UpdateOnlineAvaterInfo(string attackerAccount, AvaterInfo attackerAvaterInfo, string damagerAccount, AvaterInfo damagerAvaterInfo)
        {
            lock (attackerAccount)
            {
                if (OnlineAccountDict.ContainsKey(attackerAccount))
                {
                    OnlineAccountDict[attackerAccount].SetPlayerCache(attackerAvaterInfo);
                }
                if (OnlineAccountDict.ContainsKey(damagerAccount))
                {
                    OnlineAccountDict[damagerAccount].SetPlayerCache(damagerAvaterInfo);
                }
            }
        }

        public void SetOnlineAvaterIndex(string account, AvaterCode avater)
        {
            lock (account)
            {
                int index = 0;
                List<AvaterAttributeInfo> tempAttributeInfoList = GetOnlineAvaterInfo(account).AttributeInfoList;
                for (int i = 0; i < tempAttributeInfoList.Count; i++)
                {
                    if (tempAttributeInfoList[i].Avater == avater)
                    {
                        index = i; break;
                    }
                }
                if (OnlineAccountDict.ContainsKey(account))
                {
                    OnlineAccountDict[account].SetOnlinePlayerAvaterIndex(index);
                }
            }
        }

        public int GetOnlineAvaterIndex(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).OnlinePlayerAvaterIndex;
            }
        }
    }
}
