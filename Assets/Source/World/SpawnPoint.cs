using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source
{
    /**
     * Generic spawn point that can instantiate any GameObject at its location
     */
    public class SpawnPoint : MonoBehaviour
    {
#nullable enable
        public T? Spawn<T>(GameObject template) where T : Object, new()
        {
            try
            {
                GameObject obj = Instantiate(template);
                obj.transform.position = transform.position;
                obj.transform.rotation = transform.rotation;
                return obj.GetComponent(typeof(T)) as T;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }
    }
#nullable disable
}