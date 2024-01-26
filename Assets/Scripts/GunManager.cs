using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject currentgun;
    [SerializeField]Player player;
    void Awake()
    {
        currentgun = FindAnyObjectByType<Gun>().gameObject;
        player = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gun_Pickup")
        {
            Gun_Pickup gunReplacement = collision.GetComponent<Gun_Pickup>();
            player.gun = null;
            currentgun.transform.SetParent(currentgun.transform);
            Gun gun = currentgun.GetComponent<Gun>();
            gun.disable = true;
            Rigidbody2D rb = currentgun.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(-3, 5), ForceMode2D.Impulse);
            Destroy(gun.gameObject, 5);
            currentgun = Instantiate(gunReplacement.gun);
            gun = currentgun.GetComponent<Gun>();
            gun.setParent(gameObject);
            player.gun = gun;
            PlayerData.instance.setGun(gun.gungun);
            FightTrigger.instance.endBoss();
        }
    }
}
