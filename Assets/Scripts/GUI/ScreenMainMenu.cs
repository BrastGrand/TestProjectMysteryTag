using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenMainMenu : GUIScreen
    {
        [SerializeField] private Button startGameButton;

        private void Awake()
        {
            startGameButton.onClick.RemoveAllListeners();
            startGameButton.onClick.AddListener(OpenSelectLevel);
        }

        private void OpenSelectLevel()
        {
            GUIController.Instance.ShowScreen<ScreenSelectLevel>(true);
        }
    }
}
