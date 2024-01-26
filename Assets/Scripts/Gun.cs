using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int shots, spread, knockback, damage;
    [SerializeField] float reloadTime;
    Timer timer;
    [SerializeField]GameObject bulletType, bulletSpawn;
    Camera cam;
    public bool disable;
    public GunScrOBJ gungun;
    [SerializeField]AudioSource gunShot;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        cam = Camera.main;
        timer = new Timer();
    }
    private void Update()
    {
        if(bulletSpawn.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        spriteRenderer.flipX = false;
        if(!disable)
        aim();
        timer.timeUpdate();
    }
    void aim()
    {
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition)
            - transform.position;
        float angle = Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(-angle+90, Vector3.forward);
        transform.rotation = rotation;
    }
    public Vector2 shoot(float crit, float accur)
    {
        
        if (!timer.timeEnd)
        {
            return Vector2.zero;
        }
        gunShot.Play();
        timer.setTimer(reloadTime);
        for (int i = 0; i < shots; i++)
        {
            GameObject bullet = Instantiate(bulletType, bulletSpawn.transform.position, transform.rotation);
            ProjectileData bulletLogic = bullet.GetComponent<ProjectileData>();
            bulletLogic.damage = damage;
            bulletLogic.crit = crit;
            bullet.transform.rotation = Quaternion.Euler(0,0, transform.rotation.eulerAngles.z + Random.Range(-((spread-spread*accur)/2), (spread - spread * accur)/ 2));

        }
        Vector2 direction = (cam.ScreenToWorldPoint(Input.mousePosition)
            - transform.position).normalized;
        return -direction * knockback;
    }
    public void setParent(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector2.zero-Vector2.down*0.15f;
    }
}
