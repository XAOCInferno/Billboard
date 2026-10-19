using System;
using Source.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

namespace Source
{
    /**
     * Character, a possessable object
     */
    [RequireComponent(typeof(CharacterState))]
    [RequireComponent(typeof(CustomCharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    [Serializable]
    public class Character : MonoBehaviour, IPossessable
    {
        [SerializeField] private Vector3 cameraOffset;

        protected CustomCharacterController controller;
        protected CharacterState state;

        private void Awake()
        {
            controller = GetComponent<CustomCharacterController>();
            state = GetComponent<CharacterState>();
            state.OnPossessedChange += HandlePossessedChange;
        }

        /**
         * Possess this object
         * Informs state we are possessed
         */
        public void Possess()
        {
            state.possessed = true;
        }

        /**
         * Handles possession changing
         * Will attach camera to this object if possessed
         */
        private void HandlePossessedChange(bool possessed)
        {
            if (!possessed) return;
            CameraController.AttachCamera(transform, cameraOffset);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}