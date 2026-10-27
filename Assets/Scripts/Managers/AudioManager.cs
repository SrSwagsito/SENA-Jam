using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set;}

    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] AudioMixerGroup sfxMixer;

    [SerializeField] private int minSFXSources = 5;
    [SerializeField] private int maxSFXSources = 15;

    private AudioSource musicSource;
    private List<AudioSource> sfxSource;

    [SerializeField] Sound[] sounds;

    private Dictionary<string, float> cooldownSound = new Dictionary<string, float>();

    private void Awake()
    {
        if(Instance != null) { Destroy(gameObject); return;}
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetUpMusicSource();
    }

    public void SetUpMusicSource()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = musicMixer;
        musicSource.loop = true;
    }

    public void SetUpSFXSource()
    {
        sfxSource = new List<AudioSource>();
        for(int i = 0; i < minSFXSources; i++)
        {
            CreateNewSFX();
        }
    }

    private AudioSource CreateNewSFX()
    {
        AudioSource sound = gameObject.AddComponent<AudioSource>();
        sound.outputAudioMixerGroup = sfxMixer;

        sound.spatialBlend =  0;
        sfxSource.Add(sound);
     
        return sound;
    }

    private AudioSource GetAvailableSource()
    {
        foreach(AudioSource source in sfxSource)
        {
            if (!source.isPlaying) {
                {
                    return source;
                } }
        }

        if(sfxSource.Count < maxSFXSources)
        {
            return CreateNewSFX();
        }

        AudioSource lowest = sfxSource[0];

        return lowest;
    }

    private Sound GetSound(string name)
    {
        return Array.Find(sounds, s => s.name == name);
    }

    private bool CheckCooldown(Sound s)
    {
        if (s.cooldown <= 0) return true;

        if (!cooldownSound.ContainsKey(s.name))
        {
            cooldownSound[s.name] = Time.time;
                return true;
        }

        if(Time.time - cooldownSound[s.name] >= s.cooldown) return true;

        return false;
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);
        if(sound == null)
        {
            Debug.LogWarning("No existe el sonido " + name); return;
        }

        if (CheckCooldown(sound)) return;

        AudioSource src = GetAvailableSource();

        src.spatialBlend = 0f;
        src.volume = sound.volume;

        AudioClip clip = sound.clips.Length == 1 ? sound.clips[0] : sound.clips[UnityEngine.Random.Range(0, sound.clips.Length)]; 


    }
}
