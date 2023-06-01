using FantasyOfSango.Bases;
using FantasyOfSango.Systems;
using Photon.SocketServer;
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
            bool isAccountHasRegist = RegisterSystem.Instance.IsAccountHasRegist(account);
            bool isRegistSuccess = false;
            if (!isAccountHasRegist)
            {
                bool isAddUserInfo = RegisterSystem.Instance.InitUserInfo(account, password, nickname);
                bool isInitAvaterInfo = RegisterSystem.Instance.InitAvaterInfo(account, nickname);
                bool isInitMissionInfo = RegisterSystem.Instance.InitMissionInfo(account);
                bool isInitItemInfo = RegisterSystem.Instance.InitItemInfo(account);
                if (isAddUserInfo && isInitAvaterInfo && isInitMissionInfo && isInitItemInfo)
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
    }
}
