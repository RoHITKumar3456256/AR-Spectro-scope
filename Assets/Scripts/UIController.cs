using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// UIController - Manages HUD, status messages, and button interactions
/// for the AR Spectro-scope application
/// </summary>
public class UIController : MonoBehaviour
{
    [Header("Status UI")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI versionText;

    [Header("Scanning UI")]
    public GameObject scanningIndicator;
    public Image scanRingImage;
    public float scanRingSpeed = 90f;

    [Header("Buttons")]
    public Button scanButton;
    public Button resetButton;
    public Button helpButton;
    public Button closeHelpButton;

    [Header("Panels")]
    public GameObject helpPanel;
    public GameObject mainHUD;

    private ARSpectroManager spectroManager;
    private bool helpVisible = false;

    void Start()
    {
        spectroManager = FindObjectOfType<ARSpectroManager>();

        // Button listeners
        scanButton?.onClick.AddListener(OnScanPressed);
        resetButton?.onClick.AddListener(OnResetPressed);
        helpButton?.onClick.AddListener(OnHelpPressed);
        closeHelpButton?.onClick.AddListener(OnHelpPressed);

        // Initial UI state
        if (helpPanel != null) helpPanel.SetActive(false);
        SetScanning(false);
        SetStatus("Initializing AR...");
        SetInstruction("Please wait while AR session starts");

        if (versionText != null)
            versionText.text = "AR Spectro-scope v1.0";

        StartCoroutine(WaitForARReady());
    }

    void Update()
    {
        // Rotate scanning ring
        if (scanRingImage != null && scanningIndicator != null && scanningIndicator.activeSelf)
            scanRingImage.transform.Rotate(0f, 0f, -scanRingSpeed * Time.deltaTime);
    }

    IEnumerator WaitForARReady()
    {
        yield return new WaitForSeconds(2f);
        SetStatus("AR Ready");
        SetInstruction("Point camera at a flat surface, then tap to scan");
    }

    void OnScanPressed()
    {
        SetStatus("Scanning environment...");
        SetInstruction("Tap on a detected surface to place the Spectro-scope");
        SetScanning(true);
        spectroManager?.EnablePlaneDetection(true);
    }

    void OnResetPressed()
    {
        spectroManager?.ResetScan();
        SetStatus("Reset complete");
        SetInstruction("Point camera at a flat surface");
        SetScanning(false);
    }

    void OnHelpPressed()
    {
        helpVisible = !helpVisible;
        if (helpPanel != null) helpPanel.SetActive(helpVisible);
    }

    public void OnAnalysisComplete(string result)
    {
        SetStatus("Analysis Complete");
        SetInstruction("Tap Reset to scan again");
        SetScanning(false);
    }

    public void SetStatus(string msg)
    {
        if (statusText != null) statusText.text = msg;
    }

    public void SetInstruction(string msg)
    {
        if (instructionText != null) instructionText.text = msg;
    }

    public void SetScanning(bool active)
    {
        if (scanningIndicator != null)
            scanningIndicator.SetActive(active);
    }
}
