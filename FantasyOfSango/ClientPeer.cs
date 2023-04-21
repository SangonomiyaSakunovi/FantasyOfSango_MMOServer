using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using SangoCommon.DataCache.PlayerDataCache;
using SangoCommon.LocationCode;
using SangoCommon.ServerCode;
using SangoCommon.Tools;
using static SangoCommon.Struct.CommonStruct;

//Developer : SangonomiyaSakunovi
//Discription: ClientPeer behaviours should define here

namespace FantasyOfSango
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public string Account { get; private set; }
        public AOISceneGrid AOISceneGrid { get; private set; }
        public int OnlinePlayerAvaterIndex { get; private set; }
        public PlayerCache PlayerCache { get; private set; }

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

        public void SetAOIGrid(AOISceneGrid aoiSceneGrid)
        {
            AOISceneGrid = aoiSceneGrid;
        }

        public void SetPlayerCache(PlayerCache playerCache)
        {
            PlayerCache = playerCache;
        }
        public void SetOnlinePlayerAvaterIndex(int index)
        {
            OnlinePlayerAvaterIndex = index;
        }        
    }
}
