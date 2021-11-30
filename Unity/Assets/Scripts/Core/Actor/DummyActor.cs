using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyActor : Actor
{
    public float speed = 1.0f;

    private void Start()
    {
        //StartCoroutine(UpdateRandomDirection());
    }

    private IEnumerator UpdateRandomDirection()
    {
        while (true)
        {
            targetDirection.x = Random.Range(-1.0f, 1.0f);
            targetDirection.z = Random.Range(-1.0f, 1.0f);
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }

    //private void Update()
    //{
    //    //targetPosition = transform.position + targetDirection;
    //    //transform.position = Vector3.Lerp(transform.position, targetPosition, 2.0f * Time.deltaTime);

    //    Vector3 scalar = targetPosition - transform.position;
    //    Vector3 direction = scalar.normalized;
    //    transform.position += direction * speed * Time.deltaTime;
    //}

    public override void MoveTo(Vector3 position)
    {
        //base.MoveTo(position);

        //targetPosition = position;
        //transform.position = position;
    }
}
