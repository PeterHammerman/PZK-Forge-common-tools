using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesEntityController : OnePermaInstance<ScenesEntityController>
{

    public float defaultTime = 600f;
    private bool timerActive;

    public class Entity : ScriptableObject
    {
        public string SceneName;
        public int ID;
        public float timeLeft;
        public float cooldownTime;
    }


    public List<Entity> entities;


    private void Update()
    {
        if (entities.Count > 0)                 //do only when there are entities in list
        {
            if (!timerActive)                   //do it once when timerActive bool is false
            {
                InvokeRepeating("Timer", 1f, 1f);   //count every second
                timerActive = true;
            }
        }
        else
        {
            CancelInvoke("Timer");
            timerActive = false;
        }

    }

    void Timer()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].timeLeft -= 1;                          //substract 1 second from all entities
            if (entities[i].timeLeft < 0)
            {
                entities.RemoveAt(i);                           //when entity time out, remove from list
                Debug.Log("removed from entity cooldown list");
            }

            if (i > 0)                                             //iteration fuse
                i -= 1;                                             //back 1 iteration when removed from list
        }
    }


    public void AddEntity(string _sceneName, int _iD, float _cooldown)
    {
        Entity tempEntity = new Entity();
        tempEntity.SceneName = _sceneName;
        tempEntity.ID = _iD;

        if (_cooldown == 0)                                          //cooldown default/specified
            tempEntity.timeLeft = defaultTime;
        else
            tempEntity.timeLeft = _cooldown;

        entities.Add(tempEntity);
    }

    public void CheckEntityOnLoad(string _sceneName, int _iD, out bool outputE)
    {
        bool outp = false;

        if (entities.Count > 0)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].ID == _iD && entities[i].SceneName == _sceneName)
                {
                    outp = true;
                }


            }
        }
        else
        {
            outp = false;
        }

        outputE = outp;
    }

    

}
