using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 input;
    PhysicsController physics;
    Rigidbody2D rb;
    [SerializeField]int speed, healthInt, maxhealthInt;
    public Gun gun;
    //Classes i need
    PlayerData instance = PlayerData.instance;
    [SerializeField]Health_UI health;
    void Start()
    {
        //
        //
        maxhealthInt = instance.maxHealthR();
        rb = GetComponent<Rigidbody2D>();
        physics = new PhysicsController(rb);
        gun = Instantiate(instance.getGun().prefGun.GetComponent<Gun>());
        gun.setParent(gameObject);
        healthInt = maxhealthInt;
        health.setMax(maxhealthInt);
        health.setHealth(maxhealthInt);
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        //input.y = Input.GetAxis("Vertical");
        physics.returnVelocity(input * PlayerData.instance.speedR());
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            physics.AddForce(gun.shoot(PlayerData.instance.critR(), PlayerData.instance.accuracyR()));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            ProjectileData bullet = collision.GetComponent<ProjectileData>();
            if (Random.value < bullet.crit)
                bullet.damage += (bullet.damage / 2) + 1;
            else
            {
                bullet.damage = (int)Mathf.Clamp(bullet.damage - PlayerData.instance.damagereductionR(), 1, bullet.damage);
            }

            if(Random.value < PlayerData.instance.dogdeR())
            {
                return;
            }

            healthInt -= bullet.damage;
            
            health.setHealth( healthInt );
        }
    }
}
