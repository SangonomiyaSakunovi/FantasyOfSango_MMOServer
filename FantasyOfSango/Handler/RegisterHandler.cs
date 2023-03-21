using SangoCommon.ServerCode;
using SangoCommon.Tools;
using FantasyOfSango.Base;
using Photon.SocketServer;
using PlayerScript;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Handler
{
    public class RegisterHandler : BaseHandler
    {
        public RegisterHandler()
        {
            OpCode = OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            int changeUserInfoLineNum = 0;
            string account = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Account);
            string password = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Password);
            string nickname = DictTools.GetStringValue(operationRequest.Parameters, (byte)ParameterCode.Nickname);
            changeUserInfoLineNum = PlayerAdd.AddUserInfo(nickname, account, password);
            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            //Just need to transfer Bool, so use ReturnCode instead Parameters
            if (changeUserInfoLineNum != 0)
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
