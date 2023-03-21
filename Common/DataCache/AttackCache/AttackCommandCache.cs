using SangoCommon.ComBatCode;
using SangoCommon.DataCache.PositionCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SangoCommon.DataCache.AttackCache
{
    public class AttackCommandCache
    {
        public string Account { get; set; }
        public SkillCode SkillCode { get; set; }
        public Vector3Cache Vector3Cache { get; set; }
        public QuaternionCache QuaternionCache { get; set; }
    }
}
