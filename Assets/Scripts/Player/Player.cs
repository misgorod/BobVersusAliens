using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }
	
}
