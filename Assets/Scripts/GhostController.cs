using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GhostState
{
    DIE,
    SCARED,
    RECOVERING,
    RIGHT
}
public class GhostController : MonoBehaviour
{
    private Animator thisAnim;
    private Text timeText;
    void Start()
    {
        thisAnim = GetComponent<Animator>();
        timeText = GetComponentInChildren<Text>();
    }
    public void SetState(GhostState state)
    {
        switch (state)
        {
            case GhostState.RIGHT:
                thisAnim.SetTrigger("Scared");
                break;
            case GhostState.DIE:
                thisAnim.SetTrigger("Die");
                break;
            case GhostState.SCARED:
                thisAnim.SetTrigger("Scared");
                break;
            case GhostState.RECOVERING:
                thisAnim.SetTrigger("Recovering");
                StartCoroutine(GameManager.Instance.DelayTime(3, () => SetState(GhostState.RIGHT)));
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            collision.GetComponent<PacStudentController>().ReduceHP(1);
        }
    }
}
