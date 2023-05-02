using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Constants;
using SangoCommon.Classs;
using SangoCommon.Enums;
using System.Collections.Generic;
using System.Threading;
using SangoCommon.Structs;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Threads
{
    public class SyncPlayerTransformThreads : BaseThreads
    {
        private Thread _thread;
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
                SendAOITransform();
            }
        }
        public override void Stop()
        {
            _thread.Abort();
        }

        private void SendAOITransform()
        {
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetOnlinePlayerPeer();
            for (int i = 0; i < onlinePeerList.Count; i++)
            {
                ClientPeer peer = onlinePeerList[i];
                AOISceneGrid aoiSceneGrid = peer.AOISceneGrid;
                List<TransformOnline> surroundAOITransformList = new List<TransformOnline>();
                if (aoiSceneGrid != null)
                {
                    List<string> surroundAOIAccountList = OnlineAccountCache.Instance.GetSurroundAOIAccount(aoiSceneGrid);
                    for (int j = 0; j < surroundAOIAccountList.Count; j++)
                    {
                        TransformOnline aoiTransform = OnlineAccountCache.Instance.GetAccountTransfrom(surroundAOIAccountList[j]);
                        surroundAOITransformList.Add(aoiTransform);
                    }
                }
                string surroundAOITransformListJson = SetJsonString(surroundAOITransformList);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.PlayerTransformList, surroundAOITransformListJson);
                EventData eventData = new EventData((byte)EventCode.SyncPlayerTransform);
                eventData.SetParameters(dict);
                peer.SendEvent(eventData, new SendParameters());
            }
        }
    }
}
