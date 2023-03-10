using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerListener : MonoBehaviour   //ATTACH SCRIPT TO MUSIC AUDIOSOURCE IN SCENE
{

    MusicSceneController musicController;
    public bool forceLoop = true;        //forcing loop music
   
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);  //prevent to destroy when scene changing
        if(forceLoop)
        {
            GetComponent<AudioSource>().loop = true;
        }

        musicController = MusicSceneController.Instance;  //In main scene must have "MusicController" instance with MusicSceneController script to work.
        GetComponent<AudioSource>().Stop();

        if (musicController != null)                                      //Sending actual music to play by controller
        {
            musicController.nextMusic = GetComponent<AudioSource>();
            musicController.trigger = true;

        }
    }



}
