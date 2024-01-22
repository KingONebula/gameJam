using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int moveRange, speed;
    [SerializeField] Transform top, player;
    Vector3 target;
    Rigidbody2D body;
    bool attacking;
    Timer timer;
    void Start()
    {
        timer = new Timer();
        timer.setTimer(4);
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer.timeUpdate();
        if (timer.timeEnd && !attacking)
        {
            body.velocity = Vector3.zero;
            body.gravityScale = 1.5f;
            attacking = true;
        }
        if(!attacking)
        idle();



    }
    void idle()
    {
        body.AddForce(getNextPoint()*speed);
        if (Mathf.Abs(transform.position.x - target.x) < 1)
        {
            //yield return new WaitForSeconds(0.5f);
            setNextPoint();
        }
    }
    Vector3 getNextPoint()
    {
        float x = (target.x-transform.position.x);
        return new Vector2(x, 0).normalized;
    }
    void setNextPoint()
    {
        target = top.position;
        target.x+= Random.Range(-(float)moveRange, (float)moveRange);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        timer.setTimer(20);
        body.gravityScale = 0;
        body.velocity = Vector3.zero;
        transform.position = top.position;
        attacking = false;
    }
}
