using UnityEngine;

public class TimeTracker : MonoBehaviour
{
    public static TimeTracker Instance { get; private set; }

    private float startTime;
    private bool isTrackingTime = false;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of TimeTracker
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

    public void StartTrackingTime()
    {
        Debug.Log("Time tracking started!");  // Log to ensure this is called
        startTime = Time.time;
        isTrackingTime = true;
    }

    public void StopTrackingTime()
    {
        isTrackingTime = false;
    }

    // New method to get the elapsed time
    public float GetElapsedTime()
    {
        if (isTrackingTime)
        {
            return Time.time - startTime;
        }
        return 0f;
    }
}
