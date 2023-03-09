using Common.ServerCode;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using FantasyOfSango.Base;
using FantasyOfSango.Cache;
using FantasyOfSango.Handler;
using FantasyOfSango.System;
using FantasyOfSango.Threads;
using log4net.Config;
using Photon.SocketServer;
using System.Collections.Generic;
using System.IO;

//Developer : SangonomiyaSakunovi
//Discription: The main cs of PhotonServer, give the Config

namespace FantasyOfSango
{
    public class SangoServer : ApplicationBase
    {
        //There will be only one _log exist, so the permission readonly
        public static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public static SangoServer Instance
        {
            get; private set;
        }

        public ClientPeer clientPeer = null;

        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();

 

        //Build connectioner peer
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            Log.Info("New Client connected");
            clientPeer = new ClientPeer(initRequest);
            return clientPeer;
        }

        //Call when the server initiating
        protected override void Setup()
        {
            Instance = this;

            //Configure customize log path, the applicationLogPath need to specify
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"]
                = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            //Configure the configFile position
            FileInfo logConfigFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (logConfigFileInfo.Exists)
            {
                //Set Factory and Read
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
                XmlConfigurator.ConfigureAndWatch(logConfigFileInfo);
            }
            //Use _log to show customize info
            Log.Info("Sango Server is Running");

            //Init the Service

            //Init the Handler
            InitHandler();
            //Init the Cache
            InitCache();
            //Init the System
            InitSystem();
            //Init the Threads
            InitThreads();

            Log.Info("Init all Component");
        }

        //Call when the server shutDown
        protected override void TearDown()
        {
            CleanThreads();
            Log.Info("Sango Server is Shut Down");
        }

        private void InitHandler()
        {
            DefaultHandler defaultHandler = new DefaultHandler();
            HandlerDict.Add(defaultHandler.OpCode, defaultHandler);
            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.OpCode, loginHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            HandlerDict.Add(registerHandler.OpCode, registerHandler);
            SyncPlayerDataHandler syncPlayerDataHandler = new SyncPlayerDataHandler();
            HandlerDict.Add(syncPlayerDataHandler.OpCode, syncPlayerDataHandler);
            SyncPlayerTransformHandler syncPlayerPositionHandler = new SyncPlayerTransformHandler();
            HandlerDict.Add(syncPlayerPositionHandler.OpCode, syncPlayerPositionHandler);
            SyncPlayerAccountHandler syncPlayerAccountHandler = new SyncPlayerAccountHandler();
            HandlerDict.Add(syncPlayerAccountHandler.OpCode, syncPlayerAccountHandler);
            AttackCommandHandler attackCommandHandler = new AttackCommandHandler();
            HandlerDict.Add(attackCommandHandler.OpCode, attackCommandHandler);
            AttackDamageHandler attackDamageHandler = new AttackDamageHandler();
            HandlerDict.Add(attackDamageHandler.OpCode, attackDamageHandler);
            ChooseAvaterHandler chooseAvaterHandler = new ChooseAvaterHandler();
            HandlerDict.Add(chooseAvaterHandler.OpCode, chooseAvaterHandler);
        }

        private void InitCache()
        {
            OnlineAccountCache onlineAccountCache = new OnlineAccountCache();
            onlineAccountCache.InitCache();
            OnlineAttackCache onlineAttackCache = new OnlineAttackCache();
            onlineAttackCache.InitCache();
        }

        private void InitSystem()
        {
            OnlineAttackSystem onlineAttackSystem = new OnlineAttackSystem();
            onlineAttackSystem.InitSystem();
        }

        #region Threads
        SyncPlayerTransformThreads syncPlayerTransformThreads = new SyncPlayerTransformThreads();
        #endregion

        private void InitThreads()
        {
            syncPlayerTransformThreads.Run();
        }

        private void CleanThreads()
        {
            syncPlayerTransformThreads.Stop();
        }
    }
}
