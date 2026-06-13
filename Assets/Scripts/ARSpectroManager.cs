using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// AR Spectro-scope - Main AR Session Manager
/// Handles AR plane detection and spectral overlay placement
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
public class ARSpectroManager : MonoBehaviour
{
    [Header("AR Components")]
    public ARSessionOrigin arSessionOrigin;
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager arRaycastManager;

    [Header("Spectro Overlay")]
    public GameObject spectroOverlayPrefab;
    public GameObject spectroUIPrefab;

    [Header("Settings")]
    public bool autoStartScan = true;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject currentOverlay;
    private bool isScanning = false;

    void Start()
    {
        Debug.Log("[AR Spectro-scope] Initializing AR session...");
        if (autoStartScan)
            StartCoroutine(InitializeAR());
    }

    IEnumerator InitializeAR()
    {
        yield return new WaitUntil(() => ARSession.state == ARSessionState.SessionTracking);
        Debug.Log("[AR Spectro-scope] AR Session active.");
        EnablePlaneDetection(true);
    }

    void Update()
    {
        if (!isScanning) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                HandleTouchPlacement(touch.position);
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            HandleTouchPlacement(Input.mousePosition);
#endif
    }

    void HandleTouchPlacement(Vector2 screenPosition)
    {
        if (arRaycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            PlaceSpectroOverlay(hitPose);
        }
    }

    void PlaceSpectroOverlay(Pose pose)
    {
        if (currentOverlay != null)
            Destroy(currentOverlay);

        if (spectroOverlayPrefab != null)
        {
            currentOverlay = Instantiate(spectroOverlayPrefab, pose.position, pose.rotation);
            Debug.Log("[AR Spectro-scope] Overlay placed at: " + pose.position);
            StartSpectralAnalysis();
        }
    }

    void StartSpectralAnalysis()
    {
        if (currentOverlay == null) return;
        SpectroAnalyzer analyzer = currentOverlay.GetComponent<SpectroAnalyzer>();
        if (analyzer != null)
            analyzer.BeginAnalysis();
    }

    public void EnablePlaneDetection(bool enable)
    {
        if (arPlaneManager != null)
            arPlaneManager.enabled = enable;
        isScanning = enable;
        Debug.Log("[AR Spectro-scope] Plane detection: " + enable);
    }

    public void ResetScan()
    {
        if (currentOverlay != null)
            Destroy(currentOverlay);
        EnablePlaneDetection(true);
        Debug.Log("[AR Spectro-scope] Scan reset.");
    }

    public bool IsScanning() => isScanning;
}
