using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public GameObject rankPrefab;
    public GameObject playerNamePrefab;
    public GameObject timePrefab;

    void Start()
    {
        DisplayLeaderboard();
    }

    void DisplayLeaderboard()
    {
        // Clear existing leaderboard entries to prevent overlapping
        foreach (Transform child in leaderboardPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Fetch leaderboard entries from the manager
        List<LeaderboardEntry> entries = LeaderboardManager.Instance.GetEntries();

        // Sort the entries by time (assuming ascending order: lower time is better)
        entries.Sort((entry1, entry2) => entry1.Time.CompareTo(entry2.Time));

        for (int i = 0; i < entries.Count; i++)
        {
            CreateLeaderboardEntryUI(i + 1, entries[i].PlayerName, entries[i].Time);
        }
    }

    public void CreateLeaderboardEntryUI(int rank, string playerName, float completionTime)
    {
        // Create ranking text (1st, 2nd, etc.)
        TextMeshProUGUI rankText = Instantiate(rankPrefab, leaderboardPanel.transform).GetComponent<TextMeshProUGUI>();
        rankText.text = rank.ToString() + GetOrdinalSuffix(rank);

        // Create player name text
        TextMeshProUGUI playerNameText = Instantiate(playerNamePrefab, leaderboardPanel.transform).GetComponent<TextMeshProUGUI>();
        playerNameText.text = playerName;

        // Create completion time text
        TextMeshProUGUI timeText = Instantiate(timePrefab, leaderboardPanel.transform).GetComponent<TextMeshProUGUI>();
        
        TimeSpan time = TimeSpan.FromSeconds(completionTime);
        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Minutes, time.Seconds, time.Milliseconds/10);
        timeText.text = formattedTime;
    }

    private string GetOrdinalSuffix(int rank)
    {
        if (rank % 100 >= 11 && rank % 100 <= 13)
        {
            return "th";
        }
        else if (rank % 10 == 1)
        {
            return "st";
        }
        else if (rank % 10 == 2)
        {
            return "nd";
        }
        else if (rank % 10 == 3)
        {
            return "rd";
        }
        else
        {
            return "th";
        }
    }
}
