//Developer : SangonomiyaSakunovi
//Discription:

namespace SangoCommon.Enums
{
    public enum EventCode : byte
    {
        NewAccountJoin,
        SyncPlayerTransform,
        AttackCommand,
        AttackResult,
        ChooseAvater,
        Chat
    }

    public enum OperationCode : byte
    {
        Default,
        Login,
        Register,
        SyncPlayerData,
        SyncPlayerTransform,
        SyncPlayerAccount,
        AttackCommand,
        AttackDamage,
        ChooseAvater,
        ItemEnhance,
        MissionUpdate,
        Chat,
        Shop,
    }

    public enum ParameterCode : byte
    {
        Account,
        Password,
        Nickname,
        AvaterInfo,
        MissionInfo,
        ItemInfo,
        PlayerTransform,
        OnlineAccountList,
        PlayerTransformList,
        AttackCommand,
        AttackDamage,
        AttackResult,
        ChooseAvater,
        SyncPlayerTransformResult,
        PredictPlayerTransform,
        ItemEnhanceReq,
        ItemEnhanceRsp,
        MissionUpdateReq,
        MissionUpdateRsp,
        OnlineAccountChatMessage,
        ShopInfoReq,
        ShopInfoRsp,
    }

    public enum ReturnCode : short
    {
        Success,
        Fail,
        AccountOnline
    }
}
