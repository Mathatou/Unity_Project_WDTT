using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.Manager;

namespace UnityTutorial.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;

        [Header("Camera attributes")]
        [Space]
        [SerializeField] private Transform CameraRoot;              // Reference to the camera's root (pivot point)
        [SerializeField] private Transform Camera;                  // Reference to the actual camera
        [SerializeField] private float UpperLimit = -40f;           // Camera pitch upper limit
        [SerializeField] private float BottomLimit = 70f;           // Camera pitch lower limit
        [SerializeField] private float MouseSensitivity = 21.9f;    // Mouse sensitivity for camera movement

        [Header("In air management")]
        [Space]
        [SerializeField] private float Dis2Ground = 0.8f;           // Distance to check for grounding
        [SerializeField] private LayerMask GroundCheck;             // Layer used to detect the ground
        [SerializeField] private float AirResistance = 0.8f;        // Air resistance when player is airborne

        // Components
        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;

        // Animator and control state
        private bool _grounded = false;
        private bool _hasAnimator;

        // Animator parameter hashes
        private int _xVelHash;
        private int _yVelHash;
        private int _zVelHash;
        private int _groundHash;
        private int _fallingHash;
        private int _crouchHash;

        // Camera rotation state
        private float _xRotation;

        // Movement constants and state
        private const float _walkSpeed = 2f;
        private const float _runSpeed = 6f;
        private Vector2 _currentVelocity;

        private void Start()
        {
            // Cache component references
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            // Cache animation parameter hashes for performance
            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");
            _crouchHash = Animator.StringToHash("Crouch");
        }

        private void FixedUpdate()
        {
            SampleGround();    // Check if the player is grounded
            Move();            // Handle movement logic
            HandleCrouch();    // Handle crouch state
        }

        private void LateUpdate()
        {
            CamMovements();    // Handle camera rotation
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            // Determine target speed based on input
            float targetSpeed = _inputManager.Run ? _runSpeed : _walkSpeed;
            if (_inputManager.Crouch) targetSpeed = 1.5f;
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

            // Ground movement
            if (_grounded)
            {
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
                var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

                // Apply force to match target velocity
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            }
            // Air movement (apply resistance)
            else
            {
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance, 0, _currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }

            // Update animation parameters
            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;

            // Keep camera positioned at the camera root
            Camera.position = CameraRoot.position;

            // Rotate vertically (pitch)
            _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);

            // Rotate player horizontally (yaw)
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void HandleCrouch()
        {
            // Update crouch animation state
            _animator.SetBool(_crouchHash, _inputManager.Crouch);
        }

        private void SampleGround()
        {
            if (!_hasAnimator) return;

            // Cast ray downward to check if grounded
            RaycastHit hitInfo;
            if (Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                _grounded = true;
                SetAnimationGrounding();
                return;
            }

            // Player is airborne
            _grounded = false;
            _animator.SetFloat(_zVelHash, _playerRigidbody.velocity.y);
            SetAnimationGrounding();
        }

        private void SetAnimationGrounding()
        {
            // Update grounded/falling animation states
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }
    }
}
