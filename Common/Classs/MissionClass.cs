using SangoCommon.Enums;
using System;

//Developer : SangonomiyaSakunovi
//Discription: 

namespace SangoCommon.Classs
{
    [Serializable]
    public class MissionUpdateReq
    {
        public string MissionId { get; set; }
        public MissionTypeCode MissionTypeCode { get; set; }
        public MissionUpdateTypeCode missionUpdateTypeCode { get; set; }
    }

    [Serializable]
    public class MissionUpdateRsp
    {
        public string MissionId { get; set; }
        public bool IsCompleteSuccess { get; set; }
        public MissionTypeCode MissionTypeCode { get; set; }
        public MissionUpdateTypeCode missionUpdateTypeCode { get; set; }
    }
}
