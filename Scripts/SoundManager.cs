using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource src;
    public static SoundManager instance;

    private void Awake()
    {
        
        src = GetComponent<AudioSource>();
        //Does not destroy gameObject when loading new level
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Avoid glitch of having two music objects playing at same time
        else if(instance !=null && instance!=this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip audio)
    {
        //Play one shot allows to play audio only once for sound effects
        src.PlayOneShot(audio);
    }
}
