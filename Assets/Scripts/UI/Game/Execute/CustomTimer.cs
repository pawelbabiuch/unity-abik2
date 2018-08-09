public class CustomTimer
{
    private float seconds;
    private int minuteCounts;
    private int hourCounts;

    public CustomTimer()
    {
        seconds = 0;
        minuteCounts = 0;
        hourCounts = 0;
    }

    public void UpdateTimer(float deltaTime)
    {
        UpdateSecond(deltaTime);
        UpdateMinute();
        UpdateHour();
    }

    public void Reset()
    {
        seconds = 0.0f;
        minuteCounts = 0;
        hourCounts = 0;
    }

    public string ToSeconds()
    {
        if (seconds >= 10.0f)
            return ((int)seconds).ToString();
        else
            return "0" + ((int)seconds).ToString();
    }

    public string ToMinutes()
    {
        if (minuteCounts > 9)
            return minuteCounts.ToString() + ":" + ToSeconds();
        else
            return "0" + minuteCounts.ToString() + ":" + ToSeconds();
    }

    public string ToHours()
    {
        if (hourCounts > 9)
            return hourCounts.ToString() + ":" + ToSeconds();
        else
            return "0" + hourCounts.ToString() + ":" + ToSeconds();
    }

    private void UpdateSecond(float deltaTime)
    {
        seconds += deltaTime;
    }

    private void UpdateMinute()
    {
        if (seconds >= 60.0f)
        {
            minuteCounts++;
            seconds -= 60.0f;
        }
    }

    private void UpdateHour()
    {
        if (minuteCounts >= 60)
        {
            hourCounts++;
            minuteCounts -= 60;
        }
    }
}
