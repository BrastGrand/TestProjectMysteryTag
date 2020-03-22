using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    [CreateAssetMenu(fileName = "MissionsContainer", menuName = "Data/MissionsContainer")]
    public class MissionsContainer : ScriptableObject
    {
        [SerializeField] private List<Mission> missionsList;

        public int GetMissionsCount => missionsList.Count;

        public Mission GetMission(int index)
        {
            if (index < 0 || index > missionsList.Count - 1)
            {
                Debug.LogError("Mission not found");
                return null;
            }
            return missionsList[index];
        }

#if UNITY_EDITOR
        [ContextMenu("GenerateMission")]
        public void GenerateMission()
        {
            missionsList.Clear();
            int countKill = 10;
            for (int i = 0; i < 100; i++)
            {
                missionsList.Add(new Mission($"Mission {i + 1}", countKill, 0.75f, UnityEngine.Random.Range(30,  Mathf.Clamp(30 + i, 5, 100))));
                countKill += UnityEngine.Random.Range(2, 10);
            }
        }
#endif
    }

    [Serializable]
    public class Mission
    {
#if UNITY_EDITOR
        public string nameMissionEditor;

        public Mission(string nameMissionEditor, int countNeedKillEnemy, float spawnWait, int countAsteroidsInWave)
        {
            this.nameMissionEditor = nameMissionEditor;
            this.countNeedKillEnemy = countNeedKillEnemy;
            this.spawnWait = spawnWait;
            this.countAsteroidsInWave = countAsteroidsInWave;
        }
#endif
        public int countNeedKillEnemy;
        public float spawnWait;
        public int countAsteroidsInWave;

    }
}