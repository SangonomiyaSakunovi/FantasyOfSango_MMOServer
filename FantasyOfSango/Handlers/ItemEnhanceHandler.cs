using FantasyOfSango.Bases;
using FantasyOfSango.Enums;
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
    public class ItemEnhanceHandler : BaseHandler
    {
        public ItemEnhanceHandler()
        {
            OpCode = OperationCode.ItemEnhance;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            if (peer.PeerEnhanceModeCode == PeerEnhanceModeCode.Done)
            {
                peer.SetPeerEnhanceModeCode(PeerEnhanceModeCode.Running);
                ItemEnhanceRsp itemEnhanceRsp = null;
                string itemEnhanceRequestJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.ItemEnhanceReq);
                ItemEnhanceReq itemEnhanceReq = DeJsonString<ItemEnhanceReq>(itemEnhanceRequestJson);

                switch (itemEnhanceReq.ItemTypeCode)
                {
                    case ItemTypeCode.Weapon:
                        itemEnhanceRsp = ItemEnhanceSystem.Instance.GetWeaponEnhanceOrBreakResult(itemEnhanceReq, peer);
                        break;
                }
                
                string itemEnhanceResponseJson = SetJsonString(itemEnhanceRsp);
                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.ItemEnhanceRsp, itemEnhanceResponseJson);
                response.SetParameters(dict);
                peer.SendOperationResponse(response, sendParameters);
                peer.SetPeerEnhanceModeCode(PeerEnhanceModeCode.Done);

                if (!itemEnhanceRsp.IsEnhanceSuccess)
                {
                    SangoServer.Log.Warn("疑似用户作弊，对应Id为：" + peer.Account);
                }                
            }
            else
            {
                SangoServer.Log.Warn("疑似用户攻击服务器，对应Id为：" + peer.Account);
            }
        }
    }
}
