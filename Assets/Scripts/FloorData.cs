using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorData : MonoBehaviour
{
    // Start is called before the first frame update
    public static FloorData instance;
    private void Start()
    {
        Debug.Log("TriggerBoss");
        instance = this;   
        int size = PlayerData.instance.bossesCount();
        int bossIndex = Random.Range(0, size);
        boss = PlayerData.instance.popBosses(bossIndex);
    }
    public static GameObject boss;
}
