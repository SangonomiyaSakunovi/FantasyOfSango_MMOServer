using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Enums;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Structs;
using SangoCommon.Tools;

//Developer : SangonomiyaSakunovi
//Discription: ClientPeer behaviours should define here

namespace FantasyOfSango
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public string Account { get; private set; }
        public AOISceneGrid AOISceneGrid { get; private set; }
        public int CurrentAvaterIndex { get; private set; }
        public AvaterInfo AvaterInfo { get; private set; }
        public MissionInfo MissionInfo { get; private set; }
        public ItemInfo ItemInfo { get; private set; }
        
        public int TransformClock { get; private set; }
        public TransformOnline CurrentTransformOnline { get; private set; }
        public TransformOnline LastTransformOnline { get; private set; }

        public PeerEnhanceModeCode PeerEnhanceModeCode { get; private set; }

        //Call father class to intiate
        public ClientPeer(InitRequest initRequest) : base(initRequest) { }
  
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            SangoServer.Log.Info("The Client is DisConnect");
            OnlineAccountCache.Instance.RemoveOnlineAccount(this);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //Initiate a Handlers
            BaseHandler handler = DictTools.GetDictValue<OperationCode, BaseHandler>
                (SangoServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            //SangoServer.Log.Info("The handler is " + handler.OpCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                BaseHandler defaultHandler = DictTools.GetDictValue<OperationCode, BaseHandler>
                                                (SangoServer.Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }

        public void SetAccount(string account)
        {
            Account = account;
        }

        public void SetAOIGrid(AOISceneGrid aoiSceneGrid)
        {
            AOISceneGrid = aoiSceneGrid;
        }

        public void SetAvaterInfo(AvaterInfo avaterInfo)
        {
            AvaterInfo = avaterInfo;
        }

        public void SetMissionInfo(MissionInfo missionInfo)
        {
            MissionInfo = missionInfo;
        }

        public void SetItemInfo(ItemInfo itemInfo)
        {
            ItemInfo = itemInfo;
        }

        public void SetCurrentAvaterIndexByAvaterCode(AvaterCode avater)
        {
            int index = 0;
            for (int i = 0; i < AvaterInfo.AttributeInfoList.Count; i++)
            {
                if (AvaterInfo.AttributeInfoList[i].Avater == avater)
                {
                    index = i; break;
                }
            }
            CurrentAvaterIndex = index;
        }

        public void SetTransformOnline(TransformOnline transformOnline)
        {
            LastTransformOnline = CurrentTransformOnline;
            CurrentTransformOnline = transformOnline;
        }

        public void SetTransformClock(int clock)
        {
            TransformClock = clock;
        }

        public void SetPeerEnhanceModeCode(PeerEnhanceModeCode peerEnhanceModeCode)
        {
            PeerEnhanceModeCode = peerEnhanceModeCode;
        }
    }
}
