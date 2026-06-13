using System.Collections;
using UnityEngine;
using UnityEngine.Android;

/// <summary>
/// CameraPermissionHandler - Requests Android camera permission at runtime
/// Required for AR Foundation on Android devices
/// </summary>
public class CameraPermissionHandler : MonoBehaviour
{
    [Header("UI")]
    public GameObject permissionPromptPanel;
    public UnityEngine.UI.Button retryButton;

    private bool permissionGranted = false;

    void Start()
    {
        retryButton?.onClick.AddListener(RequestPermission);
        StartCoroutine(CheckAndRequestPermission());
    }

    IEnumerator CheckAndRequestPermission()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            if (permissionPromptPanel != null)
                permissionPromptPanel.SetActive(true);

            Permission.RequestUserPermission(Permission.Camera);

            // Wait for user response
            yield return new WaitForSeconds(1f);
            float timeout = 10f;
            float elapsed = 0f;
            while (!Permission.HasUserAuthorizedPermission(Permission.Camera) && elapsed < timeout)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        permissionGranted = Permission.HasUserAuthorizedPermission(Permission.Camera);
        if (permissionPromptPanel != null)
            permissionPromptPanel.SetActive(!permissionGranted);

        if (permissionGranted)
            Debug.Log("[AR Spectro-scope] Camera permission granted.");
        else
            Debug.LogWarning("[AR Spectro-scope] Camera permission DENIED.");
#else
        permissionGranted = true;
        yield return null;
#endif
    }

    void RequestPermission()
    {
        StartCoroutine(CheckAndRequestPermission());
    }

    public bool IsPermissionGranted() => permissionGranted;
}
