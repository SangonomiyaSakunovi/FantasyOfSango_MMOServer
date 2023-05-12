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
                        string missionCollectionName = MongoDBCollectionConstant.MissionInfos;
                        string missionObjectId = MongoDBIdConstant.MissionInfo_ + account;
                        AvaterInfo avaterInfo = MongoDBService.Instance.LookUpOneData<AvaterInfo>(avaterCollectionName, avaterObjectId);
                        MissionInfo missionInfo = MongoDBService.Instance.LookUpOneData<MissionInfo>(missionCollectionName, missionObjectId);
                        SangoServer.Instance.clientPeer.SetAccount(account);
                        SangoServer.Instance.clientPeer.SetAvaterInfo(avaterInfo);
                        SangoServer.Instance.clientPeer.SetMissionInfo(missionInfo);
                        OnlineAccountCache.Instance.AddOnlineAccount(SangoServer.Instance.clientPeer, account);
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
