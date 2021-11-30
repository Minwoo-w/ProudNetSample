using System;
using System.Collections.Generic;
using System.Text;
using Core;

namespace GameServer.Core
{
    public class Actor : Entity
    {
        // ID
        public int ID { get { return id; } }
        private static int idSequence = 0;
        public bool bPublic = true;

        public Actor()
        {
            id = idSequence++;
            transform = new Transform();
            Awake();
        }

        public virtual void Awake() { }
        public virtual void Update(float deltaTime) { }
        public virtual void Destory() { }
    }
}
