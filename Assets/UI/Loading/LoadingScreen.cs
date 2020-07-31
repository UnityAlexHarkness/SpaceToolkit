using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public void TransitionToMainMenu()
    {
        SceneManager.Instance.LoadNewScene("MainMenu");
    }
}
