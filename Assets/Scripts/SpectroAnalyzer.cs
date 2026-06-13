using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// SpectroAnalyzer - Core spectroscopy simulation engine
/// Generates and displays spectral band data in AR overlay
/// </summary>
public class SpectroAnalyzer : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshPro spectralReadout;
    public TextMeshPro wavelengthLabel;
    public GameObject dataPanel;
    public LineRenderer spectrumLine;

    [Header("Scan Settings")]
    public float scanDuration = 2.5f;
    public int spectrumResolution = 64;

    [Header("Colors")]
    public Color violetColor = new Color(0.58f, 0f, 0.83f);
    public Color redColor    = new Color(1f, 0.1f, 0.1f);

    private bool isAnalyzing = false;

    // Visible light spectrum wavelengths (nm)
    private readonly float[] wavelengths = { 380f, 420f, 450f, 490f, 530f, 570f, 600f, 630f, 680f, 750f };
    private float[] intensities;

    void Awake()
    {
        intensities = new float[wavelengths.Length];
        if (dataPanel != null) dataPanel.SetActive(false);
    }

    public void BeginAnalysis()
    {
        if (!isAnalyzing)
            StartCoroutine(RunSpectralScan());
    }

    IEnumerator RunSpectralScan()
    {
        isAnalyzing = true;
        if (dataPanel != null) dataPanel.SetActive(true);

        // Scanning animation
        float elapsed = 0f;
        while (elapsed < scanDuration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / scanDuration;
            UpdateReadout($"Scanning... {(int)(progress * 100)}%");
            yield return null;
        }

        // Simulate spectral readings
        for (int i = 0; i < intensities.Length; i++)
            intensities[i] = Random.Range(0.15f, 1.0f);

        DrawSpectrum();
        DisplayResults();
        isAnalyzing = false;
    }

    void DrawSpectrum()
    {
        if (spectrumLine == null) return;

        spectrumLine.positionCount = wavelengths.Length;
        for (int i = 0; i < wavelengths.Length; i++)
        {
            float x = Mathf.Lerp(-0.5f, 0.5f, i / (float)(wavelengths.Length - 1));
            float y = intensities[i] * 0.3f;
            spectrumLine.SetPosition(i, new Vector3(x, y, 0));
        }

        // Gradient from violet to red
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(violetColor, 0.0f),
                new GradientColorKey(Color.blue,  0.2f),
                new GradientColorKey(Color.cyan,  0.4f),
                new GradientColorKey(Color.green, 0.55f),
                new GradientColorKey(Color.yellow,0.7f),
                new GradientColorKey(redColor,    1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );
        spectrumLine.colorGradient = gradient;
    }

    void DisplayResults()
    {
        int peakIdx = GetPeakIndex();
        float peakWavelength = wavelengths[peakIdx];
        string band = GetSpectralBand(peakWavelength);
        float peakIntensity = intensities[peakIdx];

        UpdateReadout(
            $"SPECTRAL ANALYSIS\n" +
            $"─────────────────\n" +
            $"Peak: {peakWavelength:F0} nm\n" +
            $"Band: {band}\n" +
            $"Intensity: {peakIntensity:P0}\n" +
            $"Status: COMPLETE"
        );

        if (wavelengthLabel != null)
            wavelengthLabel.text = $"{peakWavelength:F0} nm";
    }

    int GetPeakIndex()
    {
        int peak = 0;
        for (int i = 1; i < intensities.Length; i++)
            if (intensities[i] > intensities[peak]) peak = i;
        return peak;
    }

    string GetSpectralBand(float nm)
    {
        if (nm < 420) return "Violet";
        if (nm < 450) return "Blue-Violet";
        if (nm < 490) return "Blue";
        if (nm < 530) return "Cyan-Green";
        if (nm < 570) return "Green";
        if (nm < 600) return "Yellow";
        if (nm < 630) return "Orange";
        if (nm < 680) return "Red";
        return "Deep Red";
    }

    void UpdateReadout(string text)
    {
        if (spectralReadout != null)
            spectralReadout.text = text;
    }

    public bool IsAnalyzing() => isAnalyzing;
}
