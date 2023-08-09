using Configure;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

namespace Character.Controller
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        private Transform cameraTransform;
        public const float RotationSmoothTime = 0.12f;
        private Vector3 verticalVelocity;
        private bool isGrounded;
        private float _targetRotation;
        private float _rotationVelocity;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;
        }

        private void FixedUpdate()
        {
            ProcessGravity();
        }

        public void ProcessMove(Vector2 input, float movementSpeed)
        {
            Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

            if (inputDirection != Vector3.zero)
            {
                _targetRotation = GetDirectionAngle(inputDirection) + GetCameraDirectionAngle();
                float rotation = Mathf.SmoothDampAngle(
                    transform.eulerAngles.y, _targetRotation,
                    ref _rotationVelocity, RotationSmoothTime
                );
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
            Vector3 horizontalVelocity = targetDirection.normalized * movementSpeed;
            characterController.Move((horizontalVelocity + verticalVelocity) * Time.fixedDeltaTime);
        }

        private void ProcessGravity()
        {
            verticalVelocity.y += EnvironmentConfigure.Gravity * Time.fixedDeltaTime;
            if (isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2;
            }
        }

        public void ProcessJump(float jumpHeight)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * EnvironmentConfigure.Gravity);
            }
        }

        private float GetDirectionAngle(Vector3 inputDirection)
        {
            return Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        }

        private float GetCameraDirectionAngle() => cameraTransform.eulerAngles.y;
    }
}