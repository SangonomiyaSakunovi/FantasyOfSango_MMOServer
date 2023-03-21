using SangoCommon.DataCache.PositionCache;
using SangoCommon.ServerCode;
using SangoCommon.Tools;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;

namespace FantasyOfSango.Handler
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
            TransformCache playerTransformCache = DeJsonString<TransformCache>(playerTransformJson);
            OnlineAccountCache.Instance.SetOnlinePlayerTransform(peer, playerTransformCache);
        }
    }
}
