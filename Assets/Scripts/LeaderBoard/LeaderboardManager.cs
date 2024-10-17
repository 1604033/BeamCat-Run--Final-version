using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    private List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*public void AddEntry(string playerName, float completionTime)
    {
        // Check if player already exists in the leaderboard
        LeaderboardEntry existingEntry = leaderboardEntries.Find(entry => entry.PlayerName == playerName);

        if (existingEntry != null)
        {
            // If player already exists, update the time only if the new time is lower
            if (completionTime < existingEntry.Time)
            {
                existingEntry.Time = completionTime;
                Debug.Log($"Updated {playerName}'s time to: {completionTime}");
            }
            else
            {
                Debug.Log($"{playerName}'s time wasn't updated. The new time is higher than the previous one.");
            }
        }
        else
        {
            // If player doesn't exist, add a new entry
            leaderboardEntries.Add(new LeaderboardEntry(playerName, completionTime));
            Debug.Log($"Added {playerName} with time: {completionTime}");
        }
    }*/

    public void AddOrUpdateEntry(string playerName, float completionTime)
{
    LeaderboardEntry existingEntry = leaderboardEntries.Find(entry => entry.PlayerName == playerName);

    if (existingEntry != null)
    {
        // Update the existing entry if the new time is better (lower)
        if (completionTime < existingEntry.Time)
        {
            existingEntry.Time = completionTime;
        }
    }
    else
    {
        // Add new entry if the player is not on the leaderboard yet
        leaderboardEntries.Add(new LeaderboardEntry(playerName, completionTime));
    }

    // Sort the leaderboard by completion time (ascending)
    leaderboardEntries.Sort((x, y) => x.Time.CompareTo(y.Time));
}


    public List<LeaderboardEntry> GetEntries()
    {
        // Sort the leaderboard by completion time (ascending)
        leaderboardEntries.Sort((x, y) => x.Time.CompareTo(y.Time));
        return leaderboardEntries;
    }
}
