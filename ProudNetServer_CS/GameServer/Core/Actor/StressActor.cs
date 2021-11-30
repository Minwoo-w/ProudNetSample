using Core;

namespace GameServer.Core
{
    public class StressActor : Actor
    {
        private Vector3 targetPosition = Vector3.zero;
        private float arriveDistance = 0.2f;
        private float speed = 1.0f;

        public override void Update(float deltaTime)
        {
            Vector3 scalar = targetPosition - transform.position;
            float scalarMagnitude = scalar.Magnitude();
            Vector3 direction = scalar.Normalrize();

            float moveMagnitude = speed * deltaTime;
            Vector3 moveVector = direction * moveMagnitude;
            if (moveMagnitude < scalarMagnitude)
            {
                transform.position += moveVector;
            }
            else
            {
                transform.position = targetPosition;
            }

            if (scalar.Magnitude() < arriveDistance)
            {
                float range = 5.0f;
                float x = Utility.Random.RandomRange(-range, range);
                float z = Utility.Random.RandomRange(-range, range);
                targetPosition = new Vector3(x, 0.0f, z);
            }
        }
    }
}
