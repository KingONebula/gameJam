using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Timer timer;
    [SerializeField]float cycleTime;
    int previousTick;
    int posY;
    [SerializeField] float y;
    [SerializeField]GameObject projectileWall;
    void Awake()
    {

        posY = 1;
        y = posY * 14f;
        timer = new Timer(100000000);
        timer.setTimer(cycleTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        timer.timeUpdate();
        if (previousTick != timer.tick)
        {
            posY *= -1;
            transform.position = new Vector3(transform.position.x, transform.position.y + y * posY);
            Instantiate(projectileWall, transform);
            previousTick = timer.tick;
        }
        if (timer.timeEnd)
        {
            Destroy(gameObject);
        }
    }
}
