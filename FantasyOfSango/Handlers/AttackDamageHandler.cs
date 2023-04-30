using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Systems;
using Photon.SocketServer;
using System.Collections.Generic;

namespace FantasyOfSango.Handlers
{
    public class AttackDamageHandler : BaseHandler
    {
        public AttackDamageHandler()
        {
            OpCode = OperationCode.AttackDamage;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string attackDamageJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.AttackDamage);
            AttackDamage attackDamageCache = DeJsonString<AttackDamage>(attackDamageJson);
            AttackResult attackResultCache = OnlineAttackSystem.Instance.GetAttackResult(attackDamageCache);
            if (attackResultCache != null)
            {
                string attackResultJson = SetJsonString(attackResultCache);
                SangoServer.Log.Info(attackResultJson);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.AttackResult, attackResultJson);

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                response.SetParameters(dict);
                peer.SendOperationResponse(response, sendParameters);

                List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetOtherOnlinePlayerPeer(peer);
                foreach (ClientPeer onlinePeer in onlinePeerList)
                {
                    EventData eventData = new EventData((byte)EventCode.AttackResult);
                    eventData.SetParameters(dict);
                    onlinePeer.SendEvent(eventData, sendParameters);
                }
            }
            OnlineAttackCache.Instance.AddOnlineAttackDamageCache(attackDamageCache);
        }
    }
}
