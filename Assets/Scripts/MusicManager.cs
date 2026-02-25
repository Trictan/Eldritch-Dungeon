using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource loopSource;

    void Start()
    {
        double t = AudioSettings.dspTime + 0.2;

        introSource.PlayScheduled(t);
        loopSource.PlayScheduled(t + introSource.clip.length);
    }
}
