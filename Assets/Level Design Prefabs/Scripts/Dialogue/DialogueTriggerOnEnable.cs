using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerOnEnable : MonoBehaviour
{
    public DialogueSpeaker dialogueToTrigger;

    // Start is called before the first frame update
    void Start()
    {
        dialogueToTrigger.scriptTriggered = true;
    }
}
