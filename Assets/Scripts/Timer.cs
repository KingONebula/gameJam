using UnityEngine;

[System.Serializable]
public class Timer
{
    float timeCount, timeLength;
    public bool timeEnd, doesTick;
    public int tickCount, tick;
    // Start is called before the first frame update

    // Update is called once per frame
    public Timer()
    {
        doesTick = false;
        tickCount = 0;
        tick = 0;
    }
    public Timer(int tc)
    {
        doesTick = true;
        tickCount = tc;
    }
    public void timeUpdate()
    {
        timeEnd = false;
        timeCount = Mathf.Clamp(timeCount + Time.unscaledDeltaTime, 0, timeLength);
        if (timeCount == timeLength)
        {
            if(!doesTick)
            timeEnd = true;
            if (doesTick)
            {
                tick++;
                if (tick == tickCount)
                {
                    timeEnd = true;
                    tick = 0;
                }
                changeTime(timeLength);

            }
        }
    }
    public void setTimer(float Length)
    {
        timeLength = Length;
        timeCount = 0;
        timeEnd = false;
    }
    public void changeTime(float Length)
    {
        timeLength = Length;
        timeCount = 0;
    }
    public float getPercent()
    {
        if (timeLength == 0)
            return 0;
        Debug.Log(timeCount);
        return timeCount / timeLength;
    }
}
