using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement : MonoBehaviour
{
    private AudioSource PacSource;
    private Animator PacController;
    private string[] triggers = {"P_RIGHT", "P_DOWN", "P_LEFT","P_UP" };
    public Transform PacStudent;
    float duration = 2.0f;
    float timeOut = 0.0f;
    Vector3[] positions = { new Vector3(-12.0f, 13.0f, 0.0f), new Vector3(-7.0f, 13.0f, 0.0f), new Vector3(-7.0f, 9.0f, 0.0f), new Vector3(-12.0f, 9.0f, 0.0f) };
    public int currentPos;
    // Start is called before the first frame update
    void Start()
    {
        PacController = GetComponent<Animator>();
        PacStudent.transform.position = new Vector3(-12.0f, 13.0f, 0.0f);
        PacSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOut<duration)
        {
            int nextPos = currentPos + 1;
            
            if (nextPos == positions.Length)
            {
                nextPos = 0;
            }
            PacStudent.transform.position = Vector3.Lerp(positions[currentPos], positions[nextPos], timeOut / duration);
            timeOut += Time.deltaTime;
            PacController.SetTrigger(triggers[currentPos]);
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
        if (!PacSource.isPlaying)
        {
            PacSource.Play();
        }
    }
}
