using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_UI : MonoBehaviour
{
    // Start is called before the first frame update
    int maxHealth, currentHealth;
    [SerializeField]RectTransform BG, Bar;
    public void setMax(int max)
    {
        maxHealth = max;
    }
    public void setHealth(int hp)
    {
        currentHealth = hp;
        Bar.localScale = new Vector3( (float)currentHealth / (float)maxHealth, 1, 1);
    }
}
