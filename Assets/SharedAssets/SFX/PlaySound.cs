using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    [SerializeField] AudioSource soundSources;

    public void Play()
    {
        soundSources.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    }
}
