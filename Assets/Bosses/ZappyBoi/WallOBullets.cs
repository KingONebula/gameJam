using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOBullets : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    Timer timer;
    Rigidbody2D rb;
    void Start()
    {
        timer = new Timer();
        timer.setTimer(3);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(speed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer.timeUpdate();
        if (timer.timeEnd)
            Destroy(gameObject);
    }
}
