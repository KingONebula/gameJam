using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    bool fight;
    [SerializeField] GameObject boss, spawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!fight && collision.tag == "Player")
        {
            fight = true;
            if(boss!=null)
                Instantiate(boss, spawn.transform);
            else
            RoomLogic.instance.startFight();
        }
    }
}
