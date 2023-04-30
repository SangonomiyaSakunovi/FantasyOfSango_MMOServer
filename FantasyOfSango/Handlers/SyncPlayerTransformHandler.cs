using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;

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
            string playerTransformJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.PlayerTransformCache);
            TransformOnline playerTransformCache = DeJsonString<TransformOnline>(playerTransformJson);
            OnlineAccountCache.Instance.SetOnlinePlayerTransform(peer, playerTransformCache);
            OnlineAccountCache.Instance.UpdateOnlineAccountAOIInfo(peer.Account, SceneCode.Island, playerTransformCache.Vector3Position.X, playerTransformCache.Vector3Position.Z);
        }
    }
}
