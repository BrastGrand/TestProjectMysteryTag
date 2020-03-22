using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestProjectForMysteryTag
{
    public class SelectLevelButton : MonoBehaviour
    {
        [SerializeField] private Button loadLevelButton;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI parametrLevelText;
        [SerializeField] private Image closeImage;
        private int currentIndex;

        private void Start()
        {
            loadLevelButton.onClick.RemoveAllListeners();
            loadLevelButton.onClick.AddListener(LoadLevel);
        }

        private void LoadLevel()
        {
            GameManager.Instance.ActivateMission(currentIndex);
            GameManager.Instance.LoadGame();

        }

        public void Init(int indexMission, int needDestroy, bool isOpen)
        {
            currentIndex = indexMission;
            levelText.text = $"LEVEL {currentIndex + 1}";
            parametrLevelText.text = $"DESTROY {needDestroy}\nASTEROID ";
            closeImage.gameObject.SetActive(!isOpen);
            loadLevelButton.interactable = isOpen;
        }
    }
}