using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
//using ECM.Components;
//using ECM.Controllers;

public class InitCustomMap : MonoBehaviour
{
    public GameObject blueCrystalPrefab; // Assign the Blue Crystal prefab in the inspector
    public GameObject climber;
    public GameObject grappleAttachmentRunning;

    private void Start()
    {
        // Delay until Asset Bundle map is loaded additively
        StartCoroutine(Initialize());
    }

    public void InitializeClimber(string configObjectName = "")
    {
        /*
        ClimberConfig cf = GameObject.Find("Climber Config").GetComponent<ClimberConfig>();

        BaseFirstPersonController bfpsc = climber.GetComponent<BaseFirstPersonController>();

        bfpsc.forwardSpeed = cf.forwardSpeed;
        bfpsc.backwardSpeed = cf.backwardSpeed;
        bfpsc.strafeSpeed = cf.strafeSpeed;
        bfpsc.runSpeedMultiplier = cf.runSpeedMultiplier;
        bfpsc.baseJumpHeight = cf.jumpHeight;
        bfpsc.extraJumpTime = cf.extraJumpTime;
        bfpsc.extraJumpPower = cf.extraJumpPower;
        bfpsc.maxMidAirJumps = cf.maxMidAirJumps;
        bfpsc.acceleration = cf.acceleration;
        bfpsc.deceleration = cf.deceleration;
        bfpsc.airControl = cf.airControl;

        CharacterMovement cm = bfpsc.movement;

        cm.maxLateralSpeed = cf.maxLateralSpeed;
        cm.maxRiseSpeed = cf.maxRiseSpeed;
        cm.maxFallSpeed = cf.maxFallSpeed;
        cm.slopeLimit = cf.slopeLimit;
        cm.slideGravityMultiplier = cf.slideGravityMultiplier;

        ClimbingAbilityV2 cav2 = climber.GetComponent<ClimbingAbilityV2>();

        cav2.abilityActive = cf.climbingAbilityActive;
        cav2.reach = cf.armReach;
        cav2.climbingSpeed = cf.climbingSpeed;
        cav2.jumpOffWallVerticalImpulse = cf.jumpOffWallVerticalImpulse;
        cav2.jumpOffWallHorizontalImpulse = cf.jumpOffWallHorizontalImpulse;

        // if ability active, enable picks
        // Might do it automatically actually. test it...

        EasyJumping ej = climber.GetComponent<EasyJumping>();

        ej.easyModeActive = cf.coyoteJumpActive;
        ej.maxAllowedtime = cf.maxAllowedTime;
        ej.jumpForce = cf.coyoteJumpForce;
        ej.defaultNumJumps = cf.defaultInAirJumps;

        StaminaSystem ss = climber.GetComponent<StaminaSystem>();

        if (!cf.staminaActive)
            ss.stopAllStamina = true;
        else
            ss.stopAllStamina = false;

        ss.decayFactor = cf.decayFactor;
        ss.recoveryFactor = cf.recoveryFactor;

        FallDeath fd = climber.GetComponent<FallDeath>();

        if (!cf.terminalVelocityActive)
            fd.criticalVelocity = -20000f;

        TicTac2 tt2 = climber.GetComponent<TicTac2>();

        if (cf.ticTacActive)
            tt2.enabled = true;

        tt2.jumpForceWall = cf.jumpForceWall;
        tt2.jumpForceForward = cf.jumpForceForward;
        tt2.jumpForceNormal = cf.jumpForceNormal;
        tt2.jumpForceUp = cf.jumpForceUp;
        tt2.legLength = cf.legLength;
        tt2.allowableHeightFromGround = cf.allowableHeightFromGround;

        Dash d = climber.GetComponent<Dash>();

        d.abilityActive = cf.dashActive;
        d.dashStaminaDepletionAmount = cf.staminaDepletion;
        d.airControlWhileDash = cf.dashAirControl;
        d.dashForce = cf.dashForce;
        d.weakDashForce = cf.weakDashForce;
        d.dashResetTimeSeconds = cf.resetTime;

        GrappleHook g = climber.GetComponent<GrappleHook>();

        g.abilityActive = cf.grappleActive;
        g.maxDistance = cf.maxGrappleDistance;
        g.maxLateralSpeed = cf.maxLateralSpeed;

        // if ability active, activate grapple models
        if (g.abilityActive)
        {
            g.enabled = true;
            grappleAttachmentRunning.SetActive(true);
        }
        */
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.5f);

        /*
        // Ensure the Asset Bundle is already loaded
        string modName = PersistentSaveObject.modName;

        if (string.IsNullOrEmpty(modName))
        {
            Debug.LogError("Mod name not set in PersistentSaveObject.");
            yield break;
        }

        // Find all checkpoints and add the CheckpointSet component
        Transform checkpointsParent = GameObject.Find("/Systems/Checkpoints").transform;
        
        if (checkpointsParent != null)
        {
            foreach (Transform checkpoint in checkpointsParent)
            {
                Debug.Log("checkpoint: " + checkpoint.name);

                CheckpointSet checkpointSet = checkpoint.gameObject.AddComponent<CheckpointSet>();
                checkpointSet.invisible = true;
            }
        }
        else
        {
            Debug.LogWarning("Checkpoints parent not found.");
            yield break;
        }

        // Find all death areas and add tags
        /*
        Transform deathAreas = GameObject.Find("/Systems/Death Areas").transform;

        if (deathAreas != null)
        {
            foreach (Transform deathArea in deathAreas)
            {
                Debug.Log("death area: " + deathArea.name);

                if (deathArea.name.Contains("Die"))
                    deathArea.gameObject.tag = "Die";

                if (deathArea.name.Contains("DieWater"))
                    deathArea.gameObject.tag = "DieWater";

                if (deathArea.name.Contains("DieSoundless"))
                    deathArea.gameObject.tag = "DieSoundless";
            }
        }
        else
        {
            Debug.LogWarning("No death areas found");
            yield break;
        }
        

        // Find all Blue Crystals and replace them with prefabs
        Transform blueCrystalsParent = GameObject.Find("/Systems/Blue Crystals").transform;

        if (blueCrystalsParent != null)
        {
            int i = 0;
            foreach (Transform crystal in blueCrystalsParent)
            {
                Debug.Log("crystal: " + crystal.name);

                // Instantiate the Blue Crystal prefab at the same position and rotation
                GameObject bc = Instantiate(blueCrystalPrefab, crystal.position, crystal.rotation);
                bc.name = "Blue Crystal (" + i + ")";
                crystal.gameObject.SetActive(false); // Remove the placeholder crystal
                i++;
            }
        }
        else
        {
            Debug.LogWarning("Blue Crystals parent not found.");
            yield break;
        }


        // Move the FirstPositionCheckpoint to the ClimberStart position and rotation
        // Do the same with the actual Climber object
        GameObject firstPositionCheckpoint = GameObject.Find("/Level Mechanics/Checkpoints/FirstPositionCheckpoint");
        Transform climberStart = GameObject.Find("/Systems/ClimberStart").transform;
        if (firstPositionCheckpoint != null && climberStart != null)
        {
            firstPositionCheckpoint.transform.position = climberStart.position;
            firstPositionCheckpoint.transform.rotation = climberStart.rotation;

            climber.transform.position = climberStart.position;
            climber.transform.rotation = climberStart.rotation;
        }

        // Move EndingTrigger and EndCube to the Ending position
        GameObject endingTrigger = GameObject.Find("/Level Mechanics/EndingTrigger");
        GameObject endCube = GameObject.Find("/Level Mechanics/EndCube");
        Transform ending = GameObject.Find("/Systems/Ending").transform;
        if (endingTrigger != null && endCube != null && ending != null)
        {
            endingTrigger.transform.position = ending.position;
            endingTrigger.transform.rotation = ending.rotation;

            endCube.transform.position = ending.position;
            endCube.transform.rotation = ending.rotation;

            // Copy Box Collider values
            BoxCollider endingCollider = ending.GetComponent<BoxCollider>();
            if (endingCollider != null)
            {
                CopyBoxColliderValues(endingCollider, endingTrigger.AddComponent<BoxCollider>());
                CopyBoxColliderValues(endingCollider, endCube.AddComponent<BoxCollider>());
            }
        }
        else
        {
            Debug.LogWarning("Ending or associated objects not found.");
            yield break;
        }

        InitializeClimber();
        */
    }

    private Transform FindChildByPath(string path)
    {
        /*
        string[] parts = path.Split('/');

        // Iterate over all loaded scenes
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);

            // Check if the scene is loaded
            if (scene.isLoaded)
            {
                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    // Start at the root object if it matches the first part
                    if (rootObject.name == parts[0])
                    {
                        Transform current = rootObject.transform;

                        // Traverse the rest of the path
                        for (int j = 1; j < parts.Length; j++)
                        {
                            current = current.Find(parts[j]);
                            if (current == null)
                                break; // Stop if a part is not found
                        }

                        // Return if the entire path is matched
                        if (current != null)
                            return current;
                    }
                }
            }
        }

        */
        return null; // Return null if no match is found
    }


    private void CopyBoxColliderValues(BoxCollider source, BoxCollider target)
    {
        if (source == null || target == null) return;
        target.center = source.center;
        target.size = source.size;
        target.isTrigger = source.isTrigger;
    }
}
