using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nettention.Proud;
using Vector3 = UnityEngine.Vector3;

public class Actor : MonoBehaviour
{
    protected bool bMine = false;
    protected bool bInitMovement = false;
    protected Vector3 prevPosition = Vector3.zero;
    protected Vector3 targetPosition = Vector3.zero;
    protected Vector3 targetDirection = Vector3.zero;
    
    public int id; // 고유번호

    public virtual void MoveTo(Vector3 position)
    {
        if (bInitMovement == false)
        {
            prevPosition = transform.position;
            transform.position = targetPosition;
            bInitMovement = true;
        }
    }

    public virtual void SetIsMine(bool b)
    {
        bMine = b;
        if (Camera.main != null)
        {
            Camera.main.transform.SetParent(transform, false);
        }
    }
}
