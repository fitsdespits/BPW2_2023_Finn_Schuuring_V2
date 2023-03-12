using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTools : MonoBehaviour
{
    public static void LoadSoundEffect(AudioSource audioSource, SoundEffectData soundEffect)
    {
        if(soundEffect.doRandomizePitch)
        {
            audioSource.pitch = Random.Range(1 - soundEffect.pitchRange, 1 + soundEffect.pitchRange);
        }

        audioSource.outputAudioMixerGroup = soundEffect.mixerGroup;
        audioSource.volume = soundEffect.volume;
        AudioClip clip = soundEffect.audioClips[Random.Range(0,soundEffect.audioClips.Length)];
        
        if (soundEffect.doLoopClip)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
            return;
        }
        audioSource.PlayOneShot(clip);
    }
}
