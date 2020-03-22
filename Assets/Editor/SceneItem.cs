using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneItem : UnityEditor.Editor
{
    [MenuItem("Open Scene/Menu")]
    public static void OpenLoadMenu()
    {
        OpenScene("menu");
    }

    [MenuItem("Open Scene/Loading")]
    public static void OpenLoadLoading()
    {
        OpenScene("loading");
    }

    [MenuItem("Open Scene/Game")]
    public static void OpenLoadGame()
    {
        OpenScene("game");
    }

    static void OpenScene(string name)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene("Assets/Scenes/" + name + ".unity");
        }
    }
}
