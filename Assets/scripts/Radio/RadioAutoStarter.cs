using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAutoStarter : MonoBehaviour              //Radio autostarter on load
{



    Radio menuMusicScript;

    // Start is called before the first frame update
    void Start()
    {

        menuMusicScript = Radio.Instance;

        if(menuMusicScript != null)
        {

            if (!menuMusicScript.playing)
            {
                menuMusicScript.fadingTo = 1;
                menuMusicScript.fading = true;

                menuMusicScript.PlayButton();
            }

            menuMusicScript.UIChangeTrack();
        }



    }


}
