using Common.Constant;
using Common.DataCache.PositionCache;
using Common.ServerCode;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;
using System.Collections.Generic;
using System.Threading;

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
                SendTransform();
            }
        }
        public override void Stop()
        {
            _thread.Abort();
        }

        private void SendTransform()
        {
            List<TransformCache> playerTransformCacheList = new List<TransformCache>();
            foreach (var item in OnlineAccountCache.Instance.OnlinePlayerTransformDict.Values)
            {
                lock (item)
                {
                    TransformCache playerPositionCache = item;
                    playerTransformCacheList.Add(playerPositionCache);
                }                
            }
            string playerTransformCacheListJson = SetJsonString(playerTransformCacheList);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.PlayerTransformCacheList, playerTransformCacheListJson);

            foreach (var item in OnlineAccountCache.Instance.GetOnlinePlayerPeer())
            {
                lock (item)
                {
                    EventData eventData = new EventData((byte)EventCode.SyncPlayerTransform);
                    eventData.SetParameters(dict);
                    item.SendEvent(eventData, new SendParameters());
                }                
            }
        }
    }
}
