using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereDefinition : MonoBehaviour
{
    public string atmosphereName;
    public Material newSkybox;
    public Color fogTo;
    public float fogDensityTo;
    public bool changeFogDensity = false;
    public Color ambientTopTo;
    public Color ambientMidTo;
    public Color ambientBottomTo;
    public float chestLightIntensityTo;
    public float flareLightIntensityTo;

    public bool changeChestLightRange = false;
    public float chestLightRangeTo;

    public bool changeChestLightColor = false;
    public Color chestLightColorTo;

    public bool changeFlareLightColor = false;
    public Color flareLightColorTo;

    private GameObject climber;

    public bool save = true;

    private void Start()
    {
        climber = GameObject.Find("Climber");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Climber")
        {
            other.GetComponent<AtmosphereThresholdTransitioner>().ChangeAtmosphere(this);

            if (save)
            {
                // Store the game object name so you only need to do a lookup when loading vs store a reference table
                // to all atmosphere definition objects.

                // NOTE: the following is not necessary in the asset bundle.
                // According to docs, Once the asset bundle is loaded, it won't generate duplicates, and hence
                // it will use the main scene's Save reference.
                //Save.StoreAtmosphere(gameObject.name);
            }
        }
    }

    public void SwitchToAtmosphere()
    {
        GameObject.Find("Climber").GetComponent<AtmosphereThresholdTransitioner>().ChangeAtmosphereInstantly(this);
    }
}
