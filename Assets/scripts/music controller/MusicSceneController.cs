using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSceneController : OnePermaInstance<MusicSceneController>  //Main scene controller for music
{

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource nextMusic;
    [HideInInspector]
    public bool trigger;
    private float fadeOut;
    private bool music1FadeOut;
    private bool music2FadeOut;
    private float volumeBeforeFade;
    public float fadingSpeed = 0.003f;
    bool triggerSwitch = true;


    // Start is called before the first frame update
    void Start()
    {
        triggerSwitch = !triggerSwitch;
        UpdateMusic();

        if(music1 != null)
            music1.Play();

        if(music2 != null)
            music2.Play();
    }

    void PlayMusic()                           //play actual music, fade out second
    {
        if (triggerSwitch)
        {
            if (music1 != null)
            {
                if (music2 != null)
                    music2FadeOut = true;

              
            }

        }
        else
            if (music2 != null)
            {
                if (music1 != null)
                    music1FadeOut = true;

               

            }
      


    }

    public void UpdateMusic()                       //function to execute
    {


            if (music1 == null)
            {
                music1 = nextMusic;
                music1.volume = nextMusic.volume;
                nextMusic = null;
            }
            else
                if (music2 == null)
            {
                music2 = nextMusic;
                music2.volume = nextMusic.volume;
                nextMusic = null;
            }

        

            trigger = false;
            PlayMusic();
    
    }

    // Update is called once per frame


    void Update()                                            //runtime behavior, delete inactive old music
    {
        if (music1FadeOut && music1 != null)
        {
            if (music1.clip != music2.clip)                   // prevent if is the same audioclip, continue play previous
            {
                music1.volume -= fadingSpeed;

                if (music1.volume <= 0)
                {
                    music1.Stop();
                    //  music1 = null;
                    music1.volume = 1;
                    music1FadeOut = false;
                    Destroy(music1.gameObject);
                    music1 = null;
                    triggerSwitch = !triggerSwitch;
                    music2.Play();
                }
            }
            else
            {
                Destroy(music2.gameObject);
                music1FadeOut = false;
                music2 = null;
            }
        }

        if (music2FadeOut && music2 != null)
        {
            if (music1.clip != music2.clip)            // prevent if is the same audioclip, continue play previous
            {
                music2.volume -= fadingSpeed;

                if (music2.volume <= 0)
                {
                    music2.Stop();
                    //  music2 = null;
                    music2.volume = 1;
                    music2FadeOut = false;
                    Destroy(music2.gameObject);
                    music2 = null;
                    triggerSwitch = !triggerSwitch;
                    music1.Play();
                }
            }
            else
            {
                Destroy(music1.gameObject);
                music2FadeOut = false;
                music1 = null;
            }
        }

        if (trigger)
        {
            UpdateMusic();                           //main void when listener pass current music
        }

    }
}
