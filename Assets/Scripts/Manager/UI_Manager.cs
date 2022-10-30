using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public AudioClip BM_intro;
    public Text scoreText;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("SCORE").ToString();
        timeText.text = PlayerPrefs.GetString("TIME");
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
