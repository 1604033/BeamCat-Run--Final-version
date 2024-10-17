using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompletionUI : MonoBehaviour
{
    public Text levelNameText;
    public Text completionTimeText;
    public Text highestScoreText; // New field for displaying the highest score
    private string levelToReplay;

    void Start()
    {
        // Get the last level played (the level that was just completed)
        levelToReplay = PlayerPrefs.GetString("LastLevelPlayed", "Unknown Level");
        levelNameText.text = levelToReplay;

        // Get the completion time (assuming it's saved after the level completion)
        float completionTime = PlayerPrefs.GetFloat("CompletionTime", 0f);
        completionTimeText.text = FormatTime(completionTime);

        // Get the highest score (fastest time)
        float highestScore = PlayerPrefs.GetFloat("HighestScore", float.MaxValue);
        if (highestScore == float.MaxValue)
        {
            highestScoreText.text = "Best Time: --:--:--"; // If no score, display placeholder
        }
        else
        {
            highestScoreText.text = "Best Time: " + FormatTime(highestScore); // Display the highest score
        }
    }

    // Formats the time into minutes:seconds:milliseconds
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        float milliseconds = (time * 100) % 100; // Adjusted to show two decimal points for milliseconds
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    // Replay button functionality
    public void ReplayLevel()
    {
        if (!string.IsNullOrEmpty(levelToReplay))
        {
            SceneManager.LoadScene(levelToReplay); // Load the previous level
        }
        else
        {
            Debug.LogError("Level to replay is not set correctly.");
        }
    }
}