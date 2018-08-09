using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    private AudioSource audioSource;

    [Header("Dźwięki")]
    public AudioClip defaultSound;
    public AudioClip gameSound;

    public static AudioManager ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound(defaultSound);
    }

    public void ChangeVolume(float range)
    {
        float y = range * 100;
        float volume = -80 + y;
        mainMixer.SetFloat("mainVolume", volume);
    }

    public void PlaySound(AudioClip clip = null)
    {
        if (clip != null)
            audioSource.clip = clip;
        audioSource.Play();
    }
}
