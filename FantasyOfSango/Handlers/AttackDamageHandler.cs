﻿using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
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
    public class AttackDamageHandler : BaseHandler
    {
        public AttackDamageHandler()
        {
            OpCode = OperationCode.AttackDamage;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string attackDamageJson = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.AttackDamage);
            AttackDamage attackDamage = DeJsonString<AttackDamage>(attackDamageJson);

            AttackResult attackResult = OnlineAttackSystem.Instance.GetAttackResult(attackDamage);
            if (attackResult != null)
            {
                string attackResultJson = SetJsonString(attackResult);
                SangoServer.Log.Info(attackResultJson);
                Dictionary<byte, object> dict = new Dictionary<byte, object>();
                dict.Add((byte)ParameterCode.AttackResult, attackResultJson);

                OperationResponse response = new OperationResponse(operationRequest.OperationCode);
                response.SetParameters(dict);
                peer.SendOperationResponse(response, sendParameters);

                List<ClientPeer> onlinePeerList = OnlineAccountCache.Instance.GetSurroundAOIClientPeerList(peer);
                foreach (ClientPeer onlinePeer in onlinePeerList)
                {
                    EventData eventData = new EventData((byte)EventCode.AttackResult);
                    eventData.SetParameters(dict);
                    onlinePeer.SendEvent(eventData, sendParameters);
                }
            }
            OnlineAttackCache.Instance.AddOnlineAttackDamageCache(attackDamage);
        }
    }
}
