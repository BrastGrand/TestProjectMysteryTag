using System.Collections.Generic;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Data/LevelSettings")]
    public class LevelSettings : ScriptableObject
    {
        [SerializeField] private List<GameObject> asteroidList = new List<GameObject>();
        [SerializeField] private Collider destroyZoneCollider;
        [SerializeField] private Vector3 offsetDestroyZone;
        [SerializeField] private Vector3 offsetSpawn;
        [SerializeField] private float startWait;

        public void Init(out List<GameObject> asteroidPrefabList, out Collider destroyZone, out Vector3 offsetDestroyZone, out Vector3 offsetSpawn, out float startWait)
        {
            asteroidPrefabList = asteroidList;
            destroyZone = destroyZoneCollider;
            offsetDestroyZone = this.offsetDestroyZone;
            offsetSpawn = this.offsetSpawn;
            startWait = this.startWait;
        }
    }
}