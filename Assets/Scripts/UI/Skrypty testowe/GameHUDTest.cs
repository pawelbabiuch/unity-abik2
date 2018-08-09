using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHUDTest : MonoBehaviour {

    public static GameHUDTest inst { get; private set; }

    private void Awake()
    {
        inst = this;
    }

    private void TestInfo()
    {
        Debug.LogWarning("Launched one of the GameHUdTest method script");
    }

    public void ResetLevel()
    {
        TestInfo();
        Time.timeScale = 1;
        string sceneName = SceneManager.GetSceneAt(1).name;
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
