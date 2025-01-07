using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSpeaker : MonoBehaviour
{
    /* this is a stripped down version of the DialogueSpeaker script
     * It maintains the interface so you just have to configure it.
     */

    public string localizationKey = ""; // if empty, use gameObject name
    public string localizationTable = "";
    public string triggerType = "proximity"; // |script|
    public bool resettable = false;
    public float resetTime = 1f;
    public bool deactivated = false;
    public bool inProximity = false;
    public bool scriptTriggered = false;
    public bool saveDialogueDeletion = false;

    public int dialogueIndex = 0;
    public string[] dialogue;
    public AudioClip[] speechAudio;
    public AudioSource aS;

    public GameObject nextDialogueObject;

    public string state = "quiet"; //|speaking|speaking done|complete"

    public ActivateDeactivateService activateDeactivateBeforeDialogue;
    public ActivateDeactivateService activateDeactivateAfterDialogue;

    public GameObject textBox;
    public TextMeshProUGUI dialogueBox;
    public TextMeshProEffect tmpe;
    public GameObject climbingCharacter;
    //public BaseFirstPersonController characterController;
    //public WeaponBobbingV2.BobStyle1 bobEffect;
    public bool climbingEnabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        localizationTable = SceneManager.GetActiveScene().name + "_dialogue";

        if (localizationKey == "")
            localizationKey = gameObject.name;

        climbingCharacter = GameObject.Find("Climber");

        if (climbingCharacter)
            characterController = climbingCharacter.GetComponent<BaseFirstPersonController>();

        if (GameObject.Find("Bobbing"))
            bobEffect = GameObject.Find("Bobbing").GetComponent<WeaponBobbingV2.BobStyle1>();

        textBox.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Check for interrupts
        if (PauseHelper.currentlyPaused || PauseHelper.cutscenePause || deactivated || Scanner.scanObjectActive)
            return;

        if (state == "quiet")
        {
            if (Input.GetButtonDown("Interact") && triggerType == "proximity" && inProximity)
            {
                PauseHelper.narrationPause = true;
                PausePlayer();

                state = "speaking";
                dialogueIndex = 0;
                textBox.SetActive(true);

                if (activateDeactivateBeforeDialogue)
                    activateDeactivateBeforeDialogue.ActivateDeactivate();
            }

            if (triggerType == "script" && scriptTriggered)
            {
                PauseHelper.narrationPause = true;
                PausePlayer();

                scriptTriggered = false;
                state = "speaking";
                dialogueIndex = 0;
                textBox.SetActive(true);

                if (activateDeactivateBeforeDialogue)
                    activateDeactivateBeforeDialogue.ActivateDeactivate();
            }
        }

        if (state == "speaking")
        {
            // Display current dialogue at index
            // Wait for speaking to finish to go to next dialogue (Wait to prompt player)
            if (dialogueIndex < dialogue.Length)
            {
                // Display the localized string only
                string currentKey = localizationKey + "_" + dialogueIndex.ToString();

                string result = LocalizationSettings.StringDatabase.GetLocalizedString(localizationTable, currentKey);
                StringTable stringTable = LocalizationSettings.StringDatabase.GetTable(localizationTable);

                if (stringTable.GetEntry(currentKey) != null)
                    dialogueBox.text = LocalizationSettings.StringDatabase.GetLocalizedString(localizationTable, currentKey);
                else
                    dialogueBox.text = dialogue[dialogueIndex];

                if (aS && dialogueIndex < speechAudio.Length)
                    aS.PlayOneShot(speechAudio[dialogueIndex]);

                tmpe.Play();
            }

            StartCoroutine(WaitToPromptPlayer());

            state = "already spoke";
        }

        if (state == "already spoke")
        {
            // do nothing, waiting for speaking done
        }

        if (state == "speaking done")
        {
            // Prompt Player
            if (Input.GetButtonDown("Interact"))
            {
                state = "speaking";
                dialogueIndex++;

                if (dialogueIndex == dialogue.Length)
                    state = "complete";
            }
        }

        if (state == "complete")
        {
            if (resettable)
            {
                if (resetTime != 0)
                {
                    StartCoroutine(WaitToReset());
                }
                else
                {
                    dialogueIndex = 0;
                    state = "quiet";
                }
            }

            // Unpause the player
            UnpausePlayer();
            
            PauseHelper.narrationPause = false;
            textBox.SetActive(false);
            deactivated = true;

            if (saveDialogueDeletion)
                Save.StoreDeactivation(gameObject.name);

            if (activateDeactivateAfterDialogue)
                activateDeactivateAfterDialogue.ActivateDeactivate();
        }
        */
    }

    private IEnumerator WaitToPromptPlayer()
    {
        //yield return new WaitForSeconds(tmpe.DurationInSeconds);
        yield return new WaitForSeconds(1);

        state = "speaking done";
    }

    private IEnumerator WaitToReset()
    {
        yield return new WaitForSeconds(resetTime);

        dialogueIndex = 0;
        state = "quiet";
        deactivated = false;
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Climber")
            inProximity = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Climber")
            inProximity = false;
    }

    private void PausePlayer()
    {
        /*
        if (climbingCharacter.GetComponent<ClimbingAbilityV2>().onWall != true)
        {
            characterController.pause = true;
            characterController.Pause();
        }

        bobEffect.enabled = false;

        climbingCharacter.GetComponent<CharacterSoundEventManager>().audioSource.volume = 0;

        if (climbingCharacter.GetComponent<ClimbingAbilityV2>().abilityActive == true)
        {
            climbingEnabled = true;

            if (climbingCharacter.GetComponent<ClimbingAbilityV2>().onWall)
            {
                climbingCharacter.GetComponent<ClimbingAbilityV2>().CrossFadeIfNotAlreadyPlaying("ClingOnWall");
                climbingCharacter.GetComponent<ClimbingAbilityV2>().pause = true;
                characterController.Pause();
                climbingCharacter.GetComponent<ClimbingAbilityV2>().ss.PauseStamina();
            }

            climbingCharacter.GetComponent<ClimbingAbilityV2>().abilityActive = false;
        }
        */
    }

    private void UnpausePlayer()
    {
        /*
        PauseHelper.narrationPause = false;

        bobEffect.enabled = true;

        climbingCharacter.GetComponent<CharacterSoundEventManager>().audioSource.volume = 1;

        if (climbingCharacter.GetComponent<ClimbingAbilityV2>().onWall != true)
        {
            characterController.pause = false;
            characterController.Pause();
        }

        if (climbingEnabled)
        {
            climbingCharacter.GetComponent<ClimbingAbilityV2>().abilityActive = true;
            climbingCharacter.GetComponent<ClimbingAbilityV2>().pause = false;
            characterController.Pause();
        }
        */
    }
}
