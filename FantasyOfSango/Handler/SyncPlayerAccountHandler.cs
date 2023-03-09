﻿using Common.ServerCode;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;
using System.Collections.Generic;

namespace FantasyOfSango.Handler
{
    public class SyncPlayerAccountHandler : BaseHandler
    {
        public SyncPlayerAccountHandler()
        {
            OpCode = OperationCode.SyncPlayerAccount;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            List<string> onlineAccountList = OnlineAccountCache.Instance.GetOtherOnlinePlayerAccount(peer.Account);
            string onlineAccountJson = SetJsonString(onlineAccountList);
            dict.Add((byte)ParameterCode.OnlineAccountList, onlineAccountJson);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);

            Dictionary<byte, object> dict1 = new Dictionary<byte, object>();
            dict1.Add((byte)ParameterCode.Account, peer.Account);
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetOtherOnlinePlayerPeer(peer);
            foreach (ClientPeer onlinePeer in onlinePeerList)
            {
                EventData eventData = new EventData((byte)EventCode.NewAccountJoin);
                eventData.SetParameters(dict1);
                onlinePeer.SendEvent(eventData, sendParameters);
            }
        }
    }
}