using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInXSec : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    void Start()
    {
        timer.setTimer(1);
    }

    // Update is called once per frame
    void Update()
    {
        timer.timeUpdate();
        if (timer.timeEnd)
            Destroy(gameObject);
    }
}
