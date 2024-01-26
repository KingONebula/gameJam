using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardLogic2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] waypoints;
    [SerializeField] GameObject wave, wall, barrageL, barrageR,handL, handR,triPref;
    Rigidbody2D rb;
    int wayPointIndex;
    [SerializeField] Vector3 oldWaypoint, nextWaypoint, cVelocity;
    Timer timer, move, moveTime, acceleration, flashtime;
    bool attacking, aceelBool;
    [SerializeField]Animator animator;
    [SerializeField] SpriteRenderer whiteflash;
    //customVar
    [SerializeField] int health;



    [SerializeField] CircleCollider2D collidr;
    [SerializeField] ParticleSystem deathparticles;
    [SerializeField] GameObject gun;
    Rigidbody2D body;

    void Awake()
    {
        MusicManager.instance.playMusic();
        body = GetComponent<Rigidbody2D>();
        flashtime = new Timer();
        flashtime.setTimer(0);
        acceleration = new Timer(3);
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
        flashtime.timeUpdate();
        whiteflash.color = new Color(1, 1, 1, 1 - flashtime.getPercent());
        if (move.timeEnd)
        {
            move.setTimer(2);
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartCoroutine(tri_Attack());
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

            wayPointIndex = 1;
        }
        Vector3 offset = Random.insideUnitCircle;
        setNextPoint( waypoints[wayPointIndex].position + offset, 2);
    }
    //lazer code
    IEnumerator barrage_List()
    {
        attacking = true;
        setNextPoint(waypoints[2].position, 0.5f);
        //Play VFX
        yield return new WaitForSeconds(0.5f);
        //PlayVFX
        barrageL.SetActive(true);
        handL.SetActive(false);
        yield return new WaitForSeconds(3f);
        barrageL.SetActive(false);
        handL.SetActive(true);
        setNextPoint(waypoints[1].position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        handR.SetActive(false);
        barrageR.SetActive(true);
        yield return new WaitForSeconds(3f);
        barrageR.SetActive(false);
        handR.SetActive(true);
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
        animator.SetTrigger("wall");
        Instantiate(wall, waypoints[2].position + Vector3.left * 3 + Vector3.up * 5, Quaternion.identity);
        yield return new WaitForSeconds(3);
        CalcNextPoint();
        attacking = false;

    }
    IEnumerator tri_Attack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        attacking = true;
        setNextPoint(waypoints[Random.Range(0,2)].position, 1);
        yield return new WaitForSeconds(1.5f);
        Instantiate(triPref, player.transform.position, Quaternion.Euler(0,0,0));
        yield return new WaitForSeconds(1.5f);
        Instantiate(triPref, player.transform.position, Quaternion.Euler(0, 0, 60));
        yield return new WaitForSeconds(1.5f);
        Instantiate(triPref, player.transform.position, Quaternion.Euler(0, 0, 120));
        yield return new WaitForSeconds(1);
        CalcNextPoint();
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            flashtime.setTimer(0.25f);
            ProjectileData bullet = collision.GetComponent<ProjectileData>();
            if(bullet.owner=="boss")
            if (Random.value < bullet.crit)
            {
                bullet.damage += (bullet.damage / 2) + 1;
                Debug.Log("Crit");
            }
            health -= bullet.damage;
            if (health <= 0)
            {
                StartCoroutine(death());
                print("pp");
            }
        }

    }
    void movePosition()
    {
        acceleration.timeUpdate();
    }
    IEnumerator death()
    {
        MusicManager.instance.stopMusic();
        collidr.enabled = false;
        body.gravityScale = 0.25f;
        deathparticles.gameObject.SetActive(true);
        this.enabled = false;
        yield return new WaitForSeconds(1f);
        gun.SetActive(true);
    }
}
