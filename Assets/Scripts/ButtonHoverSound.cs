using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound;   // Assign this in the inspector with your hover sound clip
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure there is an AudioSource component on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // This method gets called when the pointer enters the button's area
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    // Play the hover sound
    private void PlayHoverSound()
    {
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
