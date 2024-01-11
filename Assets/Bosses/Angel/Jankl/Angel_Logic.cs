using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Transform[] waypoints;
    [SerializeField] GameObject orbs, beam, bullets;
    Rigidbody2D rb;
    int wayPointIndex;
    [SerializeField]Vector3 nextWaypoint, cVelocity;
    Timer timer, move;
    bool attacking, charge;

    //customVar
    [SerializeField]int health;

    void Start()
    {
        health = 20;
        wayPointIndex = waypoints.Length;
        rb = GetComponent<Rigidbody2D>();
        wayPointIndex = 0;
        timer = new Timer();
        move = new Timer();
        move.setTimer(5f);
        cVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (move.timeEnd)
        {
            move.setTimer(3);
            switch (Random.Range(0,5))
            {
                case 0:
                    StartCoroutine(lazer_List());
                    break;
                case 1:
                    StartCoroutine(Obr_Lis());
                    break;
                case 2:
                    changedirection();
                    break;
                case 3:
                    changedirection();
                    break;
            }
        }
        else if (!attacking) {
            Idle();
            move.timeUpdate();
        }
        timer.timeUpdate();
    }
    //idle code
    void Idle()
    {
        if(transform.position == nextWaypoint)
        {
            CalcNextPoint();
        }
        
        rb.MovePosition(Vector3.SmoothDamp(transform.position, nextWaypoint, ref cVelocity, 5f * Time.deltaTime));
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
        nextWaypoint = waypoints[wayPointIndex].position + offset;
    }
    //lazer code
    IEnumerator lazer_List()
    {
        rb.bodyType = RigidbodyType2D.Static;
        attacking = true;
        transform.position = new Vector2(100, 100);
        //Play VFX
        yield return new WaitForSeconds(0);
        transform.position = new Vector2(0, 8);
        //PlayVFX
        yield return new WaitForSeconds(0);
        Instantiate(beam, transform);
        //LazyLogic
        yield return new WaitForSeconds(3);
        rb.bodyType = RigidbodyType2D.Dynamic;
        attacking = false;
    }
    //ChargeCode
    void changedirection()
    {
        attacking = true;
        rb.velocity = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized * 15;
        nextWaypoint = rb.velocity;
        charge = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attacking && !charge)
            return;
        rb.velocity = Vector3.Reflect(nextWaypoint, collision.GetContact(0).normal);
        nextWaypoint = rb.velocity - rb.velocity.normalized * 3;
        if (rb.velocity.magnitude <= 9)
        {
            charge = false;
            CalcNextPoint();
            attacking = false;
            return;
        }


    }
    //
    IEnumerator Obr_Lis()
    {
        rb.bodyType = RigidbodyType2D.Static;
        attacking = true;
        transform.position = new Vector2(100, 100);
        //Play VFX
        yield return new WaitForSeconds(0);
        transform.position = new Vector2(0, 8);
        //Play VFX
        yield return new WaitForSeconds(0);
        int i = 0;
        while (i < 3)
        {
            yield return new WaitForSeconds(1);
            Instantiate(orbs, transform.position, Quaternion.identity);
            i++;
        }
        attacking = false;
        rb.bodyType = RigidbodyType2D.Dynamic;

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
