using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    bool fight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!fight && collision.tag == "Player")
        {
            fight = true;
            RoomLogic.instance.startFight();
        }
    }
}
