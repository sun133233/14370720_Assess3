using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{
    private void Reset()
    {
        foreach (Transform item in transform)
        {

            if (item.name.Contains("sprite"))
            {
                item.gameObject.AddComponent<PolygonCollider2D>();
            }
            if (item.name.Contains("sprite_0"))
            {
                DestroyImmediate(item.gameObject.GetComponent<PolygonCollider2D>());
            }
        }

    }
}
