using UnityEngine;

namespace Source
{
    /**
     * Generic Singleton implementation
     * Ensures no duplication of the Singleton
     */
    public class Singleton<T> : MonoBehaviour
    {
        public static Singleton<T> instance { get; private set; }

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.
            if (instance != null && instance != this)
            {
                Debug.LogWarning($"Duplicate singleton instance! {this}");
                Destroy(this);
                return;
            }

            instance = this;
        }
    }
}