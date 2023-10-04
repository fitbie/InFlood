using UnityEngine;


namespace Extensions
{

/// <summary>
/// This class extend AudioSource play methods by adding PlayWithRandomPitch() and PlayOneShotWithRandomPitch()
/// </summary>
public static class AudioSourcePlayRandomPitchExtension
{
    public static void PlayWithRandomPitch(this AudioSource audioSource, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }


    public static void PlayOneShotWithRandomPitch(this AudioSource audioSource, AudioClip clip, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(clip);
    }

}

}
