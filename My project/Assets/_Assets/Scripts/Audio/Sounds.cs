using UnityEngine;

public class Sounds : MonoBehaviour
{
    public string soundName;

    public AudioClip clip;

    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 3f)] public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;
}
