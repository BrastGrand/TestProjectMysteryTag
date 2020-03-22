using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenGame : GUIScreen
    {
        [Header("ScreenGame parametr")]
        [SerializeField] private ScoreListener scoreListener;
        [SerializeField] private TextMeshProUGUI completeText;
        [SerializeField] private PanelLivePlayer panelLifePlayer;
        [SerializeField] private GameObject lifePrefabs;
        [SerializeField] private Button menuButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private void Awake()
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(LoadMenu);
        }

        private void LoadMenu()
        {
            GameManager.Instance.LoadMenu();
        }

        private void Start()
        {
            scoreListener.Init();
        }

        protected override void OnShow()
        {
            Debug.Log("OnShow ScreenGame");
            panelLifePlayer.Init(SpaceShooterGame.Instance.CountLive, lifePrefabs);
            SpaceShooterGame.Instance.KillEnemyAction += UpdateCompleteInfo;
            UpdateCompleteInfo();
            StartCoroutine(DisplayLevelText());
        }

        private IEnumerator DisplayLevelText()
        {
            levelText.text = string.Format("LEVEL {0}", GameManager.Instance.CurrentIndexMission + 1);
            levelText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            levelText.gameObject.SetActive(false);
        }

        private void UpdateCompleteInfo()
        {
            completeText.text = String.Format("{0}/{1}", SpaceShooterGame.Instance.CountDestroyAsteroid, SpaceShooterGame.Instance.NeedDestroyAsteroid);
        }

        protected override void OnHide()
        {
            Debug.Log("OnHide ScreenGame");
            SpaceShooterGame.Instance.KillEnemyAction -= UpdateCompleteInfo;
        }

        void Update()
        {
            scoreListener.UpdateScore();

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                LoadMenu();
            }
        }

    }
}
