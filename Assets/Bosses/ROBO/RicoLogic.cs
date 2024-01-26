using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicoLogic : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip impact;
    AudioSource source;

    [SerializeField] GameObject aimLSpot, aimRSpot, laserLSpot, laserRSpot, spraySpot;
    [SerializeField] GameObject aim, laserL, laserR, spray, rico;
    Transform nextpoint;
    Rigidbody2D body;
    Timer attackTime, flashtime;
    bool attacking;
    [SerializeField]SpriteRenderer whiteflash;
    [SerializeField] ParticleSystem deathparticles;
    [SerializeField] GameObject gun, gunspawn;
    [SerializeField] CircleCollider2D collidr;
    
    //Stats
    int health;
    void Awake()
    {
        MusicManager.instance.playMusic();
        source = GetComponent<AudioSource>();
        flashtime = new Timer();
        flashtime.setTimer(0);
        health = 30;
        body = GetComponent<Rigidbody2D>();
        attackTime = new Timer();
        attackTime.setTimer(1.5f);
        nextpoint = spraySpot.transform;
    }

    // Update is called once per frame
    void Update()
    {
        flashtime.timeUpdate();
        whiteflash.color = new Color(1, 1, 1, 1-flashtime.getPercent());
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
        
        attackTime.setTimer(1.5f);
        attacking = false;
        StopAllCoroutines();
    }
    IEnumerator aimAtPlayer()
    {
        yield return new WaitUntil(()=> Vector3.Distance(nextpoint.position, transform.position) <0.1);
        aim.SetActive(true);
        yield return new WaitForSeconds(3);
        
        attackTime.setTimer(1.5f);
        attacking = false;
        StopAllCoroutines();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            source.PlayOneShot(impact);
            flashtime.setTimer(0.25f);
            ProjectileData bullet = collision.GetComponent<ProjectileData>();
            if (bullet.owner == "boss")
                if (Random.value < bullet.crit)
                {
                    bullet.damage += (bullet.damage / 2) + 1;
                    Debug.Log("Crit");
                }
            health -= bullet.damage;
            if (health <= 0)
            {
                //RoomLogic.instance.startEnd = true;
                //Destroy(gameObject);
                StartCoroutine(death());
            }
        }
    }
    IEnumerator death()
    {
        MusicManager.instance.stopMusic();
        collidr.enabled = false;
        body.gravityScale = 0.25f;
        deathparticles.gameObject.SetActive(true);
        this.enabled= false;
        yield return new WaitForSeconds(1f);
        Instantiate(gun, gunspawn.transform);
    }
}
