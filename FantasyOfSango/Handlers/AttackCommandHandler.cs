using SangoCommon.Enums;
using SangoCommon.Tools;
using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class AttackCommandHandler : BaseHandler
    {
        public AttackCommandHandler()
        {
            OpCode = OperationCode.AttackCommand;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string attackCommandJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.AttackCommand);
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetSurroundAOIClientPeerList(peer);
            foreach (ClientPeer onlinePeer in onlinePeerList)
            {
                EventData eventData = new EventData((byte)EventCode.AttackCommand);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.AttackCommand, attackCommandJson);
                eventData.SetParameters(dict);
                onlinePeer.SendEvent(eventData, sendParameters);
            }
        }
    }
}
