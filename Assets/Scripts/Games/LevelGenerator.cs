using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestProjectForMysteryTag
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private float waveWait;
        [SerializeField] private float countAsteroidsInWave;
        private List<GameObject> asteroidPrefabs;
        private Collider destroyZoneCollider;
        private Vector3 offsetDestroyZone;
        private Vector3 offsetSpawn;
        private Vector3 zoneSpawn;
        private float startWait;
        private float spawnWait;

        private Coroutine wavesCoroutine;

        public void Init(Vector3 zoneSpawn, Vector3 zoneDestroy, float spawnWait, float waveWait, int countAsteroidsInWave = 5)
        {
            this.zoneSpawn = zoneSpawn;
            this.spawnWait = spawnWait;
            this.waveWait = waveWait;
            this.countAsteroidsInWave = countAsteroidsInWave;

            DataManager.Instance.GetLevelSettings.Init(out asteroidPrefabs, out destroyZoneCollider, out offsetDestroyZone, out zoneSpawn, out startWait);

            wavesCoroutine = StartCoroutine(SpawnWaves());

            var zoneDestroyObj = Instantiate(destroyZoneCollider,new Vector3(offsetDestroyZone.x, zoneDestroy.y), Quaternion.identity,transform) as BoxCollider;
            if (zoneDestroyObj != null) zoneDestroyObj.size = new Vector3(zoneDestroy.x * 100f, zoneDestroyObj.size.y, zoneDestroyObj.size.z * 100f);
        }

        public void StopWaves()
        {
            if (wavesCoroutine != null) StopCoroutine(wavesCoroutine);
        }
     
        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (int i = 0; i < countAsteroidsInWave; i++)
                {
                    SpawnAsteroid();
                    yield return new WaitForSeconds(spawnWait);
                }

                yield return new WaitForSeconds(waveWait);
            }
        }

        private void SpawnAsteroid()
        {
            GameObject asteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Count)];
            Vector3 spawnPosition = new Vector3(Random.Range(zoneSpawn.x, -1 * zoneSpawn.x) + offsetSpawn.x, zoneSpawn.y + offsetSpawn.y, zoneSpawn.z);
            Quaternion spawnRotation = Quaternion.identity;
            SpaceShooterGame.Instance.ObjectsPool.InstantiateFromPool(asteroid, spawnPosition, spawnRotation, PoolType.Asteroid);
        }
    }
}