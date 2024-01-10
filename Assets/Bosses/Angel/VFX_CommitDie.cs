using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_CommitDie : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    void Start()
    {
        timer = new Timer();
        timer.setTimer(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timer.timeUpdate();
        if (timer.timeEnd)
        {
            Destroy(gameObject);
        }
    }
}
