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
    public class ChooseAvaterHandler : BaseHandler
    {
        public ChooseAvaterHandler()
        {
            OpCode = OperationCode.ChooseAvater;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            AvaterCode avater = (AvaterCode)DictTools.GetDictValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.ChooseAvater);
            string account = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Account);
            OnlineAccountCache.Instance.SetOnlineAvaterIndex(account, avater);
            List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetOtherOnlinePlayerPeer(peer);
            foreach (ClientPeer onlinePeer in onlinePeerList)
            {
                EventData eventData = new EventData((byte)EventCode.ChooseAvater);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.ChooseAvater, avater);
                dict.Add((byte)ParameterCode.Account, account);
                eventData.SetParameters(dict);
                onlinePeer.SendEvent(eventData, sendParameters);
            }
        }
    }
}
