using UnityEngine.SceneManagement;

public static class SceneManagerHelper
{
    #region Variables

    public const string End = "EndGameScene";
    public const string Game = "GameScene";
    public const string Menu = "MainMenuScene";

    #endregion

    #region Public methods

    public static void LoadEndScene() => SceneManager.LoadScene(End); 
    public static void LoadGameScene() => SceneManager.LoadScene(Game); 
    public static void LoadMenuScene() => SceneManager.LoadScene(Menu);
    
    public static void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion
}