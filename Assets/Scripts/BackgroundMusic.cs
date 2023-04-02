using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] soundtrack;
    public AudioSource audito;

    // Use this for initialization
    void Start()
    {


        if (!audito.playOnAwake)
        {
            audito.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audito.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audito.isPlaying)
        {
            audito.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audito.Play();
        }
    }
}
