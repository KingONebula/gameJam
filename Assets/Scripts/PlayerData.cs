using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerData instance { get; private set; }
    public static GunScrOBJ gunInHand;
    public static float crit, speed, accuracy, damagereduction, dogde;
    public static int maxHealth;
    public static int wave;
    public static List<GameObject> bosses;
    public List<GameObject> bosses2;
    public GameObject[] bossLookUp;
    public static List<GameObject> boons;
    public List<GameObject> bonus;
    public GameObject[] boonslookup;
    void Start()
    {
        crit = 0.1f;
        speed = 7;
        accuracy = 0;
        damagereduction = 0;
        dogde = 0;
        maxHealth = 10;
        boons = new List<GameObject>();
        bosses = new List<GameObject>();
        wave = 0;
        foreach(GameObject go in bossLookUp)
        {
            bosses.Add(go);
        }
        foreach(GameObject go in boonslookup)
        {
            boons.Add(go);
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    public int bossesCount()
    {
        return bosses.Count;
    }
    public GameObject popBosses(int bossIndex)
    {
        GameObject boss = bosses[bossIndex];
        bosses.RemoveAt(bossIndex);
        bosses2 = bosses;
        return boss;
    }
    public int boonsCount()
    {
        return boons.Count;
    }
    public void AddBoon(GameObject boon)
    {
        boons.Add(boon);
        bonus = boons;
    }
    public GameObject popBoon(int boonsIndex)
    {
        GameObject boon = boons[boonsIndex];
        boons.RemoveAt(boonsIndex);
        return boon;
    }

    public void waveF()
    {
        wave++;
    }
    public float waveR()
    {
        return wave;
    }
    public void critF(float cri)
    {
        crit += cri;
    }
    public float critR()
    {
        return crit;
    }
    public void speedF(float spee)
    {
        speed += spee;
    }
    public float speedR()
    {
        return speed;
    }
    public void accuracyF(float accurac)
    {
        accuracy += accurac;
    }
    public float accuracyR()
    {
        return accuracy;
    }
    public void damagereductionF(float damagereductio)
    {
        damagereduction += damagereductio;
    }
    public float damagereductionR()
    {
        return damagereduction;
    }
    public void dogdeF(float dogd)
    {
        dogde += dogd;
    }
    public float dogdeR()
    {
        return dogde;
    }
    public void maxHealthF(int maxHP)
    {
        maxHealth += maxHP;
    }
    public int maxHealthR()
    {
        return maxHealth;
    }
    public void setGun(GunScrOBJ gun)
    {
        gunInHand = gun;
    }
    public GunScrOBJ getGun()
    {
        return gunInHand;
    }
}
