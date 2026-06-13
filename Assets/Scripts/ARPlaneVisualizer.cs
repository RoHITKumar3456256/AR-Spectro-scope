using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

/// <summary>
/// ARPlaneVisualizer - Visualizes detected AR planes with custom materials
/// Shows detected surfaces to the user before placement
/// </summary>
[RequireComponent(typeof(ARPlaneManager))]
public class ARPlaneVisualizer : MonoBehaviour
{
    [Header("Plane Visuals")]
    public Material planeMaterial;
    public Color planeColor = new Color(0f, 0.8f, 1f, 0.3f);
    public bool showPlanes = true;

    private ARPlaneManager planeManager;
    private Dictionary<UnityEngine.XR.ARSubsystems.TrackableId, GameObject> planeObjects
        = new Dictionary<UnityEngine.XR.ARSubsystems.TrackableId, GameObject>();

    void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // Added planes
        foreach (ARPlane plane in args.added)
            UpdatePlaneVisual(plane);

        // Updated planes
        foreach (ARPlane plane in args.updated)
            UpdatePlaneVisual(plane);

        // Removed planes
        foreach (ARPlane plane in args.removed)
        {
            if (planeObjects.TryGetValue(plane.trackableId, out GameObject obj))
            {
                Destroy(obj);
                planeObjects.Remove(plane.trackableId);
            }
        }
    }

    void UpdatePlaneVisual(ARPlane plane)
    {
        if (planeMaterial == null) return;

        MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = planeMaterial;
            renderer.material.color = planeColor;
            renderer.enabled = showPlanes;
        }
    }

    public void SetPlanesVisible(bool visible)
    {
        showPlanes = visible;
        foreach (ARPlane plane in planeManager.trackables)
        {
            MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
            if (renderer != null)
                renderer.enabled = visible;
        }
    }
}
