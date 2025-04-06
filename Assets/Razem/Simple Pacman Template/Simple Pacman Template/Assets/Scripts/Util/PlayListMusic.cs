using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayListMusic : MonoBehaviour
{
    public AudioSource musicAudioSource;

    public Music[] musics;
    public Music actualMusic;

    public float targetVolume = 0.5f;
    private int idMusic;
    public AudioClip middleMusic;
    public bool middle;
    private void Update()
    {
        if (GameController.instance.game == Game.GAMEPLAY && !musicAudioSource.isPlaying)
        {
            PlayMiddleMusic();
            middle = true;
        }

        musicAudioSource.volume = Mathf.Lerp(musicAudioSource.volume, targetVolume, Time.deltaTime * 2.5f);
    }

    public void PlayRandomMusic()
    {
        middle = false;
        StopMusic();
        actualMusic = musics[Random.Range(0, musics.Length)];
        musicAudioSource.PlayOneShot(actualMusic.audio);
    }

    public void PlayNextMusic()
    {
        middle = false;
        StopMusic();
        idMusic++;
        if (idMusic > musics.Length - 1)
            idMusic = 0;
        actualMusic = musics[idMusic];
        musicAudioSource.PlayOneShot(actualMusic.audio);
    }

    public void PlayMiddleMusic()
    {
        StopMusic();
        musicAudioSource.PlayOneShot(middleMusic);
    }

    public void StopMusic()
    {
        musicAudioSource.Stop();
    }

    public void PlayActualMusic()
    {
        middle = false;
        musicAudioSource.PlayOneShot(actualMusic.audio);
    }

    public void DisableMusic()
    {
        targetVolume = 0;
    }



}
