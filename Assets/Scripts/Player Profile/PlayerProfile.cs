using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile Instance;

    public string playerName = "Player";
    //public string gender;
    //public int age;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of PlayerProfile
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

    // This function can be called when the player updates their profile
    public void UpdatePlayerProfile(string newName)
    {
        playerName = newName;
    }
    /*public void UpdatePlayerProfile(string newName, string newGender, int newAge)
    {
        playerName = newName;
        gender = newGender;
        age = newAge;
    }*/
}
