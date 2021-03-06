




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace S2C
{
	public class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool JoinClientDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, int entityID);  
		public JoinClientDelegate JoinClient = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, int entityID)
		{ 
			return false;
		};
		public delegate bool LeaveClientDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, int entityID);  
		public LeaveClientDelegate LeaveClient = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, int entityID)
		{ 
			return false;
		};
		public delegate bool EntityAppearDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int entityID, bool bPublic, float px, float py, float pz);  
		public EntityAppearDelegate EntityAppear = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int entityID, bool bPublic, float px, float py, float pz)
		{ 
			return false;
		};
		public delegate bool EntityDisappearDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int entityID);  
		public EntityDisappearDelegate EntityDisappear = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int entityID)
		{ 
			return false;
		};
		public delegate bool MoveToDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.Collections.Generic.List<Core.MoveInfo> infos);  
		public MoveToDelegate MoveTo = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.Collections.Generic.List<Core.MoveInfo> infos)
		{ 
			return false;
		};
		public delegate bool CheckLatencyDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, long prevTick);  
		public CheckLatencyDelegate CheckLatency = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, long prevTick)
		{ 
			return false;
		};
		public delegate bool EntityCountDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int amount);  
		public EntityCountDelegate EntityCount = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int amount)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.JoinClient:
            ProcessReceivedMessage_JoinClient(__msg, pa, hostTag, remote);
            break;
        case Common.LeaveClient:
            ProcessReceivedMessage_LeaveClient(__msg, pa, hostTag, remote);
            break;
        case Common.EntityAppear:
            ProcessReceivedMessage_EntityAppear(__msg, pa, hostTag, remote);
            break;
        case Common.EntityDisappear:
            ProcessReceivedMessage_EntityDisappear(__msg, pa, hostTag, remote);
            break;
        case Common.MoveTo:
            ProcessReceivedMessage_MoveTo(__msg, pa, hostTag, remote);
            break;
        case Common.CheckLatency:
            ProcessReceivedMessage_CheckLatency(__msg, pa, hostTag, remote);
            break;
        case Common.EntityCount:
            ProcessReceivedMessage_EntityCount(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_JoinClient(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int hostID; Core.MoveInfoMarshaler.Read(__msg,out hostID);	
int entityID; Core.MoveInfoMarshaler.Read(__msg,out entityID);	
core.PostCheckReadMessage(__msg, RmiName_JoinClient);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=hostID.ToString()+",";
parameterString+=entityID.ToString()+",";
        NotifyCallFromStub(Common.JoinClient, RmiName_JoinClient,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoinClient;
        summary.rmiName = RmiName_JoinClient;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoinClient (remote,ctx , hostID, entityID );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoinClient);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoinClient;
        summary.rmiName = RmiName_JoinClient;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_LeaveClient(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int hostID; Core.MoveInfoMarshaler.Read(__msg,out hostID);	
int entityID; Core.MoveInfoMarshaler.Read(__msg,out entityID);	
core.PostCheckReadMessage(__msg, RmiName_LeaveClient);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=hostID.ToString()+",";
parameterString+=entityID.ToString()+",";
        NotifyCallFromStub(Common.LeaveClient, RmiName_LeaveClient,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.LeaveClient;
        summary.rmiName = RmiName_LeaveClient;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =LeaveClient (remote,ctx , hostID, entityID );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_LeaveClient);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.LeaveClient;
        summary.rmiName = RmiName_LeaveClient;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_EntityAppear(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int entityID; Core.MoveInfoMarshaler.Read(__msg,out entityID);	
bool bPublic; Core.MoveInfoMarshaler.Read(__msg,out bPublic);	
float px; Core.MoveInfoMarshaler.Read(__msg,out px);	
float py; Core.MoveInfoMarshaler.Read(__msg,out py);	
float pz; Core.MoveInfoMarshaler.Read(__msg,out pz);	
core.PostCheckReadMessage(__msg, RmiName_EntityAppear);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=entityID.ToString()+",";
parameterString+=bPublic.ToString()+",";
parameterString+=px.ToString()+",";
parameterString+=py.ToString()+",";
parameterString+=pz.ToString()+",";
        NotifyCallFromStub(Common.EntityAppear, RmiName_EntityAppear,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.EntityAppear;
        summary.rmiName = RmiName_EntityAppear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =EntityAppear (remote,ctx , entityID, bPublic, px, py, pz );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_EntityAppear);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.EntityAppear;
        summary.rmiName = RmiName_EntityAppear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_EntityDisappear(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int entityID; Core.MoveInfoMarshaler.Read(__msg,out entityID);	
core.PostCheckReadMessage(__msg, RmiName_EntityDisappear);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=entityID.ToString()+",";
        NotifyCallFromStub(Common.EntityDisappear, RmiName_EntityDisappear,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.EntityDisappear;
        summary.rmiName = RmiName_EntityDisappear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =EntityDisappear (remote,ctx , entityID );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_EntityDisappear);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.EntityDisappear;
        summary.rmiName = RmiName_EntityDisappear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_MoveTo(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        System.Collections.Generic.List<Core.MoveInfo> infos; 
        Core.MoveInfoMarshaler.Read(__msg, out infos);	
        core.PostCheckReadMessage(__msg, RmiName_MoveTo);
        if(enableNotifyCallFromStub==true)
        {
            string parameterString = "";
            parameterString+=infos.ToString()+",";
            NotifyCallFromStub(Common.MoveTo, RmiName_MoveTo,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.MoveTo;
        summary.rmiName = RmiName_MoveTo;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =MoveTo (remote,ctx , infos );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_MoveTo);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.MoveTo;
        summary.rmiName = RmiName_MoveTo;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_CheckLatency(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        long prevTick; Core.MoveInfoMarshaler.Read(__msg,out prevTick);	
core.PostCheckReadMessage(__msg, RmiName_CheckLatency);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=prevTick.ToString()+",";
        NotifyCallFromStub(Common.CheckLatency, RmiName_CheckLatency,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.CheckLatency;
        summary.rmiName = RmiName_CheckLatency;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =CheckLatency (remote,ctx , prevTick );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_CheckLatency);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.CheckLatency;
        summary.rmiName = RmiName_CheckLatency;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_EntityCount(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int amount; Core.MoveInfoMarshaler.Read(__msg,out amount);	
core.PostCheckReadMessage(__msg, RmiName_EntityCount);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=amount.ToString()+",";
        NotifyCallFromStub(Common.EntityCount, RmiName_EntityCount,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.EntityCount;
        summary.rmiName = RmiName_EntityCount;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =EntityCount (remote,ctx , amount );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_EntityCount);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.EntityCount;
        summary.rmiName = RmiName_EntityCount;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
		#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_JoinClient="JoinClient";
public const string RmiName_LeaveClient="LeaveClient";
public const string RmiName_EntityAppear="EntityAppear";
public const string RmiName_EntityDisappear="EntityDisappear";
public const string RmiName_MoveTo="MoveTo";
public const string RmiName_CheckLatency="CheckLatency";
public const string RmiName_EntityCount="EntityCount";
       
public const string RmiName_First = RmiName_JoinClient;
		#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_JoinClient="";
public const string RmiName_LeaveClient="";
public const string RmiName_EntityAppear="";
public const string RmiName_EntityDisappear="";
public const string RmiName_MoveTo="";
public const string RmiName_CheckLatency="";
public const string RmiName_EntityCount="";
       
public const string RmiName_First = "";
		#endif

		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

