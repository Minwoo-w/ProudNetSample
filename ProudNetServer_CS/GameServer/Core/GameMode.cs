using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using GameServer.Core;

namespace GameServer.Core
{
    public class GameMode
    {
        protected List<Actor> entitys = new List<Actor>();

        public event Action<Actor> OnCreateActor = null;
        public event Action<Actor> OnDestoryActor = null;

        public GameMode()
        {
            Awake();
        }

        protected virtual void Awake() { }

        public void UpdateGameMode(float deltaTime)
        {
            UpdateEntity(deltaTime);
            Update(deltaTime);
        }

        protected virtual void Update(float deltaTime) { }
        private void UpdateEntity(float deltaTime)
        {
            for (int i = 0; i < entitys.Count; i++)
            {
                entitys[i].Update(deltaTime);
            }
        }

        public T CreateActor<T>(bool bPublic = true) where T : Actor, new()
        {
            T instance = new T();
            instance.bPublic = bPublic;
            entitys.Add(instance);
            if (OnCreateActor != null) OnCreateActor.Invoke(instance);
            return instance;
        }

        public Actor CreateActor(bool bPublic = true)
        {
            return CreateActor<Actor>(bPublic);
        }

        public void DestoryActor(Actor entity)
        {
            if (entity == null)
                return;

            entitys.Remove(entity);
            entity.Destory();
            if (OnDestoryActor != null) OnDestoryActor.Invoke(entity);
        }

        public Actor FindActor(int id)
        {
            // Exception
            if (id <= 0)
                return null;

            Actor result = null;
            for (int i = 0; i < entitys.Count; i++)
            {
                if (entitys[i] != null && entitys[i].id == id)
                {
                    result = entitys[i];
                    break;
                }
            }
            return result;
        }

        public List<Actor> GetActorAll()
        {
            return entitys;
        }
    }
}
