using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPosition : MonoBehaviour
{

    public Quaternion CalculateShootRotation(Vector3 mousePosition)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition = transform.InverseTransformPoint(mousePosition);
        mousePosition.z = 0;
        mousePosition = mousePosition.normalized;
        float tmp = Vector3.SignedAngle(Vector3.right, mousePosition, Vector3.forward);
        return Quaternion.Euler(0, 0, tmp);
    }

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

}
