using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] waypoints;
    [SerializeField] GameObject wave, wall, barrageL, barrageR;
    Rigidbody2D rb;
    int wayPointIndex;
    [SerializeField] Vector3 oldWaypoint, nextWaypoint, cVelocity;
    Timer timer, move, moveTime;
    bool attacking;

    //customVar
    [SerializeField] int health;

    void Start()
    {
        health = 20;
        wayPointIndex = waypoints.Length;
        rb = GetComponent<Rigidbody2D>();
        wayPointIndex = 0;
        timer = new Timer();
        moveTime = new Timer();
        move = new Timer();
        move.setTimer(5f);
        cVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (move.timeEnd)
        {
            move.setTimer(5);
            switch (Random.Range(0, 4))
            {
                case 0:
                    StartCoroutine(barrage_List());
                    break;
                case 1:
                    StartCoroutine(wall_List());
                    break;
                case 2:
                    StartCoroutine(barrage_List());
                    break;
            }
        }
        else if(!attacking)
            move.timeUpdate();
        Idle();
        

        timer.timeUpdate();
    }
    //idle code
    void Idle()
    {
        if (transform.position == nextWaypoint && !attacking)
        {
            Instantiate(wave, transform);
            CalcNextPoint();
        }
        moveTime.timeUpdate();
        rb.MovePosition(Vector3.Lerp(oldWaypoint, nextWaypoint, moveTime.getPercent()));
    }
    void setNextPoint(Vector3 next, float time)
    {
        oldWaypoint = transform.position;
        nextWaypoint = next;
        moveTime.setTimer(time);
    }
    void CalcNextPoint()
    {

        timer.setTimer(Random.Range(1, 2));
        wayPointIndex++;
        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
        Vector3 offset = Random.insideUnitCircle;
        setNextPoint( waypoints[wayPointIndex].position + offset, 1);
    }
    //lazer code
    IEnumerator barrage_List()
    {
        attacking = true;
        setNextPoint(waypoints[2].position, 0.5f);
        //Play VFX
        yield return new WaitForSeconds(0.5f);
        //PlayVFX
        Instantiate(barrageL, waypoints[2]);
        yield return new WaitForSeconds(2.5f);
        setNextPoint(waypoints[1].position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Instantiate(barrageR, waypoints[1]);
        yield return new WaitForSeconds(2.5f);
        CalcNextPoint();
        attacking = false;
    }
    //ChargeCode
    //
    IEnumerator wall_List()
    {

        attacking = true;
        setNextPoint( waypoints[0].position, 1);
        yield return new WaitForSeconds(1.5f);
        Instantiate (wall, waypoints[2].position+Vector3.left*3+Vector3.up*3, Quaternion.identity);
        yield return new WaitForSeconds(1);
        CalcNextPoint();
        attacking = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ProjectileData bullet = collision.GetComponent<ProjectileData>();
            if (Random.value < bullet.crit)
            {
                bullet.damage += (bullet.damage / 2) + 1;
                Debug.Log("Crit");
            }
            health -= bullet.damage;
            if (health <= 0)
            {
                RoomLogic.instance.startEnd = true;
                Destroy(gameObject);
            }
        }

    }
}
