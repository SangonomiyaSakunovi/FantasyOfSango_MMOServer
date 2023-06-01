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
            AvaterInfo avaterInfo = peer.AvaterInfo;
            string avaterInfoJson = SetJsonString(avaterInfo);
            MissionInfo missionInfo = peer.MissionInfo;
            string missionInfoJson = SetJsonString(missionInfo);
            ItemInfo itemInfo = peer.ItemInfo;
            string itemInfoJson = SetJsonString(itemInfo);

            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.AvaterInfo, avaterInfoJson);
            dict.Add((byte)ParameterCode.MissionInfo, missionInfoJson);
            dict.Add((byte)ParameterCode.ItemInfo, itemInfoJson);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
