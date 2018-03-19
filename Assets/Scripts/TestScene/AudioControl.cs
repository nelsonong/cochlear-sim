using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour
{
    public AudioClip StartClip;
    public AudioClip LoopClip;

    void Start()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        GetComponent<AudioSource>().clip = StartClip;
        GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(StartClip.length);
        yield return new WaitForSeconds(5);
        GetComponent<AudioSource>().clip = LoopClip;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().loop = true;
    }
}