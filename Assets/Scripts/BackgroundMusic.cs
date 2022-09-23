using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    public AudioClip BM_intro;
    public AudioClip BM_normal;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playBM());
    }
    IEnumerator playBM()
    {
        GetComponent<AudioSource>().clip = BM_intro;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(BM_intro.length);
        GetComponent<AudioSource>().clip = BM_normal;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
