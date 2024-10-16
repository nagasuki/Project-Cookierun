using AYellowpaper.SerializedCollections;
using PugDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SoundManager : SingletonMonoBehaviourDontDestroy<SoundManager>
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private SerializedDictionary<string, AudioClip> sounds = new(); // <SoundName, Clip>

    public void PlayeSFX(string soundName)
    {
        PlayClipAyPoint(sounds[soundName], Camera.main.transform.position);
    }

    private void PlayClipAyPoint(AudioClip clip, Vector3 position, float volume = 1f)
    {
        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
    }
}
