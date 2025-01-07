using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ClimberConfig : MonoBehaviour
{
    [Header("Trigger")]
    public bool triggerPlayerChange = false;
    public string configObjectName = "";

    [Header("Base FPS Controller")]
    public float forwardSpeed = 10f;
    public float backwardSpeed = 10f;
    public float strafeSpeed = 10f;
    public float runSpeedMultiplier = 0.3f;
    public float jumpHeight = 1.7f;
    public float extraJumpTime = 0.5f;
    public float extraJumpPower = 0f;
    public float maxMidAirJumps = 0;
    public float acceleration = 50f;
    public float deceleration = 20f;
    public float airControl = 0.625f;

    [Header("Character Movement")]
    public float maxLateralSpeed = 10f;
    public float maxRiseSpeed = 20f;
    public float maxFallSpeed = 40f;
    public float slopeLimit = 45f;
    public float slideGravityMultiplier = 1f;

    [Header("Abilities - Climbing")]
    public bool climbingAbilityActive = false;
    public float armReach = 0.8f;
    public float climbingSpeed = 3.5f;
    public float jumpOffWallVerticalImpulse = 100f;
    public float jumpOffWallHorizontalImpulse = 300f;

    [Header("Abilities - Coyote Jump")]
    public bool coyoteJumpActive = true;
    public float maxAllowedTime = 0.15f;
    public float coyoteJumpForce = 500f;
    public float defaultInAirJumps = 0;

    [Header("Abilities - Stamina")]
    public bool staminaActive = true;
    public float decayFactor = 5f;
    public float recoveryFactor = 120f;

    [Header("Abilities - Terminal Velocity")]
    public bool terminalVelocityActive = true;

    [Header("Abilities - Tic Tac")]
    public bool ticTacActive = false;
    public float jumpForceWall = 800f;
    public float jumpForceForward = 150f;
    public float jumpForceNormal = 300f;
    public float jumpForceUp = 0f;
    public float legLength = 2f;
    public float allowableHeightFromGround = 1f;

    [Header("Abilities - Dash")]
    public bool dashActive = false;
    public float staminaDepletion = 30f;
    public float dashAirControl = 0.5f;
    public float dashForce = 2000f;
    public float weakDashForce = 1000f;
    public float resetTime = 0.5f;

    [Header("Abilities - Grapple")]
    public bool grappleActive = false;
    public float maxGrappleDistance = 30f;

    private void Start()
    {
        if (triggerPlayerChange)
        {
            GameObject.Find("ModLoader").GetComponent<InitCustomMap>().InitializeClimber(configObjectName);
        }
    }

    /// <summary>
    /// Resets all configuration values to their defaults.
    /// </summary>
    public void ResetToDefaults()
    {
        // Base FPS Controller
        forwardSpeed = 10f;
        backwardSpeed = 10f;
        strafeSpeed = 10f;
        runSpeedMultiplier = 0.3f;
        jumpHeight = 1.7f;
        extraJumpTime = 0.5f;
        extraJumpPower = 0f;
        maxMidAirJumps = 0f;
        acceleration = 50f;
        deceleration = 20f;
        airControl = 0.625f;

        // Character Movement
        maxLateralSpeed = 10f;
        maxRiseSpeed = 20f;
        maxFallSpeed = 40f;
        slopeLimit = 45f;
        slideGravityMultiplier = 1f;

        // Abilities - Climbing
        climbingAbilityActive = false;
        armReach = 0.8f;
        climbingSpeed = 3.5f;
        jumpOffWallVerticalImpulse = 100f;
        jumpOffWallHorizontalImpulse = 300f;

        // Abilities - Coyote Jump
        coyoteJumpActive = true;
        maxAllowedTime = 0.15f;
        coyoteJumpForce = 500f;
        defaultInAirJumps = 0f;

        // Abilities - Stamina
        staminaActive = true;
        decayFactor = 5f;
        recoveryFactor = 120f;

        // Abilities - Terminal Velocity
        terminalVelocityActive = true;

        // Abilities - Tic Tac
        ticTacActive = false;
        jumpForceWall = 800f;
        jumpForceForward = 150f;
        jumpForceNormal = 300f;
        jumpForceUp = 0f;
        legLength = 2f;
        allowableHeightFromGround = 1f;

        // Abilities - Dash
        dashActive = false;
        staminaDepletion = 30f;
        dashAirControl = 0.5f;
        dashForce = 2000f;
        weakDashForce = 1000f;
        resetTime = 0.5f;

        // Abilities - Grapple
        grappleActive = false;
        maxGrappleDistance = 30f;
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ClimberConfig))]
    public class PlayerConfigurationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ClimberConfig config = (ClimberConfig)target;

            if (GUILayout.Button("Reset to Defaults"))
            {
                Undo.RecordObject(config, "Reset to Defaults");
                config.ResetToDefaults();
                EditorUtility.SetDirty(config);
            }
        }
    }
#endif
}
