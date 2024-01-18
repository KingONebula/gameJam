using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyOnAwak : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int force;
    int countCollision = 0;
    private void OnEnable()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * transform.right, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        countCollision++;
        if (countCollision == 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy (gameObject);
    }
}
