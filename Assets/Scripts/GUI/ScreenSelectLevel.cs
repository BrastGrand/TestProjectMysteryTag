using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenSelectLevel : GUIScreen
    {
        [SerializeField] private SelectLevelButton levelButtonPrefab;
        [SerializeField] private Transform contentTransform;
        [SerializeField] private Button backButton;

        private readonly List<SelectLevelButton> selectLevelButtons = new List<SelectLevelButton>();

        private void Start()
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(Back);
            CreateLevel();
        }

        private void Back()
        {
            GUIController.Instance.HideScreen<ScreenSelectLevel>();
            GUIController.Instance.ShowScreen<ScreenMainMenu>();
        }
        

        protected override void OnShow()
        {
            UpdateLevelInformation();
        }

        private void CreateLevel()
        {
            if (selectLevelButtons.Count == 0)
            {
                for (var i = 0; i < DataManager.Instance.MissionsContainer.GetMissionsCount; i++)
                {
                    Mission currentMission = DataManager.Instance.MissionsContainer.GetMission(i);
                    if (currentMission == null) continue;
                    SelectLevelButton levelButton = Instantiate(levelButtonPrefab, contentTransform);
                    levelButton.gameObject.transform.localScale = Vector3.one;
                    bool isOpenLevel = i <= DataManager.Instance.CountCompleteLevel;
                    levelButton.Init(i, currentMission.countNeedKillEnemy, isOpenLevel);
                    selectLevelButtons.Add(levelButton);
                }
            }
        }

        private void UpdateLevelInformation()
        {
            if(selectLevelButtons.Count == 0) CreateLevel();
            else
            {
                for (int i = 0; i < DataManager.Instance.MissionsContainer.GetMissionsCount; i++)
                {
                    Mission currentMission = DataManager.Instance.MissionsContainer.GetMission(i);
                    if (currentMission == null || i > selectLevelButtons.Count - 1) continue;
                    bool isOpenLevel = i <= DataManager.Instance.CountCompleteLevel;
                    selectLevelButtons[i].Init(i, currentMission.countNeedKillEnemy, isOpenLevel);
                }
            }
        }

        protected override void OnHide()
        {
        }
    }
}
