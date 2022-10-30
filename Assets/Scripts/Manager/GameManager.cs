using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    private void Awake()
    {
        instance = this;
    }


    public PacStudentController studentController;
    private GhostController[] ghosts;
    private int Score = 0;
    private int MaxScore;
    //gameStart
    public Text startGameText;
    private bool isGameStart;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        ghosts = GameObject.FindObjectsOfType<GhostController>();

        studentController.enabled = false;
        startGameText.gameObject.SetActive(true);

        for (int i = 3; i >= 0; i--)
        {
            if (i > 0)
                startGameText.text = i.ToString();
            else
                startGameText.text = "GO";
            yield return new WaitForSeconds(1);
        }
        startGameText.gameObject.SetActive(false);
        studentController.enabled = true;
        isGameStart = true;
    }


    private void Update()
    {
        if (isGameStart)
        {
            GameTime();
        }
    }

    //gameOver
    [SerializeField]
    private GameObject gameOver;
    public void GameOver()
    {
        int maxScore= PlayerPrefs.GetInt("SCORE", Score);
        if (maxScore<Score)
        {
            PlayerPrefs.SetInt("SCORE", Score);
            PlayerPrefs.SetString("TIME", timeText.text);
        }
        gameOver.SetActive(true);
        studentController.enabled = false;
        isGameStart = false;
        StartCoroutine(DelayTime(3, () =>
        {
            SceneManager.LoadScene(0);
        }));
    }
    

    //time
    public Text timeText;
    float gameTime;
    private void GameTime()
    {
        gameTime += Time.deltaTime;
        int ms = (int)(gameTime * 100) % 100;
        int s = (int)gameTime % 60;
        int m = (int)gameTime / 60;
        timeText.text = $"{m.ToString("00")}:{s.ToString("00")}:{ms.ToString("00")}";
    }
    public IEnumerator DelayTime(float time,UnityAction action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }


    //ghost
    public void SetGhostState(GhostState state)
    {
        foreach (GhostController item in ghosts)
        {
            item.SetState(state);
        }
    }

    public Text ScoreText;
    public void AddScore(int s)
    {
        Score += s;
        ScoreText.text = Score.ToString();
    }
  
    [SerializeField]
    private GameObject[] hpUI;
    public void UpdateHPUI(int hp)
    {
        if (hp < 0) return;
        
        hpUI[hp].SetActive(false);
    }
}
