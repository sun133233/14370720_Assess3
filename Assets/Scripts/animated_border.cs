using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animated_border : MonoBehaviour
{

    public Transform PacStudent;
    public Transform cherry;
    float duration = 2.5f;
    float duration_a = 2.0f;
    float duration_b = 3f;
    float timeOut = 0.0f;
    Vector3[] positions;
    public int currentPos;
    public Vector2 Difference = new Vector2(130, 80);
    // Start is called before the first frame update
    void Start()
    {
        Vector2 camHW = new Vector2(Screen.width, Screen.height);
        positions = new Vector3[]
        {
            new Vector3 (Difference.x,camHW.y-Difference.y),new Vector3(camHW.x-Difference.x,camHW.y-Difference.y),
           new Vector3(camHW.x-Difference.x,Difference.y), new Vector3(Difference.x,Difference.y)
        };
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Camera.main.ScreenToWorldPoint(positions[i]);
            positions[i].z = 0;
        }

        PacStudent.transform.position = new Vector3(-31.0f, 17f, 0.0f);
        cherry.transform.position = new Vector3(-25.0f, 17f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (timeOut < duration)
        {
            int nextPos = currentPos + 1;

            if (nextPos == positions.Length)
            {
                nextPos = 0;
            }
            PacStudent.transform.position = Vector3.Lerp(positions[currentPos], positions[nextPos], timeOut / duration_b);
            cherry.transform.position = Vector3.Lerp(positions[currentPos], positions[nextPos], timeOut / duration_a);
            timeOut += Time.deltaTime;
          
        }
        else
        {
            timeOut = 0.0f;
            currentPos += 1;
            if (currentPos == positions.Length)
            {
                currentPos = 0;
            }
        }
    }
}
