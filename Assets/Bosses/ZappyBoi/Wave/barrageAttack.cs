using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrageAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject bullet;
    private void OnEnable()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
