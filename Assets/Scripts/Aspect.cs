using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aspect : MonoBehaviour
{
    
   CanvasScaler cs;
    // Start is called before the first frame update
    private void Awake()
    {
        cs = GetComponent<CanvasScaler>();
    }
    void Start()
    {
        ChanageScreenSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChanageScreenSize()
    {
        if (Screen.width / Screen.height == 16 / 9)
        {
            cs.referenceResolution = new Vector2(1920, 1080);
        }
        else
            cs.referenceResolution = new Vector2(1024, 768);
    }
}
