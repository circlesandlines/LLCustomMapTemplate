using UnityEngine;
using UnityEngine.Audio;

public class SelectAudioMixerGroup : MonoBehaviour
{
    public enum MixerGroupOption
    {
        MasterMusic,
        MasterSFX
    }

    [Header("Audio Mixer Settings")]
    public MixerGroupOption mixerGroup; // Dropdown for selecting the group

    [Header("Audio Source Settings")]
    public AudioSource targetAudioSource; // Optional Audio Source to set

    private void Awake()
    {
        // Attempt to load the Audio Mixer from the Resources folder
        AudioMixer audioMixer = Resources.Load<AudioMixer>("MasterMixer");

        if (audioMixer == null)
        {
            Debug.LogError("No Audio Mixer found in Resources. Please ensure 'MasterMixer.mixer' is placed in a Resources folder.", this);
            return;
        }

        // Determine which group to search for
        string groupName = mixerGroup == MixerGroupOption.MasterMusic ? "Master/Music" : "Master/SFX";

        // Search for the Audio Mixer Group
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(groupName);
        if (groups.Length == 0)
        {
            Debug.LogError($"Audio Mixer Group '{groupName}' not found in the specified Audio Mixer.", this);
            return;
        }

        // Get the target AudioSource
        AudioSource audioSource = targetAudioSource != null ? targetAudioSource : GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No Audio Source found. Please specify one or attach this component to a GameObject with an Audio Source.", this);
            return;
        }

        // Assign the Audio Mixer Group
        audioSource.outputAudioMixerGroup = groups[0];
        Debug.Log($"Assigned '{groupName}' group to the Audio Source on {gameObject.name}.");
    }
}
