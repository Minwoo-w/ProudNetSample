using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace GameServer.Core
{
    public class StressTestGameMode : GameMode
    {
        private int stressTestEntityAmount = 2000;

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < stressTestEntityAmount; i++)
            {
                CreateActor<StressActor>();
            }
        }

        protected override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //for (int i = 0; i < entitys.Count; i++)
            //{
            //    Console.WriteLine(entitys[i].transform.position.ToString());
            //    //CreateEntity<StressEntity>();
            //}
        }
    }
}
