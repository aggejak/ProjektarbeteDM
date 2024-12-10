using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSound; // Drag your sound effect here in the Inspector
    private AudioSource audioSource;

    private void Awake()
    {
        // Add or find an AudioSource component on the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure the AudioSource doesn't loop and plays sound effects at a proper volume
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        // Add the click event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        else
        {
            Debug.LogWarning("No click sound assigned to " + gameObject.name);
        }
    }
}