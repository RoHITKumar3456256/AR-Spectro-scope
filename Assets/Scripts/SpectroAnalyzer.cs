using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Spectral Analyzer - Simulates spectroscopy data overlay
/// in Augmented Reality view
/// </summary>
public class SpectroAnalyzer : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshPro spectralReadout;
    public GameObject dataPanel;
    public LineRenderer spectrumLine;

    [Header("Spectral Settings")]
    public float scanDuration = 2.5f;
    public Color lowFreqColor = Color.red;
    public Color highFreqColor = Color.blue;

    private bool isAnalyzing = false;

    // Simulated spectral bands (nm wavelengths)
    private float[] wavelengths = { 380f, 450f, 495f, 570f, 590f, 620f, 750f };
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

        UpdateReadout("Scanning...");
        yield return new WaitForSeconds(scanDuration);

        // Generate random spectral data to simulate real readings
        for (int i = 0; i < intensities.Length; i++)
            intensities[i] = Random.Range(0.1f, 1.0f);

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
            float x = (i / (float)(wavelengths.Length - 1)) - 0.5f;
            float y = intensities[i] * 0.3f;
            spectrumLine.SetPosition(i, new Vector3(x, y, 0));
        }

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(lowFreqColor, 0.0f),
                new GradientColorKey(highFreqColor, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 1.0f)
            }
        );
        spectrumLine.colorGradient = gradient;
    }

    void DisplayResults()
    {
        float peakWavelength = wavelengths[GetPeakIndex()];
        string band = GetSpectralBand(peakWavelength);
        UpdateReadout($"Peak: {peakWavelength:F0} nm\nBand: {band}\nIntensity: {intensities[GetPeakIndex()]:P0}");
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
        if (nm < 450) return "Violet";
        if (nm < 495) return "Blue";
        if (nm < 570) return "Green";
        if (nm < 590) return "Yellow";
        if (nm < 620) return "Orange";
        return "Red";
    }

    void UpdateReadout(string text)
    {
        if (spectralReadout != null)
            spectralReadout.text = text;
    }
}
