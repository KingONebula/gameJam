using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightTrigger : MonoBehaviour
{
    bool fight;
    public static FightTrigger instance;
    [SerializeField] GameObject boss, spawn;
    [SerializeField] Gate[] doors;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!fight && collision.tag == "Player")
        {
            fight = true;
            foreach (Gate gate in doors)
            {
                gate.door();
            }
            if (boss!=null)
                Instantiate(boss, spawn.transform);
            else
            RoomLogic.instance.startFight();
        }
    }
    public void endBoss()
    {
        foreach (Gate gate in doors)
        {
            gate.door();
        }
    }
}
