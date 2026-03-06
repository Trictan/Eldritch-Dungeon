using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    public static bool soundOn = true;
    
    
    [Header("Enemy")]
    public AudioClip enemyDamage;
    public AudioClip enemyDie;

    [Header("Player")]
    public AudioClip playerDamage;
    public AudioClip playerDie;

    [Header("Game")]
    public AudioClip levelUp;
    public AudioClip buttonClick;
    public AudioClip doorClose;
    public AudioClip doorOpen;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume = 1f;

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

    //-----------3D = things that position is important, 2D = when the place the sound comes from dosent really matter----------
    public void Play2DSound(AudioClip clip)
    {
        if (!soundOn) return;

        audioSource.PlayOneShot(clip, volume);
    }

    public void Play3DSound(AudioClip clip, Vector3 pos)
    {
        if (!soundOn) return;

        AudioSource.PlayClipAtPoint(clip, pos, volume);
    }

    //-------------Enemy Sounds--------------------------
    public void EnemyTakeDamage(Vector3 pos)
    {
        Play3DSound(enemyDamage, pos);
    }
    public void EnemyDie(Vector3 pos)
    {
        Play3DSound(enemyDie, pos);
    }

    //---------------Player sounds--------------------
    public void PlayerTakeDamage(Vector3 pos)
    {
        Play3DSound(playerDamage, pos);
    }
    public void PlayerDie(Vector3 pos)
    {
        Play3DSound(playerDie, pos);
    }

    //------------Game sound-----------------------
    public void LevelUp()
    {
        Play2DSound(levelUp);
    }
    public void ButtonClick()
    {
        Play2DSound(buttonClick);
    }
    public void DoorClose()
    {
        Play2DSound(doorClose);
    }
    public void DoorOpen()
    {
        Play2DSound(doorOpen);
    }

    //-------Help setting---------
    public void ToggleSoundEffect()
    {
        soundOn = !soundOn;
        PlayerPrefs.SetInt("SoundOn", soundOn ? 1 : 0);
    }
}
