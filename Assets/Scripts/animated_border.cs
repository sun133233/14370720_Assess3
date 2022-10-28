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
    Vector3[] positions = { new Vector3(-31.0f, 17.0f, 0.0f), new Vector3(31.0f, 17f, 0.0f), new Vector3(31.0f, -17.0f, 0.0f),new Vector3(-31.0f, -17.0f, 0.0f) };
    public int currentPos;
    // Start is called before the first frame update
    void Start()
    {
       
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
