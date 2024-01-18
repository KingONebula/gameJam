using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAimPewPewPew : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    [SerializeField]GameObject spawn, bullet;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(pewpewpew());
    }
    IEnumerator pewpewpew()
    {
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(1);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(1);
        Instantiate(bullet, spawn.transform.position, transform.rotation);
        yield return new WaitForSeconds(1);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }


    private void Update()
    {
        aim();
    }
    void aim()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(-angle + 90, Vector3.forward);
            transform.rotation = rotation;
        }
    }
}
