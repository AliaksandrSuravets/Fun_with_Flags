using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    #region Variables

    public Button ExitButton;
    public Button StartButton;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        StartButton.onClick.AddListener(SceneManagerHelper.LoadGameScene);
        ExitButton.onClick.AddListener(SceneManagerHelper.OnExitButtonClicked);
    }

    #endregion
}