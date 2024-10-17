using UnityEngine;
using TMPro;

public class PlayerProfileUI : MonoBehaviour
{
    public TMP_InputField nameInputField;
    //public TMP_InputField genderInputField;
    //public TMP_InputField ageInputField;

    public void SaveProfile()
    {
        string playerName = nameInputField.text;
        //string gender = genderInputField.text;
        //int age = int.Parse(ageInputField.text);

        // Update the player's profile
        PlayerProfile.Instance.UpdatePlayerProfile(playerName);

        // You can also add feedback like hiding the menu or showing a message that the profile was saved
    }
}
