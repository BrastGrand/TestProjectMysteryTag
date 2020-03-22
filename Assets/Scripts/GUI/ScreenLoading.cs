using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestProjectForMysteryTag
{
    public class ScreenLoading : GUIScreen
    {
        private const string MenuSceneName = "menu";
        private const string GameSceneName = "game";

    
        private AsyncOperation asyncLoad = null;

        void Start()
        {
            Debug.Log("LoadingScene start");
            StartCoroutine(LoadGame(GameManager.Instance.CurrentStateGame));
        }

        IEnumerator LoadGame(StateGameEnum stateGame)
        {
            yield return null;
            string sceneName = "";
            if (stateGame == StateGameEnum.Menu)
            {
                sceneName = MenuSceneName;
            }
            else
            {
                sceneName = GameSceneName;
            }

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("Loading scene is empty! Stopped.");
                yield break;
            }

            asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            yield return asyncLoad.isDone;

            Debug.Log("LoadingScene: finish loading");      
            asyncLoad = null;

            if (sceneName == MenuSceneName)
                GameManager.Instance.RestoreMenu();
            else
                GameManager.Instance.ActivateSimpleGameOnScene();
        }

     
    }
}

