using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();  // Pornește muzica automat
    }

    public void ToggleMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();  // Oprește muzica
        else
            audioSource.Play();   // Reia muzica
    }
}