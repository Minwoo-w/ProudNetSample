using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Actor
{
    // Temp
    public float speed = 5.0f;
    public Material selfMaterial = null;
    public Material otherMaterial = null;
    public Text labelText = null;

    private void Update()
    {
        if (bMine)
        {
            if (Input.GetKeyDown(KeyCode.A)) targetDirection += Vector3.left;
            else if (Input.GetKeyUp(KeyCode.A)) targetDirection -= Vector3.left;

            if (Input.GetKeyDown(KeyCode.D)) targetDirection += Vector3.right;
            else if (Input.GetKeyUp(KeyCode.D)) targetDirection -= Vector3.right;

            if (Input.GetKeyDown(KeyCode.W)) targetDirection += Vector3.forward;
            else if (Input.GetKeyUp(KeyCode.W)) targetDirection -= Vector3.forward;

            if (Input.GetKeyDown(KeyCode.S)) targetDirection += Vector3.back;
            else if (Input.GetKeyUp(KeyCode.S)) targetDirection -= Vector3.back;

            targetPosition = transform.position + targetDirection;

            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            Vector3 scalar = targetPosition - transform.position;
            Vector3 direction = scalar.normalized;
            float restRange = scalar.magnitude;
            float moveRange = speed * Time.deltaTime;
            if (restRange > moveRange)
                transform.position += direction * speed * Time.deltaTime;
            else
                transform.position = targetPosition;
        }

        if (labelText != null)
        {
            labelText.text = id.ToString();
        }
    }

    public override void MoveTo(Vector3 position)
    {
        base.MoveTo(position);
        targetPosition = position;
    }

    public override void SetIsMine(bool b)
    {
        base.SetIsMine(b);
        Material material = (bMine) ? selfMaterial : otherMaterial;
        GetComponent<MeshRenderer>().material = material;
    }
}
