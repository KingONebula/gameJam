using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorIntro : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    [SerializeField] Vector3 down, up;
    Timer timer;
    IEnumerator Start()
    {
        timer = new Timer();
        timer.setTimer(3);
        player.enabled = false;
        yield return new WaitForSeconds(3.25f);
        player.transform.SetParent(null);
        player.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer.timeUpdate();
        transform.position = Vector3.Lerp(down, up, timer.getPercent());
    }
}
