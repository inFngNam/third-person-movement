using Cinemachine;
using UnityEngine;

namespace Controller
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField, Range(0, 10f)] private float defaultDistance;
        [SerializeField, Range(0, 10f)] private float minimumDistance;
        [SerializeField, Range(0, 10f)] private float maximumDistance;

        [SerializeField, Range(0, 10f)] private float smoothing = 4;
        [SerializeField, Range(0, 10f)] private float zoomSensitivity = 1;

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private CinemachineFramingTransposer framingTransposer;
        [SerializeField] private CinemachineInputProvider inputProvider;

        private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            currentTargetDistance = defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            float zoom = inputProvider.GetAxisValue(2) * zoomSensitivity;

            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoom, minimumDistance, maximumDistance);

            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
            {
                return;
            }

            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
            framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}