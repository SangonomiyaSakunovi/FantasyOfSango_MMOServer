using FantasyOfSango.Bases;
using FantasyOfSango.Services;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handlers
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string account = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Account);
            string password = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Password);
            string nickname = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Nickname);

            bool isAccountHasRegist = IsAccountHasRegist(account);
            bool isRegistSuccess = false;
            if (!isAccountHasRegist)
            {
                isAccountHasRegist = AddUserInfo(account, password, nickname);
            }
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (isRegistSuccess)
            {
                response.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {
                response.ReturnCode = (short)ReturnCode.Fail;
            }
            peer.SendOperationResponse(response, sendParameters);
        }

        private bool IsAccountHasRegist(string account)
        {
            string collectionName = "UserInfos";
            string objectId = "UserInfo" + account;
            UserInfo lookUpUserInfo = MongoDBService.Instance.LookUpOneData<UserInfo>(collectionName, account);
            if (lookUpUserInfo != null)
            {
                return true;
            }
            return false;
        }

        private bool AddUserInfo(string account, string password, string nickname)
        {
            string collectionName = "UserInfos";
            UserInfo userInfo = new UserInfo
            {
                _id = "UserInfo" + account,
                Account = account,
                Password = password,
                Nickname = nickname
            };
            return MongoDBService.Instance.AddOneData<UserInfo>(userInfo, collectionName);
        }

        private bool AddPlayerCache(string account)
        {
            string collectionName = "PlayerCaches";
            return false;
        }
    }
}
