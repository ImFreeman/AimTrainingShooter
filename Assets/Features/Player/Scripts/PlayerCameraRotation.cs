using Features.Input;
using UnityEngine;
using Zenject;

namespace Features.Player
{
    public readonly struct PlayerCameraRotationProtocol
    {
        public readonly Transform CameraTransform;
        public readonly Transform PlayerBodyTransform;
        public readonly float Sensivity;
        public PlayerCameraRotationProtocol(Transform cameraTransform, Transform playerBodyTransform, float sensivity)
        {
            CameraTransform = cameraTransform;
            PlayerBodyTransform = playerBodyTransform;
            Sensivity = sensivity;
        }
    }

    public class PlayerCameraRotation : ITickable
    {
        private readonly TickableManager _tickableManager;
        private readonly float _sensivity;
        private readonly Transform _cameraTransform;
        private readonly Transform _playerBodyTransform;
        private readonly ICameraRotationInput _cameraRotationInput;

        private bool _isActive;
        private float _xRotation = 0f;

        public PlayerCameraRotation(
            TickableManager tickableManager,
            ICameraRotationInput cameraRotationInput,
            PlayerCameraRotationProtocol protocol
            )
        {
            _cameraRotationInput = cameraRotationInput;
            _tickableManager = tickableManager;
            _sensivity = protocol.Sensivity;
            _cameraTransform = protocol.CameraTransform;
            _playerBodyTransform = protocol.PlayerBodyTransform;
        }

        public void SetActive(bool value)
        {
            if (value != _isActive)
            {
                if (value)
                {
                    _tickableManager.Add(this);
                }
                else
                {
                    _tickableManager.Remove(this);
                }
                _isActive = value;
            }
        }

        public void Tick()
        {
            float mouseX = _cameraRotationInput.InputX * _sensivity * Time.deltaTime;
            float mouseY = _cameraRotationInput.InputY * _sensivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            _cameraTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBodyTransform.Rotate(Vector3.up * mouseX);
        }
    }
}