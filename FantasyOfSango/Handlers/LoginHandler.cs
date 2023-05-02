using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Constants;
using FantasyOfSango.Services;
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
            string collectionName = MongoDBCollectionConstant.UserInfos;
            string objectId = MongoDBIdConstant.UserInfo_ + account;
            bool isAccountPasswordMatch = false;
            bool isAccountOnline = false;
            UserInfo lookUpUserInfo = MongoDBService.Instance.LookUpOneData<UserInfo>(collectionName,objectId);
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
                        string avaterCollectionName = MongoDBCollectionConstant.AvaterInfos;
                        string avaterObjectId = MongoDBIdConstant.AvaterInfo_ + account;
                        AvaterInfo avaterInfo = MongoDBService.Instance.LookUpOneData<AvaterInfo>(avaterCollectionName, avaterObjectId);
                        SangoServer.Instance.clientPeer.SetAccount(account);
                        OnlineAccountCache.Instance.AddOnlineAccount(SangoServer.Instance.clientPeer, account, avaterInfo);
                        OnlineAccountCache.Instance.SetOnlineAvaterIndex(account, AvaterCode.SangonomiyaKokomi);
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
