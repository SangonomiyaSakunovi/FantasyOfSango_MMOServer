using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Systems;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
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
            UserInfo lookUpUserInfo = LoginSystem.Instance.LookUpUserInfo(account);
            bool isAccountPasswordMatch = false;
            bool isAccountOnline = false;
            if (lookUpUserInfo != null)
            {
                if (lookUpUserInfo.Password == password)
                {
                    isAccountPasswordMatch = true;
                    isAccountOnline = OnlineAccountCache.Instance.IsAccountOnline(account);
                }
            }
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (isAccountPasswordMatch)
            {
                if (!isAccountOnline)
                {
                    lock (this)
                    {
                        LoginSystem.Instance.InitOnlineAccountCache(account);
                    }
                    response.ReturnCode = (short)ReturnCode.Success;
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
