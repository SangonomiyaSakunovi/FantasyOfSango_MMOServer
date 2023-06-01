using SangoCommon.Structs;
using System;

//Developer : SangonomiyaSakunovi
//Discription:

namespace SangoCommon.Classs
{
    [Serializable]
    public class TransformOnline
    {
        public string Account { get; set; }
        public Vector3Position Vector3Position { get; set; }
        public QuaternionRotation QuaternionRotation { get; set; }
    }
}
