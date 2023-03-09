using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Radio : OnePermaInstance<Radio>         //Radio controller script
{
    [Header("PLAYLIST")]
    public  AudioClip[] RegularPlaylist1;             //clips with music library - to expand with resource loading
    public string[] RegularPlaylistName;              // titles to be shown
    [HideInInspector]
    public string Title;

    [Header("Audiosource to play")]
    public  AudioSource musicSource;



    [Header("UI")]
    public GameObject currentlyPlayUI;
    public TextMeshProUGUI nowPlayingText;
    public RectTransform CDicon;

    [HideInInspector]
    bool rotateCD;
    [HideInInspector]
    public bool fading;
    [HideInInspector]
    public float fadingTo;
    [HideInInspector]
    public bool playing;
    [HideInInspector]
    public int musicTrackStart;
    [HideInInspector]
    public int currentTrack;
    [HideInInspector]
    public bool skipPressed;
    [HideInInspector]
    public bool nextSong;
    float degCD = 0;




    private void OnLevelWasLoaded(int level)            //Behavior when scene is loaded
    {



        currentlyPlayUI = GameObject.Find("CurrentlyPlaying");
        GameObject cd = GameObject.Find("CD");
        if(cd!=null)
            cd.TryGetComponent<RectTransform>(out CDicon);

        GameObject label = GameObject.Find("Label");
        if(label!= null)
        label.TryGetComponent<TextMeshProUGUI>(out nowPlayingText);

        nowPlayingText.text = Title;
    }
    

    private void Start()
    {

        currentlyPlayUI = GameObject.Find("CurrentlyPlaying");                                         //Setting up UI (to change freely)
        GameObject.Find("CD").TryGetComponent<RectTransform>(out CDicon);
        GameObject.Find("Label").TryGetComponent<TextMeshProUGUI>(out nowPlayingText);

        if(nowPlayingText != null)
        nowPlayingText.text = Title;

        PlayButton();

        fadingTo = 1;
        fading = true;
    }



    public void PlayButton()
    {
        if (!playing)
        {
            skipPressed = true;
            StopAllCoroutines();
            LosujUtwor();
            StartCoroutine(PlaylistOne());
            UIChangeTrack();

        }
        else
        {
            StopAllCoroutines();
            CancelInvoke();
            NextSong();
            StartCoroutine(PlaylistOne());
            UIChangeTrack();
        }
    }

    public void StopButton()
    {
        StopAllCoroutines();
        playing = false;
        musicSource.Stop();
        UIChangeTrack();

    }

    public void NextSong()     //nastepny utwor
    {
       
        StopAllCoroutines();
        nextSong = true;
        currentTrack++;
        UIChangeTrack();
        


    }

    public void LosujUtwor()
    {
        musicTrackStart = Random.Range(0, RegularPlaylist1.Length);
        UIChangeTrack();


    }

    public void PreviousSong()    //popzedni utwor 
    {
        StopAllCoroutines();
        nextSong = true;
        currentTrack--;
        UIChangeTrack();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.Play();
    }



    IEnumerator PlaylistOne()                  
    {
        if(!playing)
        {
            playing = true;
        }


        while(playing)
        {
            for(int i=0; i<RegularPlaylist1.Length; i++)
            {


                if (skipPressed)
                {
                    i = musicTrackStart;
                    skipPressed = false;
                }
                if(nextSong)
                {
                    nextSong = false;
                    if (currentTrack == RegularPlaylist1.Length)
                        currentTrack = 0;



                    i = currentTrack;

                    if (i == RegularPlaylist1.Length)
                        i = 0;

                }
                

                musicSource.clip = RegularPlaylist1[i];
                Title = RegularPlaylistName[i];
                musicSource.Play();
                currentTrack = i;
                Debug.Log("Now playing: " + i.ToString());
                Invoke("PlayButton", musicSource.clip.length);

                while (musicSource.isPlaying)
                {
                    yield return null;
                }


            }
        }
       

    }



    private void Update()
    {
        if(fading && musicSource != null)
        {
            musicSource.volume = Mathf.Lerp(musicSource.volume, fadingTo, Time.time*2);

            if (musicSource.volume == fadingTo)
            {
                fading = false;

                if (musicSource.volume == 0)
                    StopButton();
            }

            if (fadingTo > 0 && musicSource.volume == 0)
                PlayButton();

        }

            if (rotateCD && CDicon != null)
        {
            degCD += 5;
            CDicon.transform.localRotation = Quaternion.Euler(0, 0, degCD);
        }
    }



    public void UIChangeTrack()
    {
        StopAllCoroutines();

        if(nowPlayingText != null)
        if (nowPlayingText != null && Title != null && CDicon != null)
        {
            nowPlayingText.text = "";
            StartCoroutine(ShowTrackInfo());
        }
    }


    IEnumerator ShowTrackInfo()
    {
        if (nowPlayingText != null)
        {
            currentlyPlayUI.SetActive(true);
            rotateCD = true;
            Title = RegularPlaylistName[currentTrack];
            nowPlayingText.text = Title;
            yield return new WaitForSeconds(7);
            rotateCD = false;
            nowPlayingText.text = "";
            currentlyPlayUI.SetActive(false);
        }

        yield return null;
    }

}
