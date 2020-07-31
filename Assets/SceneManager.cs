using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager SceneManagerInstance;

    private string currentSceneName ="Loading";
    private string newSceneName;

    public static SceneManager Instance
    {
        get { return SceneManagerInstance; }
    }

    public void OnEnable()
    {
        SceneManagerInstance = this;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Camera.main.gameObject);

        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
        UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneUnloaded;
    }

    public void LoadNewScene(string sceneName)
    {
        newSceneName = sceneName;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Void", LoadSceneMode.Additive);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Void")
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(currentSceneName);
        }

        if (scene.name == newSceneName)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Void");
            currentSceneName = newSceneName;
            newSceneName = "";
        }
    }

    private void SceneUnloaded(Scene scene)
    {
        if (scene.name == currentSceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(newSceneName, LoadSceneMode.Additive);
        }
    }
}
