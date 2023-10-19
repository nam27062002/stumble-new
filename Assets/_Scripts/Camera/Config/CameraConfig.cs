using UnityEngine;
public class CameraConfig : ScriptableObject
{
    [SerializeField] [Range(1, 10)] private float defaultDistance;
    [SerializeField] [Range(1, 10)] private float minimumDistance;
    [SerializeField] [Range(1, 10)] private float maximumDistance;
    [SerializeField] [Range(1, 10)] private float smoothing;
    [SerializeField] [Range(1, 10)] private float zoomSensitivity;
    public float DefaultDistance => defaultDistance;
    public float MinimumDistance => minimumDistance;
    public float MaximumDistance => maximumDistance;
    public float Smoothing => smoothing;
    public float ZoomSensitivity => zoomSensitivity;
}