using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    [SerializeField] float cycleTime;
    int previousTick;
    [SerializeField] GameObject projectileWall;
    Transform[] spawnSpots;
    void Awake()
    {
        timer = new Timer(100000000);
        timer.setTimer(cycleTime);
    }

    // Update is called once per frame
    void Update()
    {

        timer.timeUpdate();
        if (previousTick != timer.tick)
        {
            Instantiate(projectileWall, spawnSpots[timer.tick]);
            previousTick = timer.tick;
        }
        if (timer.timeEnd)
        {
            Destroy(gameObject);
        }
    }
}