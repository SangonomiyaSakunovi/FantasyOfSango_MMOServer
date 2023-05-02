using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;

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
            OnlineAccountCache.Instance.SetOnlinePlayerTransform(peer, playerTransform);
            OnlineAccountCache.Instance.UpdateOnlineAccountAOIInfo(peer.Account, SceneCode.Island, playerTransform.Vector3Position.X, playerTransform.Vector3Position.Z);
        }
    }
}
