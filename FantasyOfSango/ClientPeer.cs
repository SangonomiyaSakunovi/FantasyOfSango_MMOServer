using SangoCommon.GameObjectCode;
using SangoCommon.ServerCode;
using SangoCommon.Tools;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

//Developer : SangonomiyaSakunovi
//Discription: ClientPeer behaviours should define here

namespace FantasyOfSango
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public string Account { get; private set; }

        //Call father class to intiate
        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {

        }
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            SangoServer.Log.Info("The Client is DisConnect");
            OnlineAccountCache.Instance.RemoveOnlineAccount(this);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Initiate a Handler
            BaseHandler handler = DictTools.GetDictValue<OperationCode, BaseHandler>
                (SangoServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            //SangoServer.Log.Info("The handler is " + handler.OpCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTools.GetDictValue<OperationCode, BaseHandler>
                                                (SangoServer.Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }

        public void SetAccount(string account)
        {
            Account = account;
        }        
    }
}
