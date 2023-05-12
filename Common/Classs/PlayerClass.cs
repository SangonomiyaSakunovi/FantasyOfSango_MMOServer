using System;
using System.Collections.Generic;

//Developer : SangonomiyaSakunovi
//Discription:

namespace SangoCommon.Classs
{
    [Serializable]
    public class UserInfo
    {
        public string _id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
    }

    [Serializable]
    public class AvaterInfo
    {
        public string _id { get; set; }
        public string Account { get; set; }
        public string Nickname { get; set; }
        public List<AvaterAttributeInfo> AttributeInfoList { get; set; }
    }

    [Serializable]
    public class MissionInfo
    {
        public string _id { get; set;}
        public string Account { get; set; }
        public List<string> MainMissionInfoList { get; set; }
        public List<string> DailyMissionInfoList { get; set; }
        public List<string> OptionalMissionInfoList { get; set; }
    }
}
