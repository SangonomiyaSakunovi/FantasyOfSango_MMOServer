using FantasyOfSango.Base;
using FantasyOfSango.System;
using SangoCommon.DataCache.PlayerDataCache;
using SangoCommon.DataCache.PositionCache;
using SangoCommon.GameObjectCode;
using SangoCommon.LocationCode;
using SangoCommon.Tools;
using System.Collections.Generic;
using static SangoCommon.Struct.CommonStruct;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Cache
{
    public class OnlineAccountCache : BaseCache
    {
        public static OnlineAccountCache Instance = null;

        private Dictionary<AOISceneGrid, List<string>> AOIAccountDict = new Dictionary<AOISceneGrid, List<string>>();
        private Dictionary<string, ClientPeer> OnlineAccountDict = new Dictionary<string, ClientPeer>();
        private Dictionary<ClientPeer, TransformCache> OnlinePlayerTransformDict = new Dictionary<ClientPeer, TransformCache>();

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

        public TransformCache GetAccountTransfromCache(string account)
        {
            ClientPeer peer = DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account);
            TransformCache transformCache = null;
            if (peer != null)
            {
                transformCache = DictTools.GetDictValue<ClientPeer, TransformCache>(OnlinePlayerTransformDict, peer);
            }
            return transformCache;
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

        public void AddOnlineAccount(ClientPeer clientPeer, string account, PlayerCache playerCache)
        {
            lock (clientPeer)
            {
                clientPeer.SetPlayerCache(playerCache);
                OnlineAccountDict.Add(account, clientPeer);
            }
        }

        public PlayerCache GetOnlinePlayerCache(string account)
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

        public void SetOnlinePlayerTransform(ClientPeer clientPeer, TransformCache playerTransformCache)
        {
            lock (clientPeer)
            {
                if (OnlinePlayerTransformDict.ContainsKey(clientPeer))
                {
                    OnlinePlayerTransformDict[clientPeer] = playerTransformCache;
                }
                else
                {
                    OnlinePlayerTransformDict.Add(clientPeer, playerTransformCache);
                }
            }
        }

        public void UpdateOnlinePlayerCache(string attackerAccount, PlayerCache attackerPlayerCache, string damagerAccount, PlayerCache damagerPlayerCache)
        {
            lock (attackerAccount)
            {
                if (OnlineAccountDict.ContainsKey(attackerAccount))
                {
                    OnlineAccountDict[attackerAccount].SetPlayerCache(attackerPlayerCache);
                }
                if (OnlineAccountDict.ContainsKey(damagerAccount))
                {
                    OnlineAccountDict[damagerAccount].SetPlayerCache(damagerPlayerCache);
                }
            }
        }

        public void SetOnlineAvaterIndex(string account, AvaterCode avater)
        {
            lock (account)
            {
                int index = 0;
                List<AttributeInfoCache> tempAttributeInfoList = GetOnlinePlayerCache(account).AttributeInfoList;
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
