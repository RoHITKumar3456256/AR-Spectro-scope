using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI Controller for AR Spectro-scope
/// Manages HUD, buttons, and status display
/// </summary>
public class UIController : MonoBehaviour
{
    [Header("HUD Elements")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI instructionText;
    public GameObject scanningIndicator;

    [Header("Buttons")]
    public Button scanButton;
    public Button resetButton;
    public Button helpButton;

    private ARSpectroManager spectroManager;

    void Start()
    {
        spectroManager = FindObjectOfType<ARSpectroManager>();

        scanButton?.onClick.AddListener(OnScanPressed);
        resetButton?.onClick.AddListener(OnResetPressed);
        helpButton?.onClick.AddListener(OnHelpPressed);

        SetStatus("Point camera at a flat surface");
        SetInstruction("Tap to place Spectro-scope");
        SetScanning(false);
    }

    void OnScanPressed()
    {
        SetStatus("Scanning environment...");
        SetScanning(true);
        spectroManager?.EnablePlaneDetection(true);
    }

    void OnResetPressed()
    {
        spectroManager?.ResetScan();
        SetStatus("Reset. Point at a surface.");
        SetScanning(false);
    }

    void OnHelpPressed()
    {
        SetInstruction("1. Point camera at flat surface\n2. Tap to place scanner\n3. Wait for spectral analysis");
    }

    public void SetStatus(string msg) => statusText?.SetText(msg);
    public void SetInstruction(string msg) => instructionText?.SetText(msg);
    public void SetScanning(bool active)
    {
        if (scanningIndicator != null)
            scanningIndicator.SetActive(active);
    }
}
