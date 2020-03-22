using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenGameOver : GUIScreen
    {

        [SerializeField] private Button restartButton;

        private void Awake()
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            GameManager.Instance.Restart();
        }
    }
}

