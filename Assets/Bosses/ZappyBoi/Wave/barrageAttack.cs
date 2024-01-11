using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrageAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject bullet;
    [SerializeField] bool isLast;
    private void Awake()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        if (isLast)
            Destroy(gameObject.transform.parent.gameObject);
    }
}
