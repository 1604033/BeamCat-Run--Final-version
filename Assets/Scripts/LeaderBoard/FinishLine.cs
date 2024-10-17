using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string playerName = PlayerProfile.Instance.playerName; // Fetch the player name
            float completionTime = TimeTracker.Instance.GetElapsedTime(); // Fetch the completion time

            // Add player's completion time to the leaderboard
            LeaderboardManager.Instance.AddOrUpdateEntry(playerName, completionTime);

            // Store current level's name and completion time
            PlayerPrefs.SetString("LastLevelPlayed", SceneManager.GetActiveScene().name);
            PlayerPrefs.SetFloat("CompletionTime", completionTime);

            // Check if this is the fastest time and update highest score if necessary
            float highestScore = PlayerPrefs.GetFloat("HighestScore", float.MaxValue); // Default to large number
            if (completionTime < highestScore)
            {
                PlayerPrefs.SetFloat("HighestScore", completionTime);
            }

            // Load the level completion scene
            SceneManager.LoadScene("Level Completion"); // Replace with the actual scene name
        }
    }
}