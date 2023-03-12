using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public SoundEffectData ambience;

    public void Awake()
    {
        LevelTools.LoadSoundEffect(audioSource, ambience);
    }
}
