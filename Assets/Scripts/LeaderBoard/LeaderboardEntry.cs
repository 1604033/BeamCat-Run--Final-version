using System;

public class LeaderboardEntry
{
    public string PlayerName { get; private set; }
    public float Time { get; set; } // Make the setter public so it can be updated

    public LeaderboardEntry(string playerName, float time)
    {
        PlayerName = playerName;
        Time = time;
    }
}

