using System;

namespace TestProjectForMysteryTag
{
    [Serializable]
    public class PlayerData
    {
        public int Score;
        public int RecordScore;
        public int CountCompleteLevel;
        public const int DEFAULT_LIVE = 3;
       
        PlayerData()
        {
            Score = 0;
            RecordScore = 0;
            CountCompleteLevel = 0;
        }
    }

    [Serializable]
    public class GameData
    {
        public string Version;
        public float SoundValue;
        public PlayerData PlayerData;

        public GameData()
        {
            Version = "1.0";
        }
    }
}