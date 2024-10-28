using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup sfxGroup;

    private Sounds[] playingSounds;

    public Sounds[] sounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);


        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.soundName.StartsWith("M_") == true)
                s.source.outputAudioMixerGroup = musicGroup;
            else if (s.soundName.StartsWith("SFX_") == true)
                s.source.outputAudioMixerGroup = sfxGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("M_Theme");
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }
        s.source.Stop();
    }

    public void StopAllSFX()
    {
        playingSounds = sounds.Where(t => t.source.outputAudioMixerGroup == sfxGroup).Where(p => p.source.isPlaying).ToArray();
        if (playingSounds.Length == 0) return;
        foreach (Sounds sound in playingSounds)
            sound.source.Stop();
    }

    public void ResumeAllSFX()
    {
        if (playingSounds.Length == 0) return;
        foreach (Sounds sound in playingSounds)
            sound.source.Play();
    }
}
