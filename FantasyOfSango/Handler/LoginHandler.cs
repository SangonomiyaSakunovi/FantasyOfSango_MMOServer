using Common.DataCache.PlayerDataCache;
using Common.ServerCode;
using Common.Tools;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using FantasyOfSango.LoadSQL;
using Photon.SocketServer;
using PlayerData;
using PlayerScript;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class LoginHandler : BaseHandler
    {
        public LoginHandler()
        {
            OpCode = OperationCode.Login;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string account = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Account);
            string password = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Password);
            Player playerData = PlayerLookUp.PlayerDataLookUp(account, password);
            bool isAccountOnline = OnlineAccountCache.Instance.IsAccountOnline(account);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //Just need to transfer Bool, so use ReturnCode instead Parameters
            if (playerData != null)
            {
                if (!isAccountOnline)
                {
                    response.ReturnCode = (short)ReturnCode.Success;
                    lock (this)
                    {
                        PlayerCache playerCache = LoadPlayerCache.LoadPlayer(account, playerData);
                        SangoServer.Instance.clientPeer.SetAccount(account);
                        OnlineAccountCache.Instance.AddOnlineAccount(SangoServer.Instance.clientPeer, account, playerCache);
                    }
                }
                else
                {
                    response.ReturnCode = (short)ReturnCode.AccountOnline;
                }
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Fail;
            }
            peer.SendOperationResponse(response, sendParameters);
        }

    }
}
