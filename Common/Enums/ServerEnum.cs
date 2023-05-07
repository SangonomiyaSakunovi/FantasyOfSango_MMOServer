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
    }

    public enum ParameterCode : byte
    {
        Account,
        Password,
        Nickname,
        AvaterInfo,
        PlayerTransform,
        OnlineAccountList,
        PlayerTransformList,
        AttackCommand,
        AttackDamage,
        AttackResult,
        ChooseAvater,
        SyncPlayerTransformResult,
        PredictPlayerTransform
    }

    public enum ReturnCode : short
    {
        Success,
        Fail,
        AccountOnline
    }
}
