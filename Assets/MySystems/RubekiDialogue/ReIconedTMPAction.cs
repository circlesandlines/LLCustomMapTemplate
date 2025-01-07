using TMPro;
using UnityEngine;

namespace NullSave
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ReIconedTMPAction : MonoBehaviour
    {

        #region Variables

        [Tooltip("Rewired Player Id")] public int playerId;
        [Tooltip("How should we lookup this action?")] public ReIconedActionLookup actionLookup;
        [Tooltip("Which controller do we prefer?")] public ReIconedUpdateType updateType;
        [Tooltip("Which Action should we monitor?")] public string actionName;
        [Tooltip("Which Action Id should we monitor?")] public int actionId;
        [Tooltip("What text do we want to update with the action?"), TextArea(2, 5)] public string formatText;
        [Tooltip("What text do we want to use if we can't find the action?"), TextArea(2, 5)] public string notFoundText;
        [Tooltip("Allow sprite tinting?")] public bool tint;
        [Tooltip("This should ensure mouse icons are displayed, nothing else")] public bool mouseOnly = false;
        [Tooltip("This should ensure using localize the text, provided a key")] public bool localized = false;
        [Tooltip("The localization table key for looking up the string")] public string key = "";
        [Tooltip("The localization table for looking up the string")] public string table = "";
        //[Tooltip("Should this component refresh the value when it's changed?")] public bool noRefresh = false;
        //[Tooltip("Has the component completed the replacement?")] public bool done = false;

        private TextMeshProUGUI tmpText;
        private Controller lastActive;

        #endregion
        /*
        #region Unity Methods

        private void Awake()
        {
            if (localized)
            {
                // replace key with localized text here
                // should be in the format:
                // <localized text>{action}<localized text>
                try
                {
                    formatText = LocalizationSettings.StringDatabase.GetLocalizedString(table, key).Replace("<br>", "\n");
                    if (ReIconed.GetActiveController(0).type == Rewired.ControllerType.Mouse)
                        mouseOnly = true;
                }
                catch
                {
                    //Debug.LogError("Couldn't find localized text");
                }
            }

            tmpText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            switch (updateType)
            {
                case ReIconedUpdateType.ActiveInput:
                    ReInput.InputSourceUpdateEvent += UpdateUI;
                    break;
                case ReIconedUpdateType.PreferController:
                case ReIconedUpdateType.PreferKeyboard:
                    ReInput.ControllerConnectedEvent += ControllerChanged;
                    ReInput.ControllerDisconnectedEvent += ControllerChanged;
                    break;
            }

            UpdateUI();
        }

        private void Reset()
        {
            playerId = 0;
            actionName = "Horizontal";
            actionId = 0;
            formatText = "Horizontal: {action}";
            notFoundText = "<sprite=0>";
            tint = true;
        }

        #endregion

        #region Public Methods

        public void SetFormatText(string value)
        {
            formatText = value;
            UpdateUI();
        }

        #endregion

        #region Private Methods

        private void ControllerChanged(ControllerStatusChangedEventArgs args)
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            //Debug.Log("update icons in reiconed 1: actionName: " + actionName + ", updateType: " + updateType);
            Controller activeController = ReIconed.GetActiveController(playerId);

            lastActive = activeController;

            InputMap map = actionLookup == ReIconedActionLookup.Name ? ReIconed.GetActionHardwareInput(actionName, playerId, updateType, mouseOnly) : ReIconed.GetActionHardwareInput(actionId, playerId, updateType, mouseOnly);
            
            //Debug.Log("InputMap map = " + map.inputName + ", " + map.tmpSpriteIndex.ToString());

            if (map == null)
            {
                Debug.LogError("why is the icon map null? action: " + actionName);
                tmpText.spriteAsset = null;
                tmpText.text = formatText.Replace("{action}", notFoundText);
            }
            else
            {
                tmpText.spriteAsset = map.TMPSpriteAsset;

                // Mouse button 3 is always selected when scrolling the mousewheel on a button based action.
                // Default to mouse button instead
                // For some reason, these default to keyboard, not mouse - even when mouseonly is selected...
                //Debug.Log("TMPA: notfoundtext = " + map.inputName);
                //notFoundText = map.inputName;

                tmpText.text = formatText.Replace("{action}", "<sprite=" + map.tmpSpriteIndex + " tint=" + (tint ? "1" : "0") + ">");
            }
        }

        #endregion
        */

    }

    public class ReIconedActionLookup : MonoBehaviour
    {

    }

    public class ReIconedUpdateType : MonoBehaviour
    {

    }

    public class Controller : MonoBehaviour
    {

    }
}