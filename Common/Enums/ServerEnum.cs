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
        ChooseAvater
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
        MissionUpdate
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
        MissionUpdateRsp
    }

    public enum ReturnCode : short
    {
        Success,
        Fail,
        AccountOnline
    }
}
