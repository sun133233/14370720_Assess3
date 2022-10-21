using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public AudioClip BM_intro;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
}
