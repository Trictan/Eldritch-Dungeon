using UnityEngine;
using System.Collections;

public class MusicManager : tungtungskibscob
{
    public AudioSource introSource;
    public AudioSource loopSource;

    private static MusicManager currentMusic;
    private static bool musicOn = true;

    public static MusicManager Instance
    {
        get
        {
            if (currentMusic == null)
            {
                currentMusic = FindObjectOfType<MusicManager>();
            }
            return currentMusic;
        }
    }

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
        // Check if music on/off
        UpdateVolume();
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

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        float vol = musicOn ? 1f : 0f;
        introSource.volume = vol;
        loopSource.volume = vol;
    }
}
