using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDeactivateObjects : MonoBehaviour
{
	public string playerObjectName;
    public string objectTag;
    public bool saveCheckpoint = false;
    public GameObject player;
    public GameObject[] deactivate;
    public GameObject[] activate;
    public AudioSource soundEffect;
	public enum InteractBy {Interact, Collide, CollideType, Enable};
    public InteractBy interactMode = InteractBy.Interact;
    private bool mode = true;
    public bool reactivateable = false;
    public float reactivateTime = 0;
    public bool reactivateOnExit = false;
    public bool save = true;
    public bool dontAllowWhenScan = true;

    public bool checkpointConditionsApply = false;

    public bool near = false;
    public bool activated = false;

    private void Start()
    {
        player = GameObject.Find(playerObjectName);

        if (GameObject.Find("Global Scene Objects"))
            save = false;

        if (interactMode == InteractBy.Enable)
        {
            ActivateDeactivate();
        }
    }

    void Update()
    {
        /* Don't actually need implementation
        if (ScannableObject.terminalOpen && dontAllowWhenScan)
            return;

        if (interactMode == InteractBy.Interact)
        {
            if (near && !activated && Input.GetButtonDown("Interact"))
                ActivateDeactivate();
        }
        else if (interactMode == InteractBy.Collide)
        {
            if (near && !activated)
            {
                if (checkpointConditionsApply)
                {
                    if (!player.gameObject.GetComponent<FallDeath>().dead &&
                        !player.gameObject.GetComponent<FallDeath>().criticalVelocityReached)
                    {
                        ActivateDeactivate();
                    }
                }
                else
                {
                    ActivateDeactivate();
                }
            }
        }
        else if (interactMode == InteractBy.CollideType)
        {
            if (near && !activated)
                ActivateDeactivate();
        }
        */
    }
	
	public void ActivateDeactivate()
	{
        /*
        if (soundEffect)
        {
            soundEffect.Play();

            // Wait for sound to stop playing before destroying
            // the trigger object.
            StartCoroutine(CheckSoundPlaying());
        }

        // If checkpoint object exists, set that here to avoid variance in logic.
        if (saveCheckpoint && GetComponent<CheckpointSet>())
        {
            GetComponent<CheckpointSet>().cps.SetNewCheckpoint(transform, player.transform, true);
            GetComponent<CheckpointSet>().alreadyPlayed = true;

            Save.SaveAllAtCheckpoint(name);
            Debug.Log("Manually Setting Checkpoint: " + name);
        }

        foreach (GameObject obj in activate)
        {
            if (obj == null)
                continue;

            // If deactivation already there, you should remove it from save
            // This implies you deleted it prior to reactivating it here
            if (PersistentSaveObject.deletedObjects != null && PersistentSaveObject.deletedObjects.Contains(obj.name))
                PersistentSaveObject.deletedObjects.Remove(obj.name);

            if (save)
                Save.StoreActivation(obj.name);

            obj.SetActive(true);
            Debug.Log("Activate: " + obj.name + ", !obj? " + obj);
        }

        foreach (GameObject obj in deactivate)
        {
            if (obj == null)
                continue;

            // If activation already there, you should remove it from save
            // This implies you activated it prior to deactivating it here
            if (PersistentSaveObject.activatedObjects != null && PersistentSaveObject.activatedObjects.Contains(obj.name))
                PersistentSaveObject.activatedObjects.Remove(obj.name);

            if (save)
                Save.StoreDeactivation(obj.name);

            obj.SetActive(false);
            Debug.Log("Activate: " + obj.name + ", !obj? " + obj);
        }

        if (!reactivateable)
        {
            activated = true;
        }
        else
        {
            if (reactivateTime == 0)
            {
                activated = false;
            }
            else
            {
                activated = true;
                StartCoroutine(ReactivateTrigger());
            }
        }
        */
	}

    private IEnumerator ReactivateTrigger()
    {
        yield return new WaitForSeconds(reactivateTime);
        activated = false;
    }

    void OnTriggerEnter(Collider o)
    {
        if (interactMode == InteractBy.CollideType)
        {
            if (o.tag == objectTag)
            {
                near = true;
            }
        }
        else if (o.name == playerObjectName)
        {
            near = true;
        }
    }

    void OnTriggerExit(Collider o)
    {
        if (interactMode == InteractBy.CollideType)
        {
            if (o.tag == objectTag)
            {
                near = false;
            }

            if (reactivateOnExit)
                activated = false;
        }
        else if (o.name == playerObjectName)
        {
            near = false;

            if (reactivateOnExit)
                activated = false;

            // If no audio supplied, just delete it right away
            //if (!soundEffect && activated)
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    private IEnumerator CheckSoundPlaying()
	{
        yield return new WaitForSeconds(soundEffect.clip.length);

		//Destroy(gameObject);
	}
}
