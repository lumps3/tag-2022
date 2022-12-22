using System;

public enum LastResult
{
    None,
    Won,
    Lost
}

public class GameValues {

    public static string username  { get; set; }
    public static int lives { get; set; } = 3;
    public static int level { get; set; } = 1;
    public static LastResult lastResult  { get; set; } = LastResult.None;
    public static int elapsedTimeMillis { get; set; }

    public static string ElapsedTimeMillisString()
    {
        return MillisToString(elapsedTimeMillis);
    }

    public static void AddToElapsedTime(float startTime, float endTime)
    {
        elapsedTimeMillis = elapsedTimeMillis + (int)Math.Round(TimeSpan.FromSeconds(endTime - startTime).TotalMilliseconds);
    }

    public static string MillisToString(int millis)
    {
        return TimeSpan.FromMilliseconds(millis).ToString(@"mm\:ss\.fff");
    }

    public static void Reset()
    {
        lives = 3;
        level = 1;
        lastResult = LastResult.None;
        elapsedTimeMillis = 0;
    }
}
