using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Systems;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Constants;
using SangoCommon.Enums;
using SangoCommon.Structs;
using SangoCommon.Tools;
using System.Collections.Generic;
using System.Threading;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Threads
{
    public class SyncPlayerTransformThreads : BaseThreads
    {
        private Thread _thread;
        private Dictionary<AOISceneGrid, List<string>> AOIMovedAccountDict = new Dictionary<AOISceneGrid, List<string>>();

        public override void Run()
        {
            _thread = new Thread(Update);
            _thread.IsBackground = true;
            _thread.Start();
        }

        public override void Update()
        {
            Thread.Sleep(ThreadsConstant.SyncPlayerTransformSleep);
            while (true)
            {
                Thread.Sleep(ThreadsConstant.SyncPlayerTransformTime);
                CheckIfTheClientMoved();
                SendAOITransform();
            }
        }

        public override void Stop()
        {
            _thread.Abort();
        }       

        private void CheckIfTheClientMoved()
        {
            AOIMovedAccountDict.Clear();
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetAllOnlinePlayerClientPeerList();
            for (int i = 0; i < onlinePeerList.Count; i++)
            {
                ClientPeer peer = onlinePeerList[i];
                if (peer.TransformClock == 0)
                {
                    if (peer.CurrentTransformOnline == null || peer.LastTransformOnline == null) continue;
                    if (Vector3Position.Distance(peer.CurrentTransformOnline.Vector3Position, peer.LastTransformOnline.Vector3Position) > ThreadsConstant.SyncPlayerTransformVector3PositionDistanceLimit)
                    {
                        SetAOIMovedAccountDict(peer);
                    }
                }
                else
                {
                    if (peer.TransformClock > ThreadsConstant.SyncPlayerTransformClockMax)
                    {
                        //TODO
                        //We define in this occasion the client has disconnected
                    }
                    else
                    {
                        TransformOnline predictTransformOnline = PredictSystem.Instance.PredictNextTransform(peer.Account, peer.LastTransformOnline, peer.CurrentTransformOnline);
                        peer.SetTransformOnline(predictTransformOnline);
                        OnlineAccountCache.Instance.UpdateOnlineAccountAOIInfo(peer.Account, SceneCode.Island, predictTransformOnline.Vector3Position.X, predictTransformOnline.Vector3Position.Z);
                        if (Vector3Position.Distance(peer.CurrentTransformOnline.Vector3Position, peer.LastTransformOnline.Vector3Position) > ThreadsConstant.SyncPlayerTransformVector3PositionDistanceLimit)
                        {
                            SetAOIMovedAccountDict(peer);
                        }
                    }
                }
                peer.SetTransformClock(peer.TransformClock + 1);
            }
        }

        private void SendAOITransform()
        {
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetAllOnlinePlayerClientPeerList();
            for (int i = 0; i < onlinePeerList.Count; i++)
            {
                ClientPeer peer = onlinePeerList[i];
                AOISceneGrid aoiSceneGrid = peer.AOISceneGrid;
                List<TransformOnline> surroundAOIMovedTransformList = new List<TransformOnline>();
                if (aoiSceneGrid != null)
                {
                    List<string> surroundAOIMovedAccountList = GetSurroundAOIMovedAccount(aoiSceneGrid);
                    for (int j = 0; j < surroundAOIMovedAccountList.Count; j++)
                    {
                        TransformOnline aoiTransform = OnlineAccountCache.Instance.GetOnlinePlayerClientPeerByAccount(surroundAOIMovedAccountList[j]).CurrentTransformOnline;
                        surroundAOIMovedTransformList.Add(aoiTransform);
                    }
                }
                string surroundAOIMovedTransformListJson = SetJsonString(surroundAOIMovedTransformList);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.PlayerTransformList, surroundAOIMovedTransformListJson);
                EventData eventData = new EventData((byte)EventCode.SyncPlayerTransform);
                eventData.SetParameters(dict);
                peer.SendEvent(eventData, new SendParameters());
            }
        }

        private void SetAOIMovedAccountDict(ClientPeer peer)
        {
            AOISceneGrid aoiCurrent = peer.AOISceneGrid;
            if (AOIMovedAccountDict.ContainsKey(aoiCurrent))
            {
                AOIMovedAccountDict[aoiCurrent].Add(peer.Account);
            }
            else
            {
                AOIMovedAccountDict.Add(aoiCurrent, new List<string> { peer.Account });
            }
        }

        private List<string> GetSurroundAOIMovedAccount(AOISceneGrid aoiSceneGrid)
        {
            List<AOISceneGrid> surroundAOIGridList = AOISystem.Instance.GetSurroundAOIGrid(aoiSceneGrid);
            List<string> aoiAccountList = new List<string>();            
            for (int i = 0; i < surroundAOIGridList.Count; i++)
            {
                if (AOIMovedAccountDict.ContainsKey(surroundAOIGridList[i]))
                {
                    List<string> tempList = DictTools.GetDictValue<AOISceneGrid, List<string>>(AOIMovedAccountDict, surroundAOIGridList[i]);
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
}
