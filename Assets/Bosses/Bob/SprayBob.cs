using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBob : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject bullet, spawn;

    int startAngle;
    int degSecond;
    float angle;
    private void OnEnable()
    {
        int headTos = headToss();
        startAngle = 180 * headTos;
        if(startAngle < 0) { startAngle = 0; }
        angle = startAngle;
        degSecond = 30 * headTos;
        transform.rotation = Quaternion.Euler(0, 0, startAngle);
        StartCoroutine(pewpewpew());
    }
    IEnumerator pewpewpew()
    {
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(.5f);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rotateAbout();


    }

    void rotateAbout()
    {
        angle += degSecond * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    int headToss()
    {
        if (Random.Range(0, 2) == 0)
        {
            return -1;
        }
        return 1;
    }
}
