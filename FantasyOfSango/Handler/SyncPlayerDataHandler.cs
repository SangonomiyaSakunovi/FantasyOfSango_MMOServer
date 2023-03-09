using Common.DataCache.PlayerDataCache;
using Common.ServerCode;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class SyncPlayerDataHandler : BaseHandler
    {
        public SyncPlayerDataHandler()
        {
            OpCode = OperationCode.SyncPlayerData;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            PlayerCache playerCache = OnlineAccountCache.Instance.GetOnlinePlayerCache(peer.Account);
            string playerCacheJson = SetJsonString(playerCache);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.PlayerCache, playerCacheJson);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
