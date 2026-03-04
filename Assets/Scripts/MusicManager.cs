using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource loopSource;

    private static MusicManager currentMusic;

    void Awake()
    {
        if (currentMusic != null)
        {
            // Gamla musiken får fortsätta tills nya prefab startar
            Destroy(currentMusic.gameObject); // alternativ: crossfade istället
        }

        currentMusic = this;
    }

    void Start()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        if (introSource.clip == null || loopSource.clip == null)
        {
        Debug.LogError("Music clips missing!");
        return;
        }
        
        double t = AudioSettings.dspTime + 0.05; // liten margin för säker buffer
        introSource.PlayScheduled(t);
        loopSource.PlayScheduled(t + introSource.clip.length);
    }
}
