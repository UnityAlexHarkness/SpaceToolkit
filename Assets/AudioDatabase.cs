using System;
using UnityEngine;



[Serializable]
public class AudioItem
{
    [SerializeField]
    public string Key;
    [SerializeField]
    public AudioClip Clip;
}

[CreateAssetMenu(fileName="AudioDB", menuName="AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    public AudioItem[] AudioClips;

    public AudioClip GetClip(string key)
    {
        for (int i = 0; i < AudioClips.Length; ++i)
        {
            if (AudioClips[i].Key == key)
            {
                return AudioClips[i].Clip;
            }
        }

        return null;
    }
}
