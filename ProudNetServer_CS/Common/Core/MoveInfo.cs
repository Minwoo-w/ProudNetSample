using Nettention.Proud;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class MoveInfo
    {
        public int id = 0;
        public Vector3 position = Vector3.zero;
    }

    public class MoveInfoMarshaler : Nettention.Proud.Marshaler
    {
        public static bool Read(Message msg, out List<MoveInfo> infos)
        {
            infos = null;

            // Get Size
            int size = 0;
            if (!msg.ReadScalar(ref size))
            {
                return false;
            }

            // Read Msg
            infos = new List<MoveInfo>();
            for (int i = 0; i < size; i++)
            {
                MoveInfo s = new MoveInfo();
                // ID
                if (!msg.Read(out s.id))
                {
                    return false;
                }

                // Transform
                Vector3 position = new Vector3();
                if (!msg.Read(out position.x))
                {
                    return false;
                }
                if (!msg.Read(out position.y))
                {
                    return false;
                }
                if (!msg.Read(out position.z))
                {
                    return false;
                }
                s.position = position;

                infos.Add(s);
            }

            return true;
        }

        public static void Write(Message msg, List<MoveInfo> infos)
        {
            msg.WriteScalar(infos.Count);
            for (int i = 0; i < infos.Count; i++)
            {
                msg.Write(infos[i].id);
                msg.Write(infos[i].position.x);
                msg.Write(infos[i].position.y);
                msg.Write(infos[i].position.z);
            }
        }
    }
}
