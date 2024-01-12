using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{


    [SerializeField] GameObject[] lightOBJ;
    [SerializeField]bool open, debug, lightsoff;
    [SerializeField]float doorOpen, time, maxtime, doorClose, pos;
    private void Start()
    {
        time = 0.1f;
        maxtime = 0.1f;
        open  = true;
    }

    private void Update()
    {
        if (debug)
        {
            debug = false;
            door();
        }
        if (open)
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0, maxtime);
            pos = Mathf.Lerp(doorClose, doorOpen, time / maxtime);
        }
        else
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0, maxtime);
            pos = Mathf.Lerp(doorOpen, doorClose, time / maxtime);
            if (time == maxtime && !lightsoff)
            {
                foreach(GameObject obj in lightOBJ)
                {
                    obj.SetActive(false);
                }
                lightsoff = true;
            }
        }
        transform.position = new Vector3(transform.position.x, pos);
    }

    public void door()
    {
        time = 0;
        open = !open;
    }
}
