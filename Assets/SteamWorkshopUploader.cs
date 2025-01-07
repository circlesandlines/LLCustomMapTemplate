using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;      // Needed for async/await
using Steamworks;

[ExecuteInEditMode]
public class SteamWorkshopUploader : MonoBehaviour
{
    [Header("Subfolder name under '.../persistentDataPath/Mods'")]
    public string modFolderName = "";
    public string changeLog = "";
    public ulong workshopFileId;
    private string pdp;

    // Replace with your actual App ID
    private const uint APP_ID = 1417930;

    // Track whether we've initialized SteamClient
    private static bool _steamInitialized = false;

    /// <summary>
    /// Called by our custom editor button below.
    /// Wraps the async call so we can safely catch exceptions.
    /// </summary>
    public void OnUploadButtonClicked()
    {
        // Launch the async method, then catch any exceptions in ContinueWith
        /*
        UploadToWorkshopAsync().ContinueWith(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Exception in UploadToWorkshopAsync: {task.Exception}");
            }
        });
        */
        pdp = Application.persistentDataPath;
        Task.Run(() => UploadToWorkshopAsync()).Wait();
    }

    /// <summary>
    /// The core async method that does the workshop upload.
    /// Using async Task instead of async void helps prevent silent editor crashes.
    /// </summary>
    private async Task UploadToWorkshopAsync()
    {
        // 1. Ensure Steam is initialized
        if (!_steamInitialized && !SteamClient.IsValid)
        {
            try
            {
                SteamClient.Init(APP_ID);
                _steamInitialized = true;
                Debug.Log("SteamClient initialized.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to initialize SteamClient: " + ex);
                return; // Stop if we can’t init
            }
        }

        if (!SteamClient.IsValid)
        {
            Debug.LogError("SteamClient is not valid. Make sure Steam is running and you're logged in.");
            return;
        }

        // 2. Validate paths
        string modsDirectory = Path.Combine(pdp, "Mods");
        string fullModPath = Path.Combine(modsDirectory, modFolderName);

        if (!Directory.Exists(fullModPath))
        {
            Debug.LogError($"Folder does not exist: {fullModPath}");
            return;
        }

        // Create a file that stores the mod name, or overwrites it if it exists
        string modNameFilePath = Path.Combine(fullModPath, "ModName.txt");

        File.WriteAllText(modNameFilePath, modFolderName);

        // Required files
        string iconPath = Path.Combine(fullModPath, "map_icon.png");
        string descPath = Path.Combine(fullModPath, "description.txt");
        string metaPath = Path.Combine(fullModPath, "metadata.txt");

        if (!File.Exists(iconPath))
        {
            Debug.LogError("map_icon.png is missing.");
            return;
        }
        if (!File.Exists(descPath))
        {
            Debug.LogError("description.txt is missing.");
            return;
        }
        if (!File.Exists(metaPath))
        {
            Debug.LogError("metadata.txt is missing.");
            return;
        }

        // 3. Load up basic info
        string description = File.ReadAllText(descPath);
        // For simplicity, we'll use the mod folder name as the Workshop item title
        string mapTitle = modFolderName;

        Debug.Log("Starting Workshop upload...");

        // 4. Submit the Workshop item
        try
        {
            if (changeLog == "")
            {
                // This is the Facepunch v2.3.x "fluent" approach
                var result = await Steamworks.Ugc.Editor.NewCommunityFile
                    .WithTitle(mapTitle)
                    .WithDescription(description)
                    .ForAppId(APP_ID)
                    .WithPreviewFile(iconPath)
                    .WithContent(fullModPath)
                    .WithTag("Maps")
                    .WithTag("Rubeki")
                    .WithPublicVisibility()
                    .SubmitAsync();

                // 5. Check result
                if (result.Success)
                {
                    Debug.Log($"Success! Workshop item ID = {result.FileId}");
                    // Optionally, store the result.FileId somewhere for future updates
                    SteamClient.Shutdown();
                    _steamInitialized = false;
                }
                else
                {
                    Debug.LogError("Workshop upload failed: " + result.Result.ToString());
                    Debug.LogError("does it need workshop agreement? : " + result.NeedsWorkshopAgreement);
                    Debug.LogError("description : " + description);
                    Debug.LogError("map title : " + mapTitle);
                    Debug.LogError("icon path : " + iconPath);
                    Debug.LogError("content path : " + fullModPath);

                    SteamClient.Shutdown();
                    _steamInitialized = false;
                }
            }
            else // edited the file vs create
            {
                var editor = new Steamworks.Ugc.Editor(workshopFileId);

                editor = editor.WithTitle(mapTitle)
                    .WithDescription(description)
                    .WithChangeLog(changeLog)
                    .ForAppId(APP_ID)
                    .WithPreviewFile(iconPath)
                    .WithContent(fullModPath)
                    .WithTag("Maps")
                    .WithTag("Rubeki")
                    .WithPublicVisibility();

                // This is the Facepunch v2.3.x "fluent" approach
                var result = await editor.SubmitAsync();

                // 5. Check result
                if (result.Success)
                {
                    Debug.Log($"Success! Edited workshop item ID = {result.FileId}");
                    // Optionally, store the result.FileId somewhere for future updates
                    SteamClient.Shutdown();
                    _steamInitialized = false;
                }
                else
                {
                    Debug.LogError("Workshop upload failed: " + result.Result.ToString());
                    Debug.LogError("does it need workshop agreement? : " + result.NeedsWorkshopAgreement);
                    Debug.LogError("description : " + description);
                    Debug.LogError("map title : " + mapTitle);
                    Debug.LogError("icon path : " + iconPath);
                    Debug.LogError("content path : " + fullModPath);

                    SteamClient.Shutdown();
                    _steamInitialized = false;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Exception during Workshop upload: {e}");
            SteamClient.Shutdown();
            _steamInitialized = false;
        }
        finally
        {
            // If you want to immediately shut down Steam after upload:
            // SteamClient.Shutdown();
            // _steamInitialized = false;
        }
    }
}

#if UNITY_EDITOR
/// <summary>
/// Simple custom editor showing a button to trigger OnUploadButtonClicked().
/// </summary>
[CustomEditor(typeof(SteamWorkshopUploader))]
public class SingleScriptWorkshopUploaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var uploader = (SteamWorkshopUploader)target;
        if (GUILayout.Button("Upload to Steam Workshop"))
        {
            uploader.OnUploadButtonClicked();
        }
    }
}
#endif
