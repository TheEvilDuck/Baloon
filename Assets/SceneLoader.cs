using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void ReloderCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene((SceneId)currentSceneIndex);
    }

    public void LoadMainMenu() => LoadScene(SceneId.MainMenu);
    public void LoadGameplay() => LoadScene(SceneId.Gameplay);

    private void LoadScene(SceneId sceneId)
    {
        SceneManager.LoadScene((int)sceneId);
    }
}
