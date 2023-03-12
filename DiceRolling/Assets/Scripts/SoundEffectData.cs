using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New SoundEffect", menuName = "SoundEffects")]

public class SoundEffectData : ScriptableObject
{
    public AudioClip[] audioClips;
    public AudioMixerGroup mixerGroup;

    public bool doLoopClip;
    
    [Range(0, 1)]
    public float volume;

    public bool doRandomizePitch;

    [Range(0, 1)]
    public float pitchRange;
}
