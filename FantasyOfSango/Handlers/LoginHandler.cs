using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Constants;
using FantasyOfSango.Enums;
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
                        string itemCollectionName = MongoDBCollectionConstant.ItemInfos;
                        string itemObjectId = MongoDBIdConstant.ItemInfo_ + account;

                        AvaterInfo avaterInfo = MongoDBService.Instance.LookUpOneData<AvaterInfo>(avaterCollectionName, avaterObjectId);
                        MissionInfo missionInfo = MongoDBService.Instance.LookUpOneData<MissionInfo>(missionCollectionName, missionObjectId);
                        ItemInfo itemInfo = MongoDBService.Instance.LookUpOneData<ItemInfo>(itemCollectionName, itemObjectId);

                        SangoServer.Instance.clientPeer.SetAccount(account);
                        SangoServer.Instance.clientPeer.SetAvaterInfo(avaterInfo);
                        SangoServer.Instance.clientPeer.SetMissionInfo(missionInfo);
                        SangoServer.Instance.clientPeer.SetItemInfo(itemInfo);
                        SangoServer.Instance.clientPeer.SetCurrentAvaterIndexByAvaterCode(AvaterCode.SangonomiyaKokomi);
                        SangoServer.Instance.clientPeer.SetPeerEnhanceModeCode(PeerEnhanceModeCode.Done);

                        OnlineAccountCache.Instance.AddOnlineAccount(SangoServer.Instance.clientPeer, account);
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
