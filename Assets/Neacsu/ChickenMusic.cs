using UnityEngine;

public class ChickenMusic : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource chickenMusic;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (backgroundMusic != null && backgroundMusic.isPlaying)
            {
                backgroundMusic.Stop();
            }

            if (chickenMusic != null && !chickenMusic.isPlaying)
            {
                chickenMusic.Play();
            }
        }
    }

}

