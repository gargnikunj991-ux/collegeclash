using UnityEngine;

public class HitSound : MonoBehaviour
{
    public static HitSound Instance;
    AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.Play();
    }
}