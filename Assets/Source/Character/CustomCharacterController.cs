using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Source
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterState))]
    public class CustomCharacterController : MonoBehaviour
    {
        private Vector2 _moveDir = Vector2.zero;
        private Vector2 _lookDir = Vector2.zero;
        private CharacterState _state;

        private PlayerInput _playerInput;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _rigidbody = GetComponent<Rigidbody>();
            _state = GetComponent<CharacterState>();
            _state.OnPossessedChange += HandlePossessedChange;
        }

        /**
         * Handles player possession changing
         */
        private void HandlePossessedChange(bool possessed)
        {
            if (possessed)
            {
                _playerInput.camera = Camera.main;
                _playerInput.enabled = true;
            }
        }

        /**
         * Message sent from Unity whenever movement input actions are pressed.
         * Extract the move and handle it
         */
        public void OnMove(InputValue value)
        {
            _moveDir = value.Get<Vector2>();

            StopCoroutine(nameof(HandleMove));
            if (_moveDir == Vector2.zero) return;

            StartCoroutine(nameof(HandleMove));
        }

        /**
         * Message sent from Unity whenever rotation input actions are pressed.
         * Extract the rotation and handle it
         */
        public void OnLook(InputValue value)
        {
            Vector2 v = value.Get<Vector2>().normalized;
            _lookDir = v;

            StopCoroutine(nameof(HandleRotation));
            if (_lookDir == Vector2.zero) return;

            StartCoroutine(nameof(HandleRotation));
        }

        /**
         * Message sent from Unity whenever sprint input action is pressed.
         * Toggle sprinting
         */
        public void OnSprint(InputValue value)
        {
            _state.sprinting = value.isPressed;
        }

        /**
         * Coroutine to handle movement.
         * Continuously moves the player in the direction of movement while we have a move direction
         */
        private IEnumerator HandleMove()
        {
            while (_moveDir != Vector2.zero)
            {
                yield return typeof(WaitForEndOfFrame);

                Vector3 move = new Vector3(_moveDir.x, 0, _moveDir.y) * _state.speed;
                // TODO: setting velocity may override any physics we are planning to make
                _rigidbody.linearVelocity = transform.TransformDirection(move);
            }

            _rigidbody.linearVelocity = Vector3.zero;

            yield return null;
        }

        /// Handles the character's rotation based on the look direction input.
        /// The method continuously updates the character's rotation while look input is received by adjusting the local rotation of the transform.
        /// It also updates the camera's rotation based on vertical (Y-axis) look input.
        /// <returns>An IEnumerator that continuously processes rotation updates until the look input is cleared.</returns>
        private IEnumerator HandleRotation()
        {
            while (_lookDir != Vector2.zero)
            {
                // TODO: In player settings
                const float lookSensitivityX = 0.5f;

                // Extract rotation
                Vector2 look = _lookDir * _state.speed;
                float x = look.x;
                float y = look.y;

                // Calculate
                Vector3 currentAngles = transform.localRotation.eulerAngles;
                Vector3 rotation = Vector3.up * (x + currentAngles.y);
                Quaternion from = Quaternion.Euler(currentAngles);
                Quaternion to = Quaternion.Euler(rotation);

                // TODO: Apply clamps and dampening

                // Apply
                transform.localRotation = Quaternion.RotateTowards(from, to, lookSensitivityX);
                CameraController.RotatePitch(y);

                yield return typeof(WaitForEndOfFrame);
            }

            yield return null;
        }
    }
}