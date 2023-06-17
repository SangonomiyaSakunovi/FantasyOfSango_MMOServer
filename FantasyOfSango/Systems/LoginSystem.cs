using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using FantasyOfSango.Constants;
using FantasyOfSango.Enums;
using FantasyOfSango.Services;
using SangoCommon.Classs;
using SangoCommon.Enums;

//Developer : SangonomiyaSakunovi
//Discription:

namespace FantasyOfSango.Systems
{
    public class LoginSystem : BaseSystem
    {
        public static LoginSystem Instance = null;

        public override void InitSystem()
        {
            base.InitSystem();
            Instance = this;
        }

        public UserInfo LookUpUserInfo(string account)
        {
            string collectionName = MongoDBCollectionConstant.UserInfos;
            string objectId = MongoDBIdConstant.UserInfo_ + account;
            return MongoDBService.Instance.LookUpOneData<UserInfo>(collectionName, objectId);
        }

        public void InitOnlineAccountCache(string account)
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
    }
}
