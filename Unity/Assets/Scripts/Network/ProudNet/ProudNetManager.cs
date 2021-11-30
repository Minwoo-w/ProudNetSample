using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using UnityEngine.Serialization;
using UnityEngine.UI;

using Vector3 = UnityEngine.Vector3;
using UnityEngine.Profiling;
using Core;
using System.Text;

public class ProudNetManager : MonoBehaviour
{
    // ToDo
    // - Client에서도 s2cProxy.cs와 c2sStub.cs가 필요할까?
    // - 보안적인 측면에서 보편적으로 서버에서 보내는, 받는 코드는 사용하지 않는 것으로 알고 있음

    private object _moveLock = new object();

    private bool bConnectedServer = false;
    private const ushort SERVER_PORT = 17788;
    private const ushort WEB_SERVER_PORT = SERVER_PORT - 1000;
    private C2S.Proxy _c2sProxy = new C2S.Proxy();
    private S2C.Stub _s2cStub = new S2C.Stub();

    private NetClient netClient = null;
    private int localEntityID = -1;

    private RmiContext defaultContext = new RmiContext(MessagePriority.MessagePriority_Low, MessageReliability.MessageReliability_Reliable, EncryptMode.EM_None);
    private RmiContext movementContext = new RmiContext(MessagePriority.MessagePriority_Low, MessageReliability.MessageReliability_Reliable, EncryptMode.EM_None);

    // Temp
    [Header(("Test Params"))] 
    private List<Actor> actors = new List<Actor>();
    public GameObject loginPanel = null;
    public InputField loginField = null;
    public InputField portField = null;
    public Text latencyText = null;
    private float deltaTime = 0.0f;

    private float updateTimer = 0.0f;

    private void Start()
    {
        netClient = new NetClient();

        // Event
        netClient.JoinServerCompleteHandler = OnJoinServerCompleted;
        netClient.ExceptionHandler = OnExceptionHandler;
        netClient.ErrorHandler = OnErrorHandler;

        _s2cStub.JoinClient = OnJoinClient;
        _s2cStub.LeaveClient = OnLeaveClient;
        _s2cStub.EntityAppear = OnEntityAppear;
        _s2cStub.EntityDisappear = OnEntityDisappear;
        _s2cStub.MoveTo = OnMoveTo;
        _s2cStub.CheckLatency = CheckLatencyAck;

        // Proxy 및 Stub Attch
        netClient.AttachProxy(_c2sProxy);
        netClient.AttachStub(_s2cStub);

        movementContext.uniqueID = 10002;

#if !UNITY_EDITOR
        TryLogin();
#endif
    }

    private void OnExceptionHandler(HostID remoteID, Exception e)
    {
        latencyText.text = e.Message;
    }

    private void OnErrorHandler(ErrorInfo errorInfo)
    {
        latencyText.text = errorInfo.comment;
    }

    public void TryLogin()
    {
        // Try Connect to Server
        NetConnectionParam param = new NetConnectionParam();

#if UNITY_EDITOR
        param.serverIP = loginField.text;
        param.serverPort = ushort.Parse(portField.text);
#elif !UNITY_EDITOR && UNITY_STANDALONE
        param.serverIP = $"127.0.0.1";
        param.serverPort = 17326;
#elif !UNITY_EDITOR && UNITY_WEBGL
        param.serverIP = $"ws://10.1.102.56:{16326}/echo";
        param.serverPort = 16326;
#endif

        System.Guid guid = new System.Guid("{ 0xafa3c0c, 0x77d7, 0x4b74, { 0x9d, 0xdb, 0x1c, 0xb3, 0xd2, 0x5e, 0x1e, 0x64 } }");
        param.protocolVersion = new Nettention.Proud.Guid();
        param.protocolVersion.Set(guid);

        // param.userWorkerThreadModel = ThreadModel.ThreadModel_MultiThreaded;
        // param.netWorkerThreadModel = ThreadModel.ThreadModel_MultiThreaded;

        netClient.Connect(param);

        Debug.Log($"TryLogin For - {param.serverIP}, {param.serverPort}");
    }

    private List<MoveInfo> moveInfoList = new List<MoveInfo>();
    private void Update()
    {
        deltaTime += Time.deltaTime;
        if (bConnectedServer)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.5f)
            {
                updateTimer -= 0.5f;
                // Sync Position
                Actor selfActor = GameMode.Instance.GetActorByID(localEntityID);
                if (selfActor != null)
                {
                    Vector3 actorPos = selfActor.transform.position;
                    moveInfoList.Clear();
                    moveInfoList.Add(new MoveInfo() { id = selfActor.id, position = selfActor.transform.position });
                    _c2sProxy.MoveTo(HostID.HostID_Server, movementContext, moveInfoList);
                    selfActor = null;
                }

                UpdateDebug();
                deltaTime = 0.0f;
            }

            GC.Collect();
        }

        netClient.FrameMove();
    }

    private void OnDestroy()
    {
        netClient.Disconnect();
    }

    private void OnJoinServerCompleted(ErrorInfo info, ByteArray replyFromServer)
    {
        if (info.errorType == ErrorType.Ok)
        {
            loginPanel.SetActive(false);
            bConnectedServer = true;
            Debug.Log($"Join Server Success");
        }
        else
        {
            latencyText.text = info.comment;
            Debug.LogError(info.comment);
        }
    }

    private HostID GetLocalHostID()
    {
        return netClient.LocalHostID;
    }

    private int GetLocalHostIDInt()
    {
        return (int)GetLocalHostID();
    }

#region Stub
    private Actor tempActor = null;
    private bool OnJoinClient(HostID remote, RmiContext rmiContext, int hostID, int entityID)
    {
        if (GetLocalHostIDInt() == hostID)
        {
            localEntityID = entityID;

            Actor actor = GameMode.Instance.GetActorByID(localEntityID);
            if (actor != null && actor.id == localEntityID)
            {
                actor.SetIsMine(true);
            }
        }
        return true;
    }

    private bool OnLeaveClient(HostID remote, RmiContext rmiContext, int hostID, int entityID)
    {
        GameMode.Instance.DestroyActor(entityID);
        return true;
    }

    private bool OnEntityAppear(HostID remote, RmiContext rmiContext, int entityID, bool bPublic, float px, float py, float pz)
    {
        if (GameMode.Instance.GetActorByID(entityID) == null)
        {
            Actor actor = null;
            if (bPublic)
            {
                actor = GameMode.Instance.CreateActor(entityID, "DummyActor");
            }
            else
            {
                actor = GameMode.Instance.CreateActor(entityID, "Player");
            }

            if (actor != null)
            {
                actor.MoveTo(new Vector3(px, py, pz));
            }
        }

        return true;
    }

    private bool OnEntityDisappear(HostID remote, RmiContext rmiContext, int entityID)
    {
        GameMode.Instance.DestroyActor(entityID);
        return true;
    }

    private bool OnMoveTo(HostID remote, RmiContext rmiContext, List<MoveInfo> infos)
    {
        lock (_moveLock)
        {
            for (int i = 0; i < infos.Count; i++)
            {
                tempActor = GameMode.Instance.GetActorByID(infos[i].id);
                if (tempActor != null)
                {
                    tempActor.MoveTo(new Vector3(infos[i].position.x, infos[i].position.y, infos[i].position.z));
                }
            }
            tempActor = null;
            infos = null;
        }
        return true;
    }
#endregion

    public void UpdateDebug()
    {
        StringBuilder str = new StringBuilder();
        str.Append($"Latency : {GetLatency()}\n");
        str.Append($"Actor Count : {GameMode.Instance.GetActorCount()}\n");
        str.Append($"Delta Time : {deltaTime}\n");
        str.Append($"Duration Time : {Time.time}\n");
        str.Append($"MonoHeapSize : {Profiler.GetMonoHeapSizeLong()}");
        latencyText.text = str.ToString();
    }

    // Ping
    private void CheckDebug()
    {
        _c2sProxy.CheckLatency(HostID.HostID_Server, defaultContext, DateTime.Now.Ticks);
    }

    private bool CheckLatencyAck(HostID remote, RmiContext rmiContext, long prevTime)
    {
        long latency = (DateTime.Now.Ticks - prevTime) / 10000;
        AddLatency(latency);
        return true;
    }

    private List<long> latencyList = new List<long>();
    private void AddLatency(long value)
    {
        if (latencyList.Count > 120)
        {
            latencyList.RemoveAt(0);
        }
        latencyList.Add(value);
    }

    private long GetLatency()
    {
        long avg = 0;
        if (latencyList.Count > 0)
        {
            for (int listCount = 0; listCount < latencyList.Count; listCount++)
            {
                avg += latencyList[listCount];
            }
            avg = avg / latencyList.Count;
        }
        return avg;
    }
}
