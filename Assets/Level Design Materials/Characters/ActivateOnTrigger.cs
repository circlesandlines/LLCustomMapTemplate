using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;

    public bool hasTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject.name == "Climber" && !hasTriggered)
        {
            hasTriggered = true;
            //Debug.Log("Triggered object: " + name);

            if (activate)
            {
                Save.StoreActivation(activate.name);

                //Debug.Log("Stored Activation: " + name);

                if (PersistentSaveObject.deletedObjects.Contains(activate.name))
                    PersistentSaveObject.deletedObjects.Remove(activate.name);
            }

            if (deactivate)
            {
                Save.StoreDeactivation(deactivate.name);
                //Debug.Log("Stored Deactivation: " + name);

                if (PersistentSaveObject.activatedObjects.Contains(deactivate.name))
                    PersistentSaveObject.activatedObjects.Remove(deactivate.name);
            }

            if (activate)
            {
                activate.SetActive(true);
                //Debug.Log("activate: " + activate.name);
            }

            if (deactivate)
            {
                deactivate.SetActive(false);
                //Debug.Log("deactivate: " + deactivate.name);
            }
        }
        */
    }
}
