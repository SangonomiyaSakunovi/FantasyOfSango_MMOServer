﻿using SangoCommon.ServerCode;
using Photon.SocketServer;
using System.Text.Json;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Base
{
    public abstract class BaseHandler
    {
        public OperationCode OpCode;

        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);

        protected virtual string SetJsonString(object ob)
        {
            string jsonString = JsonSerializer.Serialize(ob);
            return jsonString;
        }

        protected T_obj DeJsonString<T_obj>(string str)
        {
            T_obj obj = JsonSerializer.Deserialize<T_obj>(str);
            return obj;
        }
    }
}
