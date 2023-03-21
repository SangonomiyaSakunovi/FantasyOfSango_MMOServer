using SangoCommon.DataCache.PlayerDataCache;
using SangoCommon.DataCache.PositionCache;
using SangoCommon.GameObjectCode;
using SangoCommon.Tools;
using FantasyOfSango.Base;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Cache
{
    public class OnlineAccountCache : BaseCache
    {
        public static OnlineAccountCache Instance = null;

        private Dictionary<string, ClientPeer> OnlineAccountDict = new Dictionary<string, ClientPeer>();
        private Dictionary<string, PlayerCache> OnlinePlayerCacheDict = new Dictionary<string, PlayerCache>();
        private Dictionary<string, int> OnlinePlayerAvaterIndexDict = new Dictionary<string, int>();
        private Dictionary<string, AvaterCode> OnlineAvaterDict = new Dictionary<string, AvaterCode>();

        public Dictionary<ClientPeer, TransformCache> OnlinePlayerTransformDict = new Dictionary<ClientPeer, TransformCache>();

        public override void InitCache()
        {
            base.InitCache();
            Instance = this;
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
                OnlineAccountDict.Add(account, clientPeer);
                OnlinePlayerCacheDict.Add(account, playerCache);
            }
        }

        public PlayerCache GetOnlinePlayerCache(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string, PlayerCache>(OnlinePlayerCacheDict, account);
            }
        }

        public void RemoveOnlineAccount(ClientPeer clientPeer)
        {
            lock (clientPeer)
            {
                OnlineAccountDict.Remove(clientPeer.Account);
                OnlinePlayerCacheDict.Remove(clientPeer.Account);
                OnlinePlayerTransformDict.Remove(clientPeer);
                OnlineAvaterDict.Remove(clientPeer.Account);
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
                if (OnlinePlayerCacheDict.ContainsKey(attackerAccount))
                {
                    OnlinePlayerCacheDict[attackerAccount] = attackerPlayerCache;
                }
                else
                {
                    OnlinePlayerCacheDict.Add(attackerAccount, attackerPlayerCache);
                }
                if (OnlinePlayerCacheDict.ContainsKey(damagerAccount))
                {
                    OnlinePlayerCacheDict[damagerAccount] = damagerPlayerCache;
                }
                else
                {
                    OnlinePlayerCacheDict.Add(damagerAccount, damagerPlayerCache);
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
                if (OnlinePlayerAvaterIndexDict.ContainsKey(account))
                {
                    OnlinePlayerAvaterIndexDict[account] = index;
                }
                else
                {
                    OnlinePlayerAvaterIndexDict.Add(account, index);
                }
            }
        }

        public int GetOnlineAvaterIndex(string account)
        {
            lock (account)
            {
                return DictTools.GetDictValue<string,int>(OnlinePlayerAvaterIndexDict, account);
            }
        }
    }
}
