using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public static RoomLogic instance;
    [SerializeField]Transform bossSpawn;
    [SerializeField]Gate[] doors;
    public bool startEnd, End;
    void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (startEnd && !End)
        {
            End = true;
            endBoss();
        }
    }
    // Update is called once per frame
    public void endBoss()
    {
        StopCoroutine(loadBoss());
        foreach (Gate gate in doors)
        {
            gate.door();
        }
    }
    public void startFight()
    {
        StartCoroutine(loadBoss());
    }
    IEnumerator loadBoss()
    {
        foreach(Gate gate in doors)
        {
            gate.door();
        }
        yield return new WaitForSeconds(0.5f);
        //Instantiate(FloorData.boss, bossSpawn);
    }
}
