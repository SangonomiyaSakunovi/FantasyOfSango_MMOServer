﻿using FantasyOfSango.Bases;
using FantasyOfSango.Systems;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Structs;
using SangoCommon.Tools;
using System.Collections.Generic;

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

        #region LookUpAOIAccount        
        public List<ClientPeer> GetCurrentAOIClientPeerList(NPCGameObject npcGameObject, string locker = "locker")
        {
            lock (locker)
            {
                List<ClientPeer> aoiClientPeerList = new List<ClientPeer>();
                if (AOIAccountDict.ContainsKey(npcGameObject.AOISceneGrid))
                {
                    List<string> tempList = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, npcGameObject.AOISceneGrid);
                    if (tempList != null)
                    {
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            ClientPeer tempPeer = GetOnlinePlayerClientPeerByAccount(tempList[j]);
                            aoiClientPeerList.Add(tempPeer);
                        }
                    }
                }
                return aoiClientPeerList;
            }
        }

        public List<string> GetSurroundAOIAccountList(ClientPeer clientPeer, string locker = "locker")
        {
            lock (locker)
            {
                List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(clientPeer.AOISceneGrid);
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

        public List<ClientPeer> GetSurroundAOIClientPeerList(ClientPeer clientPeer, string locker = "locker")
        {
            lock (locker)
            {
                List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(clientPeer.AOISceneGrid);
                List<ClientPeer> aoiClientPeerList = new List<ClientPeer>();
                for (int i = 0; i < surroundAOIGridList.Count; i++)
                {
                    if (AOIAccountDict.ContainsKey(surroundAOIGridList[i]))
                    {
                        List<string> tempList = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIAccountDict, surroundAOIGridList[i]);
                        if (tempList != null)
                        {
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                ClientPeer tempPeer = GetOnlinePlayerClientPeerByAccount(tempList[j]);
                                aoiClientPeerList.Add(tempPeer);
                            }
                        }
                    }
                }
                return aoiClientPeerList;
            }
        }
        #endregion

        #region UpdateAOIAccount
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

        private void SetOnlineAccountAOISceneGrid(string account, AOISceneGrid aoiSceneGrid)
        {
            lock (account)
            {
                DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account).SetAOIGrid(aoiSceneGrid);
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
        #endregion

        public void AddOnlineAccount(ClientPeer clientPeer, string account)
        {
            lock (clientPeer)
            {
                OnlineAccountDict.Add(account, clientPeer);
            }
        }

        public bool IsAccountOnline(string account)
        {
            lock (account)
            {
                return OnlineAccountDict.ContainsKey(account);
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

        public List<ClientPeer> GetAllOnlinePlayerClientPeerList(string locker = "locker")
        {
            lock (locker)
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
        }

        public ClientPeer GetOnlinePlayerClientPeerByAccount(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string, ClientPeer>(OnlineAccountDict, account);
            }
        }
    }
}
