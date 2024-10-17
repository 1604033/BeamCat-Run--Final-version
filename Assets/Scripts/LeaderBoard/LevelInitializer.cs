using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInitializer : MonoBehaviour
{
    void Start()
    {
        // Start time tracking when the level starts
        TimeTracker.Instance.StartTrackingTime();
    }
}
