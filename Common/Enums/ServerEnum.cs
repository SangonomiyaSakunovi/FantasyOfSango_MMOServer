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
        PlayerCache,
        PlayerTransformCache,
        OnlineAccountList,
        PlayerTransformCacheList,
        AttackCommand,
        AttackDamage,
        AttackResult,
        ChooseAvater
    }

    public enum ReturnCode : short
    {
        Success,
        Fail,
        AccountOnline
    }
}
