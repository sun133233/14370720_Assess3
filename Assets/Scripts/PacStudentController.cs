using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    private float dt = 1.0f;
    private float duration = 2.0f;
    private float speed = 3.0f;
    private Vector2 endPos;
    private KeyCode lastInput = KeyCode.None;
    public Transform Pacman;
    private Vector2 currentPos;
    private Vector2 currentInput;
    public AudioSource[] PacmanAudio = new AudioSource[3];
    private Animator PacController;
    private ParticleSystem dust;
    public ParticleSystem wall;
    private bool isMove;
    private int Score = 0;
    public GameObject ScoreText;

    void Start()
    {
        Pacman = GameObject.FindWithTag("Player").transform;
        currentPos = new Vector2(-12f,13f);
        endPos = new Vector2(-12f, 13f);
        PacController = GetComponent<Animator>();
        dust = GameObject.FindWithTag("Player").GetComponent<ParticleSystem>();
        StartCoroutine(Controller());
        PacmanAudio[2].Stop();
    }


    IEnumerator Controller()
    {
        while (true)
        {
            LastKey();
            RaycastHit2D hit = Physics2D.Raycast(Pacman.position, currentInput, 1);
            if (hit.collider != null)
           {
                if (hit.collider != null && hit.collider.tag == "Wall")
                {
                    dust.Stop();
                    wall.transform.position = currentPos;
                    isMove = false;
                    if (dt >= 1)
                    {
                        wall.Play();
                        PacmanAudio[2].Play();
                        dt = 0;
                    }
                }
                else if (hit.collider.GetComponent<Teleport>())
                {
                    Teleport(hit.collider.GetComponent<Teleport>().TriggerEnter());
                }
                else
                {
                    StartMove();
                }
            }
            else
            {
                StartMove();
            }


            if (isMove)
            {
                if (hit.collider==null)
                {
                    PacmanAudio[1].Play();
                }
                else if (hit.collider.tag == "fish")
                {
                    Destroy(hit.collider.gameObject);
                    PacmanAudio[0].Play();
                    Score += 10;
                    PlayerPrefs.SetInt("SCORE", Score);

                }
                else if (hit.collider.gameObject.name == "cherry(Clone)")
                {
                    Destroy(GameObject.Find("cherry(Clone)"));
                    Score += 100;
                    PlayerPrefs.SetInt("SCORE", Score);
                }
                yield return new WaitForSeconds(1/speed);
            }
            else
                yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dt < 1)
        {
            dt += Time.deltaTime;
        }
        Move();
        ScoreText.GetComponent<Text>().text = Score.ToString();
    }


    private void Move()
    {
        if (isMove)
        {
            duration += Time.deltaTime * speed;
            transform.position = Vector2.Lerp(currentPos, endPos, duration);
        }
    }
    private void LastKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            lastInput = KeyCode.W;
            currentInput = Vector2.up;
            PacController.Play("P_UP");
        }
        if (Input.GetKey(KeyCode.A))
        {
            lastInput = KeyCode.A;
            currentInput = Vector2.left;
            PacController.Play("P_LEFT");
        }
        if (Input.GetKey(KeyCode.S))
        {
            lastInput = KeyCode.S;
            currentInput = Vector2.down;
            PacController.Play("P_DOWN");
        }
        if (Input.GetKey(KeyCode.D))
        {
            lastInput = KeyCode.D;
            currentInput = Vector2.right;
            PacController.Play("P_RIGHT");
        }
        currentPos = Pacman.transform.position;
        endPos = (Vector2)transform.position + currentInput;
        duration = 0;

    }
    private void StartMove()
    {
        dust.Play();
        wall.Stop();
        isMove = true;
    }
    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
        currentInput = Vector2.zero;
        dust.Stop();
        isMove = false;
    }
}
