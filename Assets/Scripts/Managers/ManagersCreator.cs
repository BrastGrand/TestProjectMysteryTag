using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class ManagersCreator : MonoBehaviour
    {
        [Header("Game managers settings")]
        [SerializeField]
        private bool isDebug = true;
        [SerializeField]
        private StateGameEnum curStateGame = StateGameEnum.Menu;
        [SerializeField]
        private int indexMission = 0;

        [Header("Data managers settings")]
        [SerializeField]
        private string profileName = "MainProfile";

        [SerializeField]
        private bool clearProfile = false;

        [SerializeField]
        private DefaultProfile defaultProfile = null;
        [SerializeField]
        private MissionsContainer missionsContainer = null;

        [SerializeField]
        private LevelSettings levelSettingsContainer = null;

        void Awake()
        {
            Debug.Log("ManagersCreator awake");
            if (!GameManager.IsAwake)
            {
                DataManager.Instance.Init(profileName, clearProfile, defaultProfile, missionsContainer, levelSettingsContainer);
                GameManager.Instance.Init(isDebug, curStateGame, indexMission);
            }
        }
    }
}
