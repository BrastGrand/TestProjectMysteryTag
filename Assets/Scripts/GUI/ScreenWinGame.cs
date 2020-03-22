using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class ScreenWinGame : GUIScreen
    {
        [SerializeField] private Button nextLevelButton;

        private void Start()
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(NextLevel);
        }

        private void NextLevel()
        {
          GameManager.Instance.LoadGame();
        }
    }
}