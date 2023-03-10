using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityOnLoad : MonoBehaviour
{

    public int ID;
    public float cooldownOverride;
    ScenesEntityController entityController;
    bool isActive;

    public Scene scene;
    
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        entityController = ScenesEntityController.Instance;

        entityController.CheckEntityOnLoad(scene.name, ID, out isActive);
        if(!isActive)
        {
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);             //Disable if is in cooldown list

    }


}
