using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using Photon.SocketServer;
using SangoCommon.Enums;
using SangoCommon.Tools;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class ChatHandler : BaseHandler
    {
        public ChatHandler()
        {
            OpCode = OperationCode.Chat;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string onlineAccountChatMessageJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.OnlineAccountChatMessage);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            response.ReturnCode = (short)ReturnCode.Success;
            peer.SendOperationResponse(response, sendParameters);
            List<ClientPeer> allOnlinePeerList = OnlineAccountCache.Instance.GetAllOnlinePlayerClientPeerList();
            foreach (ClientPeer onlinePeer in allOnlinePeerList)
            {
                EventData eventData = new EventData((byte)EventCode.Chat);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.OnlineAccountChatMessage, onlineAccountChatMessageJson);
                eventData.SetParameters(dict);
                onlinePeer.SendEvent(eventData, sendParameters);
            }

        }
    }
}
