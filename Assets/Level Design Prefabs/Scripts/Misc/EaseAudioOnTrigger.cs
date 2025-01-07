using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseAudioOnTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public float easeSpeed = 1.0f;

    public static bool isClimberInside = false;
    public float targetVolume = 0;
    public float initialVolume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            isClimberInside = true;
            targetVolume = 1.0f;
            initialVolume = audioSource.volume;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            isClimberInside = false;
            targetVolume = 0f;
            initialVolume = audioSource.volume;
        }
    }

    private void Update()
    {
        if (audioSource == null)
            return;

        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime / easeSpeed);

    }
}

