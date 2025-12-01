using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public AudioClip explosionClip;
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null && explosionClip != null)
            audioSource.PlayOneShot(explosionClip);
    }
}

