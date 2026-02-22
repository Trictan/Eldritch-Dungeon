using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip introMusic;
    public AudioClip loopMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayIntro();
    }

    void PlayIntro()
    {
        audioSource.clip = introMusic;
        audioSource.loop = false;
        audioSource.Play();

        Invoke(nameof(PlayLoop), introMusic.length); //No fading right now
    }

    void PlayLoop()
    {
        audioSource.clip = loopMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}
