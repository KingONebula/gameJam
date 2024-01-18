using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] spawn;
    [SerializeField] GameObject bullet;
    private void OnEnable()
    {
        foreach(GameObject p in spawn)
        {
            Instantiate(bullet, p.transform.position, p.transform.rotation);
        }
        gameObject.SetActive(false);
    }
}
