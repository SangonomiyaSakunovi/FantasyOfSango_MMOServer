﻿using Common.ElementCode;
using Common.GameObjectCode;
using System;

namespace Common.DataCache.PlayerDataCache
{
    [Serializable]
    public class AttributeInfoCache
    {
        public AvaterCode Avater { get; set; }
        public int HP { get; set; }
        public int HPFull { get; set; }
        public int MP { get; set; }
        public int MPFull { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementGauge { get; set; }
    }
}
