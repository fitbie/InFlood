using UnityEngine;
using System.Threading.Tasks;

namespace Extensions
{


/// <summary>
/// This class contains method that smoothly change clip from current to new.
/// </summary>
public static class AudioSourceSmoothlyChangeClip
{
    public static void SmoothlyChangeClip(this AudioSource audioSource, AudioClip clip)
    {
        SmoothChange(audioSource, clip);
    }


    
    public static async void SmoothChange(AudioSource audioSource, AudioClip clip)
    {
        float elapsedTime = 0f;
        float currentVolume = audioSource.volume;
        while (elapsedTime <= 0.1f)
        {
            audioSource.volume = Mathf.SmoothStep(currentVolume, 0, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }

        audioSource.clip = clip;
        audioSource.Play();

        elapsedTime = 0f;

        while (elapsedTime <= 0.1f)
        {
            audioSource.volume = Mathf.SmoothStep(0, currentVolume, elapsedTime / 0.1f);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }

}

}
