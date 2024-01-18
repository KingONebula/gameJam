using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazernotaPewPew : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(shutoff());
    }
    IEnumerator shutoff()
    {
        yield return new WaitForSeconds(2);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
