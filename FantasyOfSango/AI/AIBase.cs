using FantasyOfSango.FSMs;
using FantasyOfSango.Systems;
using SangoCommon.Classs;
using SangoCommon.Enums;
using SangoCommon.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SangoCommon.AI
{
    public abstract class AIBase
    {
        protected readonly NPCGameObject NPCobj;

        protected readonly FSMSystem fSMSystem;

        protected readonly ElementSystem elementSystem;

        public AIBase(NPCGameObject @object, FSMSystem fsmsystem, ElementSystem elementsystem)
        {
            NPCobj = @object;
            fSMSystem = fsmsystem;
            elementSystem = elementsystem;

            InitFSM();
        }

        public abstract void UpdateAI();

        public abstract void InitFSM();

        public abstract void SetDamaged(AvaterCode avater, SkillCode skill, Vector3Position attakerPos);

        public NPCGameObject GetNPCGameObjectInfo() => NPCobj;
    }
}
