using System.Collections.Generic;

namespace SangoCommon.Classs
{
    public class UserInfo
    {
        public string _id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
    }

    public class AvaterInfo
    {
        public string Account { get; set; }
        public string Nickname { get; set; }
        public List<AvaterAttributeInfo> AttributeInfoList { get; set; }
    }
}
