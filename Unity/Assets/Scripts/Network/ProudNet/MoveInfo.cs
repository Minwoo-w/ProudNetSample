using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using Vector3 = UnityEngine.Vector3;

namespace Core
{
    public class MoveInfo
    {
        public int id = 0;
        public Vector3 position = Vector3.zero;
    }

    public class MoveInfoMarshaler : Nettention.Proud.Marshaler
    {
        private static Queue<MoveInfo> moveQueue = new Queue<MoveInfo>();
        private static List<MoveInfo> moveList = new List<MoveInfo>();

        public static bool Read(Message msg, out List<MoveInfo> infos)
        {
            infos = moveList;
            infos.Clear();

            // Get Size
            int size = 0;
            if (!msg.ReadScalar(ref size))
            {
                return false;
            }

            // Read Msg
            for (int i = 0; i < size; i++)
            {
                if (moveQueue.Count <= 0)
                {
                    moveQueue.Enqueue(new MoveInfo());
                }

                MoveInfo s = moveQueue.Dequeue();
                // ID
                if (!msg.Read(out s.id))
                {
                    return false;
                }

                // Transform
                if (!msg.Read(out float x))
                {
                    return false;
                }
                if (!msg.Read(out float y))
                {
                    return false;
                }
                if (!msg.Read(out float z))
                {
                    return false;
                }
                s.position = new Vector3(x, y, z);
                infos.Add(s);
                s = null;
            }

            for (int i = 0; i < infos.Count; i++)
            {
                moveQueue.Enqueue(infos[i]);
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
