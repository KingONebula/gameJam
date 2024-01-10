using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenTrigger : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Vector3 down, up;
    Timer timer;
    bool Up;
    IEnumerator goUp()
    {
        timer = new Timer();
        timer.setTimer(4);
        Up = true;
        player.enabled = false;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.gameObject.transform.SetParent(transform, true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(End_Screen.instance.loadUI());
    }
    public void startElevator()
    {
        StartCoroutine(goUp());
    }
    // Update is called once per frame
    void Update()
    {
        if (Up)
        {
            timer.timeUpdate();
            transform.position = Vector3.Lerp(down, up, timer.getPercent());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            startElevator();
        }
    }
}
