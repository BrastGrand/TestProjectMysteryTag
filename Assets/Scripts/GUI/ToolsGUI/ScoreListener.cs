using TMPro;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class ScoreListener : MonoBehaviour, IScoreListener
    {
        [SerializeField] TextMeshProUGUI scoreText;

        SmoothInt64Changer smoothIntChanger = new SmoothInt64Changer();

        public void Init()
        {
            smoothIntChanger.Init(DataManager.Instance.Score);
            if (scoreText != null)
                scoreText.text = StringParser.GetMoneyStringFormat(smoothIntChanger.CurValue);

            DataManager.Instance.AddScoreListener(this);
        }

        public void OnDestroy()
        {
            DataManager.Instance.RemoveScoreListener(this);
        }

        public void OnScoreChange(int newScore, int oldScore)
        {
            smoothIntChanger.SetNewValue(newScore);
        }

        public void UpdateScore()
        {
            if (smoothIntChanger.UpdateValue())
                if (scoreText != null) scoreText.text = StringParser.GetMoneyStringFormat(smoothIntChanger.CurValue);
        }

    }
}

