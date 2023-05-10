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

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
        }

        public List<string> GetCurrentAOIAccount(AOISceneGrid aoiSceneGrid, string account = "Test")
        {
            lock (account)
            {
                return DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, aoiSceneGrid);
            }
        }

        public List<string> GetSurroundAOIAccount(AOISceneGrid aoiSceneGrid, string account = "Test")
        {
            lock (account)
            {
                List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(aoiSceneGrid);
                List<string> aoiAccountList = new List<string>();
                for (int i = 0; i < surroundAOIGridList.Count; i++)
                {
                    if (AOIAccountDict.ContainsKey(surroundAOIGridList[i]))
                    {
                        List<string> tempList = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, surroundAOIGridList[i]);
                        if (tempList != null)
                        {
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                aoiAccountList.Add(tempList[j]);
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
            return peer.CurrentTransformOnline;            
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
                return DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).AvaterInfo;
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
                if (OnlineAccountDict.ContainsKey(clientPeer.Account))
                {
                    OnlineAccountDict.Remove(clientPeer.Account);
                }               
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

        public List<ClientPeer> GetOnlinePlayerPeerList()
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

        public List<ClientPeer> GetOtherOnlinePlayerPeerList(ClientPeer localPeer)
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

        public ClientPeer GetOnlinePlayerPeer(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account);
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
