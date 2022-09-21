using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    private Animator PacController;
    public Transform PacStudent;
    float duration = 3.0f;
    float timeOut = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        PacController = GetComponent<Animator>();
        PacStudent.transform.position = new Vector3(-4.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && timeOut<duration)
        {
            PacStudent.transform.position = Vector3.Lerp(new Vector3(-4.0f, 0.0f, 0.0f), new Vector3(-4.0f, 3.0f, 0.0f), timeOut / duration);
            timeOut += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && timeOut < duration)
        {
            PacStudent.transform.position = Vector3.Lerp(new Vector3(-4.0f, 3.0f, 0.0f), new Vector3(5.0f, 3.0f, 0.0f), timeOut / duration);
            timeOut += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && timeOut < duration)
        {
            PacStudent.transform.position = Vector3.Lerp(new Vector3(5.0f, 3.0f, 0.0f), new Vector3(5.0f, -4.0f, 0.0f), timeOut / duration);
            timeOut += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && timeOut < duration)
        {
            PacStudent.transform.position = Vector3.Lerp(new Vector3(5.0f, -4.0f, 0.0f), new Vector3(-4.0f, -4.0f, 0.0f), timeOut / duration);
            timeOut += Time.deltaTime;
        }
    }
}
