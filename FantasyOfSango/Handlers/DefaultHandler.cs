using SangoCommon.Enums;
using FantasyOfSango.Bases;
using Photon.SocketServer;
using System;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class DefaultHandler : BaseHandler
    {
        public DefaultHandler()
        {
            OpCode = OperationCode.Default;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}
