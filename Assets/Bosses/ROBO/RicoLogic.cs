using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicoLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject aimLSpot, aimRSpot, laserLSpot, laserRSpot, spraySpot;
    [SerializeField] GameObject aim, laserL, laserR, spray, rico;
    Transform nextpoint;
    Rigidbody2D body;
    Timer attackTime;
    bool attacking;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        attackTime = new Timer();
        attackTime.setTimer(1.5f);
        nextpoint = spraySpot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(nextpoint.transform.position, transform.position) > 0.1)
            body.velocity = Vector3.ClampMagnitude(nextpoint.position - transform.position, 1) * 4;
        else
            body.velocity = Vector3.zero;
        if (!attacking)
        attackTime.timeUpdate();
        if (attackTime.timeEnd && !attacking)
        {
            attacking = true;
            switch (Random.Range(0, 5))
            {
                case 0:
                    nextpoint = aimLSpot.transform;
                    StartCoroutine(aimAtPlayer());
                    break;
                case 1:
                    nextpoint = aimRSpot.transform;
                    StartCoroutine(aimAtPlayer());
                    break;
                case 2:
                    nextpoint = laserLSpot.transform;
                    StartCoroutine (imafirenmalaser(laserL, 3));
                    break;
                case 3:
                    nextpoint = laserRSpot.transform;
                    StartCoroutine(imafirenmalaser(laserR, 3));
                    break;
                case 4:
                    nextpoint = spraySpot.transform;
                    StartCoroutine(imafirenmalaser(spray, 2));
                    break;
                default:
                    nextpoint = randomSpot().transform;
                    StartCoroutine(imafirenmalaser(rico, 0.75f));
                    break;
            }
        }
    }
    GameObject randomSpot()
    {
        switch(Random.Range(0, 5))
        {
            case 0:return aimLSpot;
            case 1: return aimRSpot;
            case 2: return laserLSpot;
            case 3: return spraySpot;
            default: return laserRSpot;
        }
    }
    IEnumerator imafirenmalaser(GameObject lazer, float attacktime)
    {
        yield return new WaitUntil(() => Vector3.Distance(nextpoint.position, transform.position) < 0.1);
        lazer.SetActive(true);
        yield return new WaitForSeconds(attacktime);
        attacking = false;
        attackTime.setTimer(1.5f);
        StopCoroutine(imafirenmalaser(lazer, 0));
    }
    IEnumerator aimAtPlayer()
    {
        yield return new WaitUntil(()=> Vector3.Distance(nextpoint.position, transform.position) <0.1);
        aim.SetActive(true);
        yield return new WaitForSeconds(3);
        attacking = false;
        attackTime.setTimer(1.5f);
        StopCoroutine(aimAtPlayer());
    }
}
