using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInXSec : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float destroytime;
    void Start()
    {
        Destroy(gameObject, destroytime);
    }

}
