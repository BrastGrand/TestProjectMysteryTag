using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class DataManager : Singleton<DataManager>
    {
        private string profileName;
        private bool clearProfileOnStart;
        [SerializeField]  private GameData data = new GameData();
        private bool dataDirty = false;

        private DefaultProfile defaultProfile;
        private MissionsContainer missionsContainer;
        private LevelSettings levelSettingsContainer;

        public MissionsContainer MissionsContainer => missionsContainer;
        public LevelSettings GetLevelSettings => levelSettingsContainer;
        private readonly List<IScoreListener> scoreListeners = new List<IScoreListener>();

        public void SetSoundValue(float soundValue, bool autoSave = true)
        {
            data.SoundValue = soundValue;
            if(autoSave)
                Save();
        }

        public float SoundValue
        {
            get { return data.SoundValue; }
        }

        private string FilePath
        {
            get { return Path.Combine(Application.persistentDataPath, profileName + ".json"); }
        }

        public GameData GetCurrentData
        {
            get { return data; } 
        }

        void Awake()
        {
            Debug.Log("DataManager awake");
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            Debug.Log("DataManager start");
            if (clearProfileOnStart)
            {
                Clear();
            }
            else
            {
               Load();
            }
        }

        public void Init(string profileName, bool clearProfileOnStart, DefaultProfile defaultProfile, MissionsContainer missionsContainer, LevelSettings levelSettingsContainer)
        {
            this.profileName = profileName;
            this.clearProfileOnStart = clearProfileOnStart;
            this.defaultProfile = defaultProfile;
            this.missionsContainer = missionsContainer;
            this.levelSettingsContainer = levelSettingsContainer;
            if (!Debug.isDebugBuild)
                this.clearProfileOnStart = false;

            Debug.Log("Profile path: " + FilePath);
        }


        public void Clear()
        {
            Debug.Log("CLEAR");
            data = defaultProfile != null ? defaultProfile.profileData : new GameData();

            Save();

            if (File.Exists(FilePath))
            {
                Load();
            }
            else
            {
                Debug.LogError("Profile not saved! Check file system!");
                data = new GameData();
            }

            SetSoundValue(data.SoundValue, false);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            Debug.Log("SAVE");
            File.WriteAllText(FilePath, JsonUtility.ToJson(data, false));
        }

        void Load()
        {
            if (File.Exists(FilePath))
            {
                Debug.Log("LOAD");
                data = JsonUtility.FromJson<GameData>(File.ReadAllText(FilePath));

                UpdateRuntimeByLoadedData();
            }
            else
            {
                Clear();
            }
        }

        private void SetDataDirty()
        {
            if (dataDirty == false)
            {
                dataDirty = true;
                Invoke("DefferSave", 1.0f);
            }
        }

        void DefferSave()
        {
            Save();
            dataDirty = false;
        }

        void UpdateRuntimeByLoadedData()
        {                
            SetSoundValue(data.SoundValue, false);
        }


        public int Score => data.PlayerData.Score;

        public void AddScore(int score)
        {
            int lastScore = data.PlayerData.Score;
            data.PlayerData.Score += Mathf.Max(score, 0);
            
            if (lastScore != data.PlayerData.Score && scoreListeners.Count > 0)
            {
                scoreListeners.ForEach(curListener => curListener.OnScoreChange(data.PlayerData.Score, lastScore));
            }

            SetDataDirty();
        }

        public void AddScoreListener(IScoreListener listener)
        {
            if (!scoreListeners.Contains(listener))
                scoreListeners.Add(listener);
        }

        public void RemoveScoreListener(IScoreListener listener)
        {
            scoreListeners.Remove(listener);
        }

        public int CountLive => PlayerData.DEFAULT_LIVE;

        public int CountCompleteLevel => data.PlayerData.CountCompleteLevel;

        public void SetCountCompleteLevel(int index)
        {
            data.PlayerData.CountCompleteLevel = index;
            SetDataDirty();
        }

    }
}
