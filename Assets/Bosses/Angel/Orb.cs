using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    GameObject follow;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        follow = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        rb.AddForce((follow.transform.position- transform.position).normalized*10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Boss")
            return;
        Destroy(gameObject);
    }
}
