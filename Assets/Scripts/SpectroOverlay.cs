using UnityEngine;
using System.Collections;

/// <summary>
/// SpectroOverlay - Controls the 3D AR overlay object
/// Handles animations, pulse effects, and spectral ring display
/// </summary>
public class SpectroOverlay : MonoBehaviour
{
    [Header("Animation")]
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.05f;
    public float rotateSpeed = 30f;
    public GameObject spectralRing;
    public GameObject scanBeam;

    [Header("Spawn FX")]
    public float spawnDuration = 0.5f;
    public AnimationCurve spawnCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 originalScale;
    private bool isSpawning = false;
    private bool isActive = false;

    void Awake()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Start()
    {
        StartCoroutine(SpawnAnimation());
    }

    void Update()
    {
        if (!isActive) return;

        // Pulse scale effect
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;

        // Rotate spectral ring
        if (spectralRing != null)
            spectralRing.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    IEnumerator SpawnAnimation()
    {
        isSpawning = true;
        float elapsed = 0f;
        while (elapsed < spawnDuration)
        {
            elapsed += Time.deltaTime;
            float t = spawnCurve.Evaluate(elapsed / spawnDuration);
            transform.localScale = originalScale * t;
            yield return null;
        }
        transform.localScale = originalScale;
        isSpawning = false;
        isActive = true;

        // Start scan beam
        if (scanBeam != null)
            StartCoroutine(ScanBeamAnimation());
    }

    IEnumerator ScanBeamAnimation()
    {
        if (scanBeam == null) yield break;
        scanBeam.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        scanBeam.SetActive(false);
    }

    public void Deactivate()
    {
        StartCoroutine(DespawnAnimation());
    }

    IEnumerator DespawnAnimation()
    {
        isActive = false;
        float elapsed = 0f;
        Vector3 startScale = transform.localScale;
        while (elapsed < 0.3f)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, elapsed / 0.3f);
            yield return null;
        }
        Destroy(gameObject);
    }
}
