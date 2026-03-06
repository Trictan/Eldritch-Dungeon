using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    public AudioClip enemyDamage;
    public AudioClip playerDamage;
    private bool soundOn = true;

    private float vol = 1f;

    void Awake()
    {
        //Make sure only one GameObject through everything
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EnemyTakeDamage(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(enemyDamage, pos, vol);
    }
    public void PlayerTakeDamage(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(playerDamage, pos, vol);
    }

    public void ToggleSoundEffect()
    {
        soundOn = !soundOn;
        PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
        //Sets the music
        float vol = soundOn ? 1f : 0f;
    }
}
