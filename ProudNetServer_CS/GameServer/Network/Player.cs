using System;
using System.Collections.Generic;
using System.Text;
using Nettention.Proud;
using GameServer.Core;

namespace GameServer.Network
{
    public class Player
    {
        public long tickCorrecionValue = 0; // Tick 시간 보정값
        public HostID hostID = HostID.HostID_None; // 호스트 ID
        public Actor ControlledActor { get; private set; } // 컨트롤중인 Entity

        public void SetActor(Actor newActor)
        {
            if (ControlledActor != null)
                ControlledActor.bPublic = true;

            ControlledActor = newActor;
            if (ControlledActor != null)
                ControlledActor.bPublic = false;
        }
    }
}
