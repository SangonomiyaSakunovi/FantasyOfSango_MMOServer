using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class SyncPlayerDataHandler : BaseHandler
    {
        public SyncPlayerDataHandler()
        {
            OpCode = OperationCode.SyncPlayerData;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            AvaterInfo avaterInfo = OnlineAccountCache.Instance.GetOnlineAvaterInfo(peer.Account);
            string avaterInfoJson = SetJsonString(avaterInfo);
            MissionInfo missionInfo = OnlineAccountCache.Instance.GetOnlineMissionInfo(peer.Account);
            string missionInfoJson = SetJsonString(missionInfo);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.AvaterInfo, avaterInfoJson);
            dict.Add((byte)ParameterCode.MissionInfo, missionInfoJson);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
