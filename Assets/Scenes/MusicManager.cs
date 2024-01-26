using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource musicSource;
    public static MusicManager instance;
    Timer timer;
    bool fade;
    void Start()
    {
        timer = new Timer();
        musicSource = GetComponent<AudioSource>();
        instance = this;
    }
    private void Update()
    {
        if (timer != null)
        {
            timer.timeUpdate();
        }
        if(fade)
        {
            musicSource.volume = 1-timer.getPercent();
        }
    }
    // Update is called once per frame
    public void playMusic()
    {
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }
    public void stopMusic()
    {
        if (musicSource != null)
        {
            timer.setTimer(1);
            fade = false;
        }
    }
}
