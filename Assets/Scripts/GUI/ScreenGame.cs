using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenGame : GUIScreen
    {
        [Header("ScreenGame parameters")]
        [SerializeField] private ScoreListener scoreListener;
        [SerializeField] private TextMeshProUGUI completeText;
        [SerializeField] private PanelLivePlayer panelLifePlayer;
        [SerializeField] private GameObject lifePrefabs;
        [SerializeField] private Button menuButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private void LoadMenu()
        {
            GameManager.Instance.LoadMenu();
        }

        private void Start()
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(LoadMenu);
            scoreListener.Init();
        }

        protected override void OnShow()
        {
            panelLifePlayer.Init(SpaceShooterGame.Instance.CountLive, lifePrefabs);
            SpaceShooterGame.Instance.KillEnemyAction += UpdateCompleteInfo;
            UpdateCompleteInfo();
            StartCoroutine(DisplayLevelText());
        }

        private IEnumerator DisplayLevelText()
        {
            levelText.text = $"LEVEL {GameManager.Instance.CurrentIndexMission + 1}";
            levelText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            levelText.gameObject.SetActive(false);
        }

        private void UpdateCompleteInfo()
        {
            completeText.text = $"{SpaceShooterGame.Instance.CountDestroyAsteroid}/{SpaceShooterGame.Instance.NeedDestroyAsteroid}";
        }

        protected override void OnHide()
        {
            SpaceShooterGame.Instance.KillEnemyAction -= UpdateCompleteInfo;
        }
    }
}
