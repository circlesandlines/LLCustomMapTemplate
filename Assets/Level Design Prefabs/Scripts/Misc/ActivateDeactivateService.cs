using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateDeactivateService : MonoBehaviour
{
    public GameObject[] deactivate;
    public GameObject[] activate;
    public AudioSource soundEffect;
    public bool deleteInsteadofDeactivate = false;
    public bool save = true;

    public float delay = 0;

    private void Start()
    {
        if (GameObject.Find("Global Scene Objects"))
            save = false;
    }

    public void ActivateDeactivate()
    {
        if (delay > 0)
        {
            StartCoroutine(DelayedActivation());
        }
        else
        {
            if (soundEffect)
                soundEffect.Play();

            foreach (GameObject obj in activate)
            {
                // Sometimes they're null, just skip
                if (!obj)
                    continue;

                /*
                if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                    if (save)
                        Save.StoreActivation(obj.name);*/

                obj.SetActive(true);
            }

            foreach (GameObject obj in deactivate)
            {
                if (!obj)
                    continue;

                if (deleteInsteadofDeactivate)
                {
                    /*
                    if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                        if (save)
                            Save.StoreDeactivation(obj.name);*/

                    Destroy(obj);
                }
                else
                {
                    /*
                    if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                        if (save)
                            Save.StoreDeactivation(obj.name);*/

                    obj.SetActive(false);
                }
            }
        }
    }

    private IEnumerator DelayedActivation()
    {
        yield return new WaitForSeconds(delay);

        if (soundEffect)
            soundEffect.Play();

        foreach (GameObject obj in activate)
        {
            if (!obj)
                continue;

            /*
            if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                if (save)
                    Save.StoreActivation(obj.name);*/

            obj.SetActive(true);
        }

        foreach (GameObject obj in deactivate)
        {
            if (!obj)
                continue;

            if (deleteInsteadofDeactivate)
            {
                /*
                if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                    if (save)
                        Save.StoreDeactivation(obj.name);*/

                Destroy(obj);
            }
            else
            {
                /*
                if (SceneManager.GetActiveScene().name != "IntroCutscene 1")
                    if (save)
                        Save.StoreDeactivation(obj.name);*/

                obj.SetActive(false);
            }
        }
    }
}
