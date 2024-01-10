using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boon : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerData playerData;
    private void Awake()
    {
        playerData = PlayerData.instance;
    }
    public void crit(float crit)
    {
        playerData.critF(crit);
    }
    public void speed(float speed)
    {
        playerData.speedF(speed);
    }
    public void accuracy(float accuracy)
    {
        playerData.accuracyF(accuracy);
    }
    public void damagereduction(float damagereduction)
    {
        playerData.damagereductionF(damagereduction);
    }
    public void dogde(float dogde)
    {
        playerData.dogdeF(dogde);
    }
    public void maxHealth(int maxHP)
    {
        playerData.maxHealthF(maxHP);
    }
    public void begunEnd()
    {
        End_Screen.instance.selectedBoon(gameObject);
    }
}
