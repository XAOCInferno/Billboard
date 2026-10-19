using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Source
{
    public class PlayerSingleton : Singleton<PlayerSingleton>
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private SpawnPoint spawnPoint;

        [CanBeNull] public Character playerInstance { get; private set; }

        protected void Start()
        {
            playerInstance = spawnPoint.Spawn<Character>(playerPrefab);
            Assert.IsNotNull(playerInstance);
            
            playerInstance.Possess();
        }
    }
}