using FantasyOfSango.Bases;
using FantasyOfSango.Systems;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription: 

namespace FantasyOfSango.Handlers
{
    public class MissionUpdateHandler : BaseHandler
    {
        public MissionUpdateHandler()
        {
            OpCode = OperationCode.MissionUpdate;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            MissionUpdateRsp missionUpdateRsp = null;
            string missionUpdateRequestJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.MissionUpdateReq);
            MissionUpdateReq missionUpdateReq = DeJsonString<MissionUpdateReq>(missionUpdateRequestJson);
            switch (missionUpdateReq.missionUpdateTypeCode)
            {
                case MissionUpdateTypeCode.Complete:
                    missionUpdateRsp = MissionUpdateSystem.Instance.GetMissionCompleteResult(missionUpdateReq, peer);
                    break;
            }
            string missionUpdateRspJson = SetJsonString(missionUpdateRsp);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            dict.Add((byte)ParameterCode.MissionUpdateRsp, missionUpdateRspJson);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
