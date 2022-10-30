using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;
    public Vector3 appear;
    public Vector3 TriggerEnter()
    {
        return target.position+target.GetComponent<Teleport>().appear;
    }

}
