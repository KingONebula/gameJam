using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    int startAngle;
    int degSecond;
    float angle;
    bool start, end;
    public float distance;
    [SerializeField]GameObject hitbox;
    void Start()
    {
        int headTos = headToss();
        startAngle = 90 * headTos;
        angle = startAngle;
        degSecond = -60 * headTos;
        StartCoroutine(orderOfOperations());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (start)
        {
            rotateAbout();
        }


    }
    IEnumerator orderOfOperations()
    {
        //PlayAnim
        yield return new WaitForSeconds(0.5f);
        hitbox.SetActive(true);
        start = true;
        yield return new WaitUntil(()=>end);
        hitbox.SetActive(false);
        //PlayAnim
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    void rotateAbout()
    {
        angle += degSecond * Time.deltaTime;
        if (angle >= -startAngle/startAngle * 50 && startAngle < 0)
        {
            start = false;
            end = true;
        }
        else if (angle <= -startAngle / startAngle * 50 && startAngle > 0)
        {
            start = false;
            end = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        distance = Physics2D.Raycast(transform.position, -transform.up, 50, 1 << 7).distance;
        hitbox.transform.localScale = Vector3.up * distance + Vector3.right + Vector3.forward;
    }
    int headToss()
    {
        if(Random.Range(0, 2) == 0)
        {
            return -1;
        }
         return 1;
    }
}
