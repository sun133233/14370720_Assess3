using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public  GameObject cherry;
    private List<Cherry> cherrypres=new List<Cherry>();
    private float spawn = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Instant();
        InvokeRepeating("Instant", spawn, spawn);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cherrypres.Count; i++)
        {
            Cherry cherrypre = cherrypres[i];
            if (cherrypre.obj)
            {
                cherrypre.Move();
            }
            else
            {
                cherrypres.Remove(cherrypre);
            }
        }
    }
    private void Instant()
    {
        int x = Random.Range(0,30);
        int y = Random.Range(0, 17);
        Cherry cherry = new Cherry();
        cherry.quadrant = Random.Range(0, 4);
       // int quadrant = Random.Range(0, 4);
        if (cherry.quadrant == 0)
        {
            cherry. cherryPos = new Vector2(Random.Range(-x,x), 17);
            cherry.endPos = cherry.cherryPos*-1;
            cherry.endPos.y = -17;
        }
        if (cherry.quadrant == 1)
        {
            cherry.cherryPos = new Vector2(Random.Range(-x, x), -17);
            cherry.endPos = cherry.cherryPos*-1;
            cherry.endPos.y = 17;
        }
        if (cherry.quadrant == 2)
        {
            cherry.cherryPos = new Vector2(-30, Random.Range(-y, y));
            cherry.endPos = cherry.cherryPos*-1;
            cherry.endPos.x= 30;
        }
        if (cherry.quadrant == 3)
        {
            cherry.cherryPos = new Vector2(30, Random.Range(-y, y));
            cherry.endPos = cherry.cherryPos*-1;
            cherry.endPos.x = -30;
        }
       
        cherry.obj = Instantiate(this.cherry, cherry.cherryPos, Quaternion.identity);
        cherrypres.Add(cherry);
    }
}

public class Cherry
{
    public GameObject obj;
    public float timeOut;
    float duration = 9.0f;
    public Vector2 endPos;
    public Vector2 cherryPos;
    public int quadrant;

    public void Move()
    {
        timeOut += Time.deltaTime;
        if (timeOut < duration)
        {
            obj.transform.position = Vector2.Lerp(cherryPos, endPos, timeOut / duration);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }
}
