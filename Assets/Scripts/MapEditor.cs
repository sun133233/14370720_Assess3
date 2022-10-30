using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    private void Reset()
    {
        GameObject[] obj= GameObject.FindGameObjectsWithTag("fish");
        foreach (GameObject item in obj)
        {
            item.GetComponent<BoxCollider2D>().size = Vector2.one;
        }
    }
}
