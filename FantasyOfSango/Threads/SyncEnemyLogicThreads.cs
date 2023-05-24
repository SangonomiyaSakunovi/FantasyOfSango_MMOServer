using FantasyOfSango.Bases;
using FantasyOfSango.Caches;
using SangoCommon.AI;
using SangoCommon.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FantasyOfSango.Threads
{
    public class SyncEnemyLogicThreads : BaseThreads
    {
        private Thread _thread;

        private IEnumerable<AIBase> enemyAILogics => OnlineNPCCache.Instance.GetNPCAIs();

        public override void Run()
        {
            _thread = new Thread(Update);
            _thread.IsBackground = true;
            _thread.Start();
        }

        public override void Stop()
        {
            _thread.Abort();
        }

        public override void Update()
        {
            Thread.Sleep(ThreadsConstant.SyncEnemyLogicSleep);
            while (true)
            {
                Thread.Sleep(ThreadsConstant.SyncEnemyLogicSleep);

                foreach (var ai in enemyAILogics)
                {
                    ai.UpdateAI();
                }
            }
        }
    }
}
