using System;
using Cinemachine;
using UnityEngine;

namespace MovementSystem
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CameraConfig cameraConfig;
        private CinemachineFramingTransposer _framingTransposer;
        private CinemachineInputProvider _inputProvider;
        private float _currentTargetDistance;

        private const float Tolerance = 0.001f;

        private void Awake()
        {
            InitializeCamera();
            _framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            _inputProvider = GetComponent<CinemachineInputProvider>();
            _currentTargetDistance = cameraConfig.DefaultDistance;
        }

        private void Update()
        {
            ZoomCamera();
        }

        private void InitializeCamera()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void ZoomCamera()
        {
            float zoomValue = CalculateZoomValue();
            UpdateTargetDistance(zoomValue);
            UpdateCameraDistance();
        }

        private float CalculateZoomValue()
        {
            return _inputProvider.GetAxisValue(2) * cameraConfig.ZoomSensitivity;
        }

        private void UpdateTargetDistance(float zoomValue)
        {
            _currentTargetDistance = Mathf.Clamp(_currentTargetDistance + zoomValue, cameraConfig.MinimumDistance, cameraConfig.MaximumDistance);
        }

        private void UpdateCameraDistance()
        {
            float currentDistance = _framingTransposer.m_CameraDistance;

            if (Math.Abs(currentDistance - _currentTargetDistance) < Tolerance) 
                return;

            float lerpedZoomValue = Mathf.Lerp(currentDistance, _currentTargetDistance, cameraConfig.Smoothing * Time.deltaTime);
            _framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}