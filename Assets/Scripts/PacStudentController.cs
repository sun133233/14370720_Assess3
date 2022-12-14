using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PacStudentController : MonoBehaviour
{
    private float dtTime = 1;
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
    public ParticleSystem dead;
    public GameObject[] ghost = new GameObject[4];
    public AudioClip BM_scared;
    private Vector3 startPos;
    public Text GhostScaredText;

    
    private int HP = 3;
    private bool isMove;
    private int fishCount;

    void Start()
    {
        fishCount = GameObject.FindGameObjectsWithTag("fish").Length;
        startPos = transform.position;
        Pacman = GameObject.FindWithTag("Player").transform;
        currentPos = new Vector2(-12f, 13f);
        endPos = new Vector2(-12f, 13f);
        PacController = GetComponent<Animator>();
        dust = GameObject.FindWithTag("Player").GetComponent<ParticleSystem>();
        PacmanAudio[2].Stop();
        PacmanAudio[1].Stop();
        StartCoroutine(Controller());
    }
    IEnumerator Controller()
    {
        while (true)
        {
            LastKey();
            RaycastHit2D hit = Physics2D.Raycast(Pacman.position,currentInput,1);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Wall")
                {
                    dust.Stop();
                    isMove = false;
                    wall.transform.position = currentPos;
                    if (dtTime >= 1)
                    {
                        wall.Play();
                        PacmanAudio[2].Play();
                        dtTime = 0;
                    }
                }
                else if (hit.collider.GetComponent<Teleport>())
                {
                    Teleport(hit.collider.GetComponent<Teleport>().TriggerEnter());
                }
                else if (hit.collider.GetComponent<GhostController>())
                {
                    ReduceHP(1);
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
                if (hit.collider == null)
                {
                    PacmanAudio[1].Play();
                }
                else if (hit.collider.tag == "fish")
                {
                    Destroy(hit.collider.gameObject);
                    PacmanAudio[0].Play();
                    GameManager.Instance.AddScore(10);
                    fishCount--;
                    if (fishCount<=0)
                    {
                        GameManager.Instance.GameOver();
                    }
                }
                else if (hit.collider.gameObject.name == "cherry(Clone)")
                {
                    Destroy(GameObject.Find("cherry(Clone)"));
                    GameManager.Instance.AddScore(100);
                }
                else if (hit.collider.tag == "power")
                {
                    Destroy(hit.collider.gameObject);
                    GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().clip = BM_scared;
                    GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
                    GhostStateTime(10);
                    StartCoroutine(PowerEvent());
                }
                yield return new WaitForSeconds(1 / speed);
            }
            else
                yield return null;

        }
    }
    void GhostStateTime(float time)
    {
        GameManager.Instance.SetGhostState(GhostState.SCARED);
        StartCoroutine(GameManager.Instance.DelayTime(time-3, () => GameManager.Instance.SetGhostState(GhostState.RECOVERING)));
    }
    IEnumerator PowerEvent()
    {
        GameObject gpObj = GhostScaredText.transform.parent.gameObject;
        gpObj.SetActive(true);
        GhostScaredText.text = 10.ToString();
        for (int i = 10; i >= 0; i--)
        {
            GhostScaredText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        gpObj.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (dtTime < 1)
        {
            dtTime += Time.deltaTime;
        }
        Move();
    }

    private void StartMove()
    {
        dust.Play();
        wall.Stop();
        dead.Stop();
        isMove = true;
    }
    private void Move()
    {
        RaycastHit2D hit= Physics2D.Raycast(Pacman.position, currentInput, 1);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Wall")
            {
                return;
            }

        }
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
    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
        currentInput = Vector2.zero;
        dust.Stop();
        isMove = false;
    }

    public void ReduceHP(int hp)
    {
        dead.transform.position = transform.position;
        dead.Play();
        HP -= hp;
       GameManager.Instance.UpdateHPUI(HP);
        transform.position = startPos;
        currentPos = startPos;
        endPos = startPos;
        GhostStateTime(5);
        if (HP<=0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
