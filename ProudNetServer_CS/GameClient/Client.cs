using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettention.Proud;
using Common;

namespace GameClient
{
    public class Client
    {
        static object g_critSec = new object();

        private NetClient client = null;

        public Client()
        {
            client = new NetClient();

            // set to false to exit the main loop.
            bool keepWorkerThread = true;
            // set to true if server connection is established.
            bool isConnected = false;
            // changed if another P2P peer joined.
            HostID recentP2PGroupHostID = HostID.HostID_None;

            // set a routine which is run when the server connection attempt
            // is success or failed.
            client.JoinServerCompleteHandler = (info, replyFromServer) =>
            {
                // as here is running in 2nd thread, lock is needed for console print.
                lock (g_critSec)
                {
                    if (info.errorType == ErrorType.Ok)
                    {
                        Console.Write("Succeed to connect server. Allocated hostID={0}", client.GetLocalHostID());
                        isConnected = true;

                        // send a message.
                        //g_Proxy.Chat(HostID.HostID_Server, // send destination
                        //RmiContext.ReliableSend, // how to send
                        //"Hello ProudNet~!!!.", 333, 22.33f); // user defined parameters
                    }
                    else
                    {
                        // connection failure.
                        Console.Write("Failed to connect server.\n");
                        Console.WriteLine("errorType = {0}, detailType = {1}, comment = {2}", info.errorType, info.detailType, info.comment);
                    }
                }
            };

            // set a routine for network disconnection.
            client.LeaveServerHandler = (errorInfo) =>
            {
                // lock is needed as above.
                lock (g_critSec)
                {
                    // show why disconnected.
                    Console.Write("OnLeaveServer: {0}\n", errorInfo.comment);

                    // let main loop exit
                    isConnected = false;
                    keepWorkerThread = false;
                }
            };

            // set a routine for P2P member join (P2P available)
            client.P2PMemberJoinHandler = (memberHostID, groupHostID, memberCount, customField) =>
            {
                // lock is needed as above.
                lock (g_critSec)
                {
                    // memberHostID = P2P connected client ID
                    // groupHostID = P2P group ID where the P2P peer is in.
                    Console.Write("[Client] P2P member {0} joined group {1}.\n", memberHostID, groupHostID);

                    //g_Proxy.P2PChat(memberHostID, RmiContext.ReliableSend,
                    //"Welcome!", 5, 7);
                    recentP2PGroupHostID = groupHostID;
                }
            };

            // called when a P2P member left.
            client.P2PMemberLeaveHandler = (memberHostID, groupHostID, memberCount) =>
            {
                Console.Write("[Client] P2P member {0} left group {1}.\n", memberHostID, groupHostID);
            };

            // attach RMI proxy and stub to client object.
            //client.AttachProxy(g_Proxy);	// Client-to-server =>
            //client.AttachStub(g_Stub);		// server-to-client <=

            NetConnectionParam cp = new NetConnectionParam();
            cp.serverIP = "127.0.0.1";
            cp.serverPort = Vars.SERVER_PORT;
            cp.protocolVersion.Set(Vars.PROTOCOL_VERSION);

            // Starts connection.
            // This function returns immediately.
            // Meanwhile, connection attempt is process in background
            // and the result is notified by OnJoinServerComplete event.
            if (client.Connect(cp) == false)
            {
                return;
            }

            // As we have to be notified events and message receivings,
            // We call FrameMove function for every short interval.
            // If you are developing a game, call FrameMove
            // without doing any sleep.
            // If you are developing an app, call FrameMove
            // in another thread or your timer callback routine.
            Thread workerThread = new Thread(() =>
            {
                while (keepWorkerThread)
                {
                    // Prevent CPU full usage.
                    Thread.Sleep(10);

                    // process received RMI and events.
                    client.FrameMove();
                }
            });

            workerThread.Start();

            Console.Write("a: Send a P2P message to current P2P group members except for self.\n");
            Console.Write("q: Exit.\n");

            //while (keepWorkerThread)
            //{
            //    // get user input
            //    string userInput = Console.ReadLine();

            //    // received user command. process it.
            //    if (userInput == "q")
            //    {
            //        // let worker thread exit.
            //        keepWorkerThread = false;
            //    }
            //    else if (userInput == "a")
            //    {
            //        if (isConnected)
            //        {
            //            // As we access recentP2PGroupHostID which is also accessed by
            //            // another thread, lock it.
            //            lock (g_critSec)
            //            {
            //                // Sends a P2P message to everyone in a group
            //                // specified at recentP2PGroupHostID.
            //                //RmiContext sendHow = RmiContext.ReliableSend;
            //                //sendHow.enableLoopback = false; // don't sent to myself.
            //                //g_Proxy.P2PChat(recentP2PGroupHostID, sendHow,
            //                //"Welcome ProudNet!!", 1, 1);
            //            }
            //        }
            //        else
            //        {
            //            // We have no server connection. Show error.
            //            Console.Write("Not yet connected.\n");
            //        }
            //    }
            //}

            //// Waits for 2nd thread exits.
            //workerThread.Join();

            //// Disconnects.
            //// Note: deleting NetClient object automatically does it.
            //client.Disconnect();
        }
    }
}
