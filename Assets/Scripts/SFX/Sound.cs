using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;

    public AudioClip[] clips;

    [Range(0f, 1f)] public float volume = 1f;

    public bool loop = false;
    public bool isMusic = false;

    public float cooldown = 0f;

    public int priority = 5;
}