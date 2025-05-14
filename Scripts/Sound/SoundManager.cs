using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Slider(최소 0, 최대 1f)를 적용할 실제 볼륨 크기값들.
    [Range(0f, 1f)] private float soundEffectVolume;
    [Range(0f, 1f)] private float soundEffectPitchVariance;
    [Range(0f, 1f)] private float musicVolume;
    [Range(0f, 1f)] private float masterVolume;
    
    public float MasterVolume
    {
        get => masterVolume;
        set
        {
            masterVolume = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        }
    }

    public float SoundEffectVolume
    { get => soundEffectVolume; set
        {
            soundEffectVolume = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("SoundEffectVolume", soundEffectVolume);
            if (musicAudioSource != null)
                musicAudioSource.volume = soundEffectVolume;
        }
    }
    
    public float SoundEffectPitchVariance
    {
        get => soundEffectPitchVariance; set
        {
            soundEffectPitchVariance = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("SoundEffectPitch", soundEffectPitchVariance);
            if (musicAudioSource != null)
                musicAudioSource.volume = soundEffectPitchVariance;
        }
    }
    public float MusicVolume
    { get => musicVolume; set
        {
            musicVolume = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
            if (musicAudioSource != null)
                musicAudioSource.volume = musicVolume;
        }
    }
    
    private AudioSource musicAudioSource;
    public AudioClip musicClip;

    public SoundSource soundSourcePrefab;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        //이전 조절한 값 있으면 불러오고 없으면 중간값 설정.
        soundEffectVolume = PlayerPrefs.HasKey("SoundEffectVolume") ? PlayerPrefs.GetFloat("SoundEffectVolume") : 0.5f;
        soundEffectPitchVariance = PlayerPrefs.HasKey("SoundEffectPitch") ? PlayerPrefs.GetFloat("SoundEffectPitch") : 0.5f;
        musicVolume = PlayerPrefs.HasKey("MusicVolume") ? PlayerPrefs.GetFloat("MusicVolume") : 0.5f;
        masterVolume = PlayerPrefs.HasKey("MasterVolume") ? PlayerPrefs.GetFloat("MasterVolume") : 0.5f;
        
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        ChangeBackGroundMusic(musicClip);
    }
    
    public void ChangeBackGroundMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(Instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitchVariance); 
    }
}
