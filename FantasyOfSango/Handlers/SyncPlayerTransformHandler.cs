using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class SyncPlayerTransformHandler : BaseHandler
    {
        public SyncPlayerTransformHandler()
        {
            OpCode = OperationCode.SyncPlayerTransform;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string playerTransformJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.PlayerTransform);
            TransformOnline playerTransform = DeJsonString<TransformOnline>(playerTransformJson);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            Dictionary<byte, object> dict = new Dictionary<byte, object>();
            if (peer.TransformClock <= 1)
            {
                peer.SetTransformOnline(playerTransform);
                OnlineAccountCache.Instance.UpdateOnlineAccountAOIInfo(peer.Account, SceneCode.Island, playerTransform.Vector3Position.X, playerTransform.Vector3Position.Z);
                dict.Add((byte)ParameterCode.SyncPlayerTransformResult, true);
            }
            else
            {               
                TransformOnline predictTrans = peer.CurrentTransformOnline;
                dict.Add((byte)ParameterCode.SyncPlayerTransformResult, false);
                dict.Add((byte)ParameterCode.PredictPlayerTransform, predictTrans);
            }
            peer.SetTransformClock(0);
            response.SetParameters(dict);
            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
