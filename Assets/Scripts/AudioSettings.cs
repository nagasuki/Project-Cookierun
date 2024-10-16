using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Class for managing audio settings.
/// </summary>
public class AudioSettings : MonoBehaviour
{
    /// <summary>
    /// The audio mixer used for adjusting the volume of different audio sources.
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// The slider for adjusting the master volume.
    /// </summary>
    public Slider masterSlider;

    /// <summary>
    /// The slider for adjusting the background music volume.
    /// </summary>
    public Slider bgmSlider;

    /// <summary>
    /// The slider for adjusting the sound effects volume.
    /// </summary>
    public Slider sfxSlider;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        // Add event listeners for the sliders' value changes
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        // Set the sliders' initial values to the stored volume values in PlayerPrefs
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    /// <summary>
    /// Sets the master volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetMasterVolume(float volume)
    {
        // Set the master volume in the audio mixer
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        // Store the volume in PlayerPrefs
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    /// <summary>
    /// Sets the background music volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetBGMVolume(float volume)
    {
        // Set the background music volume in the audio mixer
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        // Store the volume in PlayerPrefs
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    /// <summary>
    /// Sets the sound effects volume.
    /// </summary>
    /// <param name="volume">The volume to set.</param>
    public void SetSFXVolume(float volume)
    {
        // Set the sound effects volume in the audio mixer
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        // Store the volume in PlayerPrefs
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
