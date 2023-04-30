using FantasyOfSango.Bases;
using FantasyOfSango.Constants;
using FantasyOfSango.Services;
using Photon.SocketServer;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Tools;
using System.Collections.Generic;
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
                bool isAddUserInfo = AddUserInfo(account, password, nickname);
                bool isInitAvaterInfo = InitAvaterInfo(account, nickname);
                if (isAddUserInfo && isInitAvaterInfo)
                {
                    isRegistSuccess = true;
                }                
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
            string collectionName = MongoDBCollectionConstant.UserInfos;
            string objectId = MongoDBIdConstant.UserInfo_ + account;
            UserInfo lookUpUserInfo = MongoDBService.Instance.LookUpOneData<UserInfo>(collectionName, objectId);
            if (lookUpUserInfo != null)
            {
                return true;
            }
            return false;
        }

        private bool AddUserInfo(string account, string password, string nickname)
        {
            string collectionName = MongoDBCollectionConstant.UserInfos;
            UserInfo userInfo = new UserInfo
            {
                _id = MongoDBIdConstant.UserInfo_ + account,
                Account = account,
                Password = password,
                Nickname = nickname
            };
            return MongoDBService.Instance.AddOneData<UserInfo>(userInfo, collectionName);
        }

        private bool InitAvaterInfo(string account, string nickname)
        {
            string collectionName = MongoDBCollectionConstant.AvaterInfos;
            AvaterAttributeInfo kokomiInfo = new AvaterAttributeInfo
            {
                Avater = AvaterCode.SangonomiyaKokomi,
                HP = 80,
                HPFull = 100,
                MP = 40,
                MPFull = 50,
                Attack = 1,
                Defence = 0,
                ElementType = ElementTypeCode.Hydro,
                ElementGauge = 2
            };
            AvaterAttributeInfo yoimiyaInfo = new AvaterAttributeInfo
            {
                Avater = AvaterCode.Yoimiya,
                HP = 80,
                HPFull = 100,
                MP = 40,
                MPFull = 50,
                Attack = 1,
                Defence = 0,
                ElementType = ElementTypeCode.Hydro,
                ElementGauge = 2
            };
            AvaterAttributeInfo ayakaInfo = new AvaterAttributeInfo
            {
                Avater = AvaterCode.Ayaka,
                HP = 80,
                HPFull = 100,
                MP = 40,
                MPFull = 50,
                Attack = 1,
                Defence = 0,
                ElementType = ElementTypeCode.Hydro,
                ElementGauge = 2
            };
            AvaterAttributeInfo aetherInfo = new AvaterAttributeInfo
            {
                Avater = AvaterCode.Aether,
                HP = 80,
                HPFull = 100,
                MP = 40,
                MPFull = 50,
                Attack = 1,
                Defence = 0,
                ElementType = ElementTypeCode.Hydro,
                ElementGauge = 2
            };

            AvaterInfo avaterInfo = new AvaterInfo
            {
                _id = MongoDBIdConstant.AvaterInfo_ + account,
                Account = account,
                Nickname = nickname,
                AttributeInfoList = new List<AvaterAttributeInfo>() { kokomiInfo, yoimiyaInfo, ayakaInfo, aetherInfo }
            };
            return MongoDBService.Instance.AddOneData<AvaterInfo>(avaterInfo, collectionName);
        }
    }
}
