using System;
using UnityEngine;

namespace Source
{
    /**
     * State of a character
     */
    [Serializable]
    public class CharacterState : MonoBehaviour
    {
        [SerializeField] [Range(1, 5)] private float baseSpeed = 2.5f;
        [SerializeField] [Range(1.01f, 5)] private float sprintMultiplier = 2f;

        public float speed => sprinting ? baseSpeed * sprintMultiplier : baseSpeed;

        private bool _possessed;

        public bool possessed
        {
            get => _possessed;
            set
            {
                _possessed = value;
                OnPossessedChange?.Invoke(_possessed);
            }
        }

        public bool sprinting { get; set; }

        public event Action<bool> OnPossessedChange;
    }
}