using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource sfxAS;
    public static GameManager instance;
    public AudioSource sfxEN;
    public static GameManager instance2;
    public AudioSource sfxUP;
    public static GameManager instance3;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        if(instance2 == null)
        {
            instance2 = this;
        }
        if(instance3 == null)
        {
            instance3 = this;
        }
    }
    
    public void PlaySFX(AudioClip sfx)
    {
        sfxAS.PlayOneShot(sfx);
    }
}
