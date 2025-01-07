using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereThresholdTransitioner : MonoBehaviour
{
    /*
     * player
	        atmosphere transitioner (AT)
		        defines initial atmosphere settings
		        stores transition %age
		        stores current atmosphere
		

        volume 1
	        atmosphere setting (AS)
		        defines its own atmosphere

        volume 2
	        atmosphere setting (AS)
		        defines its own atmosphere


        AT detects V1
        AT takes current atmosphere at whatever the stats are (including in transition)
        Starts a new transition (resets the %age)


        If pausing inside volume, need to handle re-enter trigger
	        if new volume has same atmosphere as current one, don't change anything
     *
     */


    public string currentAtmosphere = "default";

    public Material currentSkybox;
    public Material newSkybox;

    public Color fogFrom;
    public Color fogTo;

    public bool changeFogDensity = false;
    public float fogDensityFrom;
    public float fogDensityTo;

    public Color ambientTopFrom;
    public Color ambientTopTo;

    public Color ambientMidFrom;
    public Color ambientMidTo;

    public Color ambientBottomFrom;
    public Color ambientBottomTo;

    public Light chestLight;
    public float chestLightRangeFrom;
    public float chestLightRangeTo;
    public float chestLightIntensityFrom;
    public float chestLightIntensityTo;
    public bool changeChestLightColor = false;
    public Color chestLightColorTo;
    public Color chestLightColorFrom;

    public Light flareLight;
    public bool changeChestLightRange = false;
    public float flareLightIntensityFrom;
    public float flareLightIntensityTo;
    public bool changeFlareLightColor = false;
    public Color flareLightColorTo;
    public Color flareLightColorFrom;

    public float transitionTime = 1f;

    private float _startTime;

    // New method where you increment these
    public float timeElapsedPercent = 0; // 0 - 1 based on %age of color
    public float deltaUpdate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        fogFrom = RenderSettings.fogColor;
        fogDensityFrom = RenderSettings.fogDensity;
        ambientTopFrom = RenderSettings.ambientSkyColor;
        ambientMidFrom = RenderSettings.ambientEquatorColor;
        ambientBottomFrom = RenderSettings.ambientGroundColor;

        if (changeChestLightRange)
            chestLightRangeFrom = chestLight.range;

        chestLightIntensityFrom = chestLight.intensity;
        flareLightIntensityFrom = flareLight.intensity;
        chestLightColorFrom = chestLight.color;
        flareLightColorFrom = flareLight.color;

        // Start at 1 since default should be active and completely transitioned
        timeElapsedPercent = 1;

        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // ease into the new atmosphere
        if (timeElapsedPercent < 1f)
        {
            timeElapsedPercent += deltaUpdate * Time.deltaTime;

            RenderSettings.fogColor = Color.Lerp(fogFrom, fogTo, timeElapsedPercent);
            RenderSettings.ambientSkyColor = Color.Lerp(ambientTopFrom, ambientTopTo, timeElapsedPercent);
            RenderSettings.ambientEquatorColor = Color.Lerp(ambientMidFrom, ambientMidTo, timeElapsedPercent);
            RenderSettings.ambientGroundColor = Color.Lerp(ambientBottomFrom, ambientBottomTo, timeElapsedPercent);

            chestLight.intensity = Mathf.Lerp(chestLightIntensityFrom, chestLightIntensityTo, timeElapsedPercent);
            flareLight.intensity = Mathf.Lerp(flareLightIntensityFrom, flareLightIntensityTo, timeElapsedPercent);

            if (changeChestLightRange)
                chestLight.range = Mathf.Lerp(chestLightRangeFrom, chestLightRangeTo, timeElapsedPercent);

            if (changeChestLightColor)
                chestLight.color = Color.Lerp(chestLightColorFrom, chestLightColorTo, timeElapsedPercent);

            if (changeFlareLightColor)
                flareLight.color = Color.Lerp(flareLightColorFrom, flareLightColorTo, timeElapsedPercent);

            if (changeFogDensity)
                RenderSettings.fogDensity = Mathf.Lerp(fogDensityFrom, fogDensityTo, timeElapsedPercent);
        }
    }

    public void ChangeAtmosphereInstantly(AtmosphereDefinition other)
    {
        if (other.newSkybox)
            RenderSettings.skybox = other.newSkybox;

        Debug.Log("Changing atmosphere at start instantly called");

        // just started the level, change instantly
        RenderSettings.fogColor = other.fogTo;
        RenderSettings.ambientSkyColor = other.ambientTopTo;
        RenderSettings.ambientEquatorColor = other.ambientMidTo;
        RenderSettings.ambientGroundColor = other.ambientBottomTo;
        
        chestLight.intensity = other.chestLightIntensityTo;
        flareLight.intensity = other.flareLightIntensityTo;
        changeChestLightColor = other.changeChestLightColor;
        changeFlareLightColor = other.changeFlareLightColor;
        changeFogDensity = other.changeFogDensity;

        if (changeChestLightRange)
            chestLight.range = other.chestLightRangeTo;

        if (changeChestLightColor)
            chestLight.color = other.chestLightColorTo;

        if (changeFlareLightColor)
            flareLight.color = other.flareLightColorTo;

        if (changeFogDensity)
            RenderSettings.fogDensity = other.fogDensityTo;

        currentAtmosphere = other.atmosphereName;

        timeElapsedPercent = 1f;

        // Store the Froms
        fogFrom = RenderSettings.fogColor;
        ambientTopFrom = RenderSettings.ambientSkyColor;
        ambientMidFrom = RenderSettings.ambientEquatorColor;
        ambientBottomFrom = RenderSettings.ambientGroundColor;

        chestLightRangeFrom = chestLight.range;
        chestLightIntensityFrom = chestLight.intensity;
        flareLightIntensityFrom = flareLight.intensity;

        chestLightColorFrom = chestLight.color;
        flareLightColorFrom = flareLight.color;
    }

    public void ChangeAtmosphere(AtmosphereDefinition other)
    {
        Debug.Log("ChangeAtmosphere. if this doesn't show, then issue calling it: " + other.atmosphereName);

        if (other.newSkybox)
            RenderSettings.skybox = other.newSkybox;

        Debug.Log("why is the start time not <0.1? " + (Time.time - _startTime));

        if (Time.time - _startTime < 0.1f)
        {
            Debug.Log("Changing atmosphere at start instantly");

            // just started the level, change instantly
            RenderSettings.fogColor = other.fogTo;
            RenderSettings.ambientSkyColor = other.ambientTopTo;
            RenderSettings.ambientEquatorColor = other.ambientMidTo;
            RenderSettings.ambientGroundColor = other.ambientBottomTo;
            chestLight.intensity = other.chestLightIntensityTo;
            flareLight.intensity = other.flareLightIntensityTo;
            changeChestLightColor = other.changeChestLightColor;
            changeFlareLightColor = other.changeFlareLightColor;
            changeFogDensity = other.changeFogDensity;

            if (changeChestLightRange)
                chestLight.range = other.chestLightRangeTo;

            if (changeChestLightColor)
                chestLight.color = other.chestLightColorTo;

            if (changeFlareLightColor)
                flareLight.color = other.flareLightColorTo;

            if (changeFogDensity)
                RenderSettings.fogDensity = other.fogDensityTo;

            currentAtmosphere = other.atmosphereName;

            timeElapsedPercent = 1f;

            // Store the Froms
            fogFrom = RenderSettings.fogColor;
            ambientTopFrom = RenderSettings.ambientSkyColor;
            ambientMidFrom = RenderSettings.ambientEquatorColor;
            ambientBottomFrom = RenderSettings.ambientGroundColor;

            chestLightRangeFrom = chestLight.range;
            chestLightIntensityFrom = chestLight.intensity;
            flareLightIntensityFrom = flareLight.intensity;

            chestLightColorFrom = chestLight.color;
            flareLightColorFrom = flareLight.color;
        }
        else
        {
            // Walked in, or reset - perhaps a different method for reset?
            Debug.Log("Enter. Current atmosphere: " + currentAtmosphere);

            // do nothing if already in same atmosphere (ignore re-collision)
            if (currentAtmosphere == other.atmosphereName)
                return;

            Debug.Log("are we getting here???");

            currentAtmosphere = other.atmosphereName;

            // from current color, no matter point of transition
            fogFrom = RenderSettings.fogColor;
            fogDensityFrom = RenderSettings.fogDensity;
            ambientTopFrom = RenderSettings.ambientSkyColor;
            ambientMidFrom = RenderSettings.ambientEquatorColor;
            ambientBottomFrom = RenderSettings.ambientGroundColor;

            chestLightIntensityFrom = chestLight.intensity;
            flareLightIntensityFrom = flareLight.intensity;

            chestLightColorFrom = chestLight.color;
            flareLightColorFrom = flareLight.color;

            // to
            fogTo = other.fogTo;
            fogDensityTo = other.fogDensityTo;
            ambientTopTo = other.ambientTopTo;
            ambientMidTo = other.ambientMidTo;
            ambientBottomTo = other.ambientBottomTo;

            if (changeChestLightRange)
                chestLight.range = other.chestLightRangeTo;

            chestLightIntensityTo = other.chestLightIntensityTo;
            flareLightIntensityTo = other.flareLightIntensityTo;

            changeChestLightColor = other.changeChestLightColor;
            changeFlareLightColor = other.changeFlareLightColor;
            changeFogDensity = other.changeFogDensity;

            chestLightColorTo = other.chestLightColorTo;
            flareLightColorTo = other.flareLightColorTo;

            timeElapsedPercent = 0;
        }
    }
}
