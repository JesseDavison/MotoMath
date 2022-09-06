using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public AudioSource MusicSource, EffectSource;


    public AudioSource GatlingSound;
    //public AudioClip GatlingSound_clip;       // attach gatling sound here... but it's removed for now so as to not take up hard drive space for the user


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip) {
        EffectSource.PlayOneShot(clip);
    }

    public void Play_GatlingSound() {

        //GatlingSound.PlayOneShot(GatlingSound_clip);

        // this is disabled because adding comprehensive sound effects that aren't awful will take more time than I'm willing
        

    }


}
