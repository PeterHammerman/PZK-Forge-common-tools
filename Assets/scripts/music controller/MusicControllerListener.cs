using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControllerListener : MonoBehaviour   //ATTACH SCRIPT TO MUSIC AUDIOSOURCE IN SCENE
{

    MusicSceneController musicController;
    public bool forceLoop;
   
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if(forceLoop)
        {
            GetComponent<AudioSource>().loop = true;
        }

        GameObject.Find("MusicController").TryGetComponent<MusicSceneController>(out musicController);
        GetComponent<AudioSource>().Stop();

        if (musicController != null)
        {
            musicController.nextMusic = GetComponent<AudioSource>();
            musicController.trigger = true;

        }
    }



}
