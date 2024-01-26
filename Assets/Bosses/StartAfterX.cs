using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAfterX : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float time;
    [SerializeField] RicoLogic cl;
    Timer timer;
    void Awake()
    {
        
        timer = new Timer();
        timer.setTimer(time);
    }

    // Update is called once per frame
    void Update()
    {
     
        timer.timeUpdate();
        if (timer.timeEnd)
        {
            cl.enabled = true;
            Destroy(this);
        }
    }
}
