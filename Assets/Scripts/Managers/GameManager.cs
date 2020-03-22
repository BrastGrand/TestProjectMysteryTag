using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestProjectForMysteryTag
{
    public class GameManager : Singleton<GameManager>
    {
        private bool isDebug = true;
        public bool IsDebug => isDebug;

        public StateGameEnum CurrentStateGame { get; set; } = StateGameEnum.Menu;
        public Mission CurrentMission => currentMission;

        public int CurrentIndexMission => currentIndexMission;

        private Mission currentMission = null;
        private int currentIndexMission = 0;

        void Awake()
        {
            Debug.Log("GameManager awake");
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            Debug.Log("Game manager start!");

            StartCoroutine(DefferGameStart());
        }

        IEnumerator DefferGameStart()
        {
            yield return new WaitForEndOfFrame();

            if (CurrentStateGame == StateGameEnum.Menu)
            {
                GUIController.Instance.ShowScreen<ScreenMainMenu>();
            }
            else
            {
                if (CurrentMission == null)
                {
                    ActivateMission(currentIndexMission);
                }

                IGame mainGameObject = FindObjectsOfType<MonoBehaviour>().OfType<IGame>().FirstOrDefault();
                if (mainGameObject != null)
                    mainGameObject.StartGame();
                else
                {
                    Debug.LogError("IGame object not found in scene! Game didn't launch..");
                }
            }
        }

        public void Init(bool isDebug, StateGameEnum gameState, int indexMission)
        {
            this.isDebug = isDebug;
            CurrentStateGame = gameState;
            currentIndexMission = indexMission;

            if (!Debug.isDebugBuild)
            {
                CurrentStateGame = StateGameEnum.Menu;
                this.isDebug = false;
                currentIndexMission = 0;
            }
        }

        #region Activators from loading scene
        public void RestoreMenu()
        {
            SceneManager.sceneLoaded += OnSceneLoadedMenu;
        }

        private void OnSceneLoadedMenu(Scene curScene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoadedMenu");
            SceneManager.sceneLoaded -= OnSceneLoadedMenu;
            StartCoroutine(DefferRestoreMenu());
        }

        private IEnumerator DefferRestoreMenu()
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("Menu restored");
            GUIController.Instance.ShowScreen<ScreenMainMenu>();
        }

        public void ActivateSimpleGameOnScene()
        {
            SceneManager.sceneLoaded += OnSceneLoadedGame;
        }

        void OnSceneLoadedGame(Scene curScene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoadedGame;
            StartCoroutine(DefferActivateGameOnSceneGame());
        }

        IEnumerator DefferActivateGameOnSceneGame()
        {
            yield return new WaitForEndOfFrame();
            IGame mainGameObject = FindObjectsOfType<MonoBehaviour>().OfType<IGame>().FirstOrDefault();
            if (mainGameObject != null)
                mainGameObject.StartGame();
            else
            {
                Debug.LogError("IGame object not found in scene! Game didn't launch..");
            }
        }
        #endregion
        public void ClearCurrentMainGame()
        {
            CurrentStateGame = StateGameEnum.Menu;
            currentMission = null;
            currentIndexMission = 0;
        }

        public void ActivateMission(int indexMission)
        {
            CurrentStateGame = StateGameEnum.Game;
            currentMission = DataManager.Instance.MissionsContainer.GetMission(indexMission);
            currentIndexMission = indexMission;
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("loading");
        }

        public void LoadMenu()
        {
            ClearCurrentMainGame();
            SceneManager.LoadScene("loading");
        }


        public void Restart()
        {
            LoadGame();
        }

        public void LevelComplete()
        {
            currentIndexMission++;
            if (DataManager.Instance.CountCompleteLevel <= currentIndexMission)
            {
                DataManager.Instance.SetCountCompleteLevel(currentIndexMission);               
                if (currentIndexMission < DataManager.Instance.MissionsContainer.GetMissionsCount)
                {
                    ActivateMission(currentIndexMission);
                }
                else
                {
                    ClearCurrentMainGame();
                }
            }
        }
    }

    public enum StateGameEnum
    {
        Menu,
        Game,
    }
}
