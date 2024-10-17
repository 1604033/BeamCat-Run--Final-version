using UnityEngine;
using TMPro; // If using TextMeshPro

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI element (or use Text if not using TMP)
    private TimeTracker timeTracker;

    void Start()
    {
        timeTracker = TimeTracker.Instance; // Get reference to the TimeTracker instance
    }

    void Update()
    {
        // Update the timer UI based on the elapsed time from the TimeTracker
        float elapsedTime = timeTracker.GetElapsedTime();
        UpdateTimerUI(elapsedTime);
    }

    // Format the elapsed time and display it
    void UpdateTimerUI(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        float milliseconds = (elapsedTime * 100) % 100; // Two decimal places for milliseconds

        // Format the time into a string with two-digit precision for milliseconds
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
