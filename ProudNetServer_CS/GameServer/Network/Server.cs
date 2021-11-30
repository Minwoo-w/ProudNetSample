using System;
using System.Collections.Generic;
using System.Threading;

using Nettention.Proud;

using Common;
using Core;
using GameServer.Core;
using GameServer.Network;
using Vector3 = Core.Vector3;

namespace GameServer
{
    public class Server
    {
        private NetServer server = null;
        private S2C.Proxy proxy = new S2C.Proxy();
        private C2S.Stub stub = new C2S.Stub();

        private GameMode gameMode = null;
        private Dictionary<HostID, Player> playerDctn = new Dictionary<HostID, Player>();

        private RmiContext defaultContext = RmiContext.ReliableSend;
        private RmiContext movementContext = new RmiContext(MessagePriority.MessagePriority_High, MessageReliability.MessageReliability_Reliable, EncryptMode.EM_None);
        private RmiContext unrealiableContext = new RmiContext(MessagePriority.MessagePriority_Medium, MessageReliability.MessageReliability_Unreliable, EncryptMode.EM_None);

        public Server()
        {
            Init();
        }

        private void Init()
        {
            server = new NetServer();

            // event
            server.ClientJoinHandler += OnClientJoin;
            server.ClientLeaveHandler += OnClientLeave;
            stub.MoveTo = ResponseMoveTo;
            stub.CheckLatency = CheckLatencyResponse;

            StartServerParameter param = new StartServerParameter();
            //param.localNicAddr = Vars.SERVER_IP;
            param.tcpPorts.Add(Vars.SERVER_PORT);
            param.protocolVersion = new Nettention.Proud.Guid(Vars.PROTOCOL_VERSION);

            param.enableNagleAlgorithm = false;
            param.enableP2PEncryptedMessaging = true;

            // Web Scoekt
            param.m_webSocketParam.webSocketType = WebSocketType.Ws;
            param.m_webSocketParam.endpoint = "^/echo/?$";
            param.m_webSocketParam.listenPort = Vars.WEB_SERVER_PORT;
            param.m_webSocketParam.threadCount = 4;
            if (param.m_webSocketParam.webSocketType == WebSocketType.Wss)
            {
                param.m_webSocketParam.certFile = "/etc/httpd/ssl/STAR.rsup.io_cert.pem";
                param.m_webSocketParam.privateKeyFile = "/etc/httpd/ssl/STAR.rsup.io_key.pem";
            }

            server.AttachProxy(proxy);
            server.AttachStub(stub);

            server.SetDirectP2PStartCondition(DirectP2PStartCondition.DirectP2PStartCondition_Always);

            try
            {
                server.Start(param);
                StartGame();
            }
            catch (Exception e)
            {

            }
        }

        public void Dispose()
        {
            if (server != null)
                server.Dispose();
        }

        private void OnClientJoin(NetClientInfo clientInfo)
        {
            if (gameMode != null && server != null)
            {
                Player player = new Player();
                player.hostID = clientInfo.hostID;
                player.SetActor(gameMode.CreateActor(false));
                playerDctn.Add(clientInfo.hostID, player);

                // 모든 플레이어에게 생성됨을 브로드캐스트
                foreach (var host in playerDctn)
                {
                    // Self
                    if (host.Key == player.hostID)
                    {
                        // Send Target Host, RMI, Join Client HostID, Join Clinet Entity Info
                        proxy.JoinClient(player.hostID, defaultContext, (int)player.hostID, player.ControlledActor.ID);

                        // Other Entitys
                        List<Actor> entitys = gameMode.GetActorAll();
                        for (int entityCount = 0; entityCount < entitys.Count; entityCount++)
                        {
                            proxy.EntityAppear(player.hostID, defaultContext,
                                entitys[entityCount].ID,
                                entitys[entityCount].bPublic,
                                entitys[entityCount].transform.position.x,
                                entitys[entityCount].transform.position.y,
                                entitys[entityCount].transform.position.z
                                );
                        }
                    }
                }
            }
        }

        private void OnClientLeave(NetClientInfo clientInfo, ErrorInfo errorinfo, ByteArray comment)
        {
            if (gameMode != null && server != null)
            {
                Player player = null;
                if (playerDctn.TryGetValue(clientInfo.hostID, out player))
                {
                    proxy.EntityDisappear(server.GetClientHostIDs(), defaultContext, player.ControlledActor.ID);
                    gameMode.DestoryActor(player.ControlledActor);
                    playerDctn.Remove(clientInfo.hostID);
                }
            }
        }

        private bool ResponseMoveTo(HostID remote, RmiContext rmiContext, List<MoveInfo> infos)
        {
            if (gameMode != null && server != null)
            {
                for (int i = 0; i < infos.Count; i++)
                {
                    Actor actor = gameMode.FindActor(infos[i].id);
                    if (actor != null)
                    {
                        actor.transform.position = new Vector3(infos[i].position.x, infos[i].position.y, infos[i].position.z);
                    }
                }
            }
            return true;
        }

        private bool CheckLatencyResponse(HostID remote, RmiContext rmiContext, long prevTime)
        {
            proxy.CheckLatency(remote, defaultContext, prevTime);
            return true;
        }

        private void StartGame()
        {
            gameMode = new StressTestGameMode();
            gameMode.OnCreateActor += (entity) => {
                proxy.EntityAppear(server.GetClientHostIDs(), defaultContext,
                                entity.ID,
                                entity.bPublic,
                                entity.transform.position.x,
                                entity.transform.position.y,
                                entity.transform.position.z
                                );
            };

            movementContext.uniqueID = 10001; // Movement Packet UniqueID

            while(true)
            {
                if (gameMode != null && server != null)
                {
                    gameMode.UpdateGameMode(0.5f);

                    // Update Actor
                    List<Actor> entitys = gameMode.GetActorAll();
                    List<MoveInfo> infos = new List<MoveInfo>();
                    for (int i = 0; i < entitys.Count; i++)
                    {
                        //if (entitys[i].bPublic)
                        {
                            MoveInfo info = new MoveInfo();
                            info.id = entitys[i].ID;
                            info.position = entitys[i].transform.position;
                            infos.Add(info);
                        }
                    }

                    if (infos.Count > 0)
                    {
                        proxy.MoveTo(server.GetClientHostIDs(), movementContext, infos);
                    }
                }
                Thread.Sleep(250);
            }

            // Game
            // Network
        }
    }
}
