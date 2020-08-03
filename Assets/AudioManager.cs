using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager AudioManagerInstance;

    public AudioSource AudioSource;

    public AudioDatabase AudioDB;
    
    public static AudioManager Instance
    {
        get { return AudioManagerInstance; }
    }

    public void OnEnable()
    {
        AudioManagerInstance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayOneShot(string key)
    {
        AudioSource.PlayOneShot(AudioDB.GetClip(key));
    }
}
