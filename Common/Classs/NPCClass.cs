using SangoCommon.Enums;
using SangoCommon.Structs;
using System;

//Developer : SangonomiyaSakunovi
//Discription: 

namespace SangoCommon.Classs
{
    [Serializable]
    public class NPCGameObject
    {
        public string _id { get; set; }
        public NPCCode NPCCode { get; set; }
        public Vector3Position Vector3Position { get; set; }
        public QuaternionRotation QuaternionRotation { get; set; }
        public NPCAttributeInfo NPCAttributeInfo { get; set; }
        public AOISceneGrid AOISceneGrid { get; set; }
    }
}
