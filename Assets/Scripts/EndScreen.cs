using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    #region Variables

    public TMP_Text EndLabel;
    public Button ExitButton;

    #endregion

    #region Unity lifecycle

    // Start is called before the first frame update
    private void Start()
    {
        ExitButton.onClick.AddListener(SceneManagerHelper.LoadMenuScene);

        if (GameManager.LivesInt == 0)
        {
            EndLabel.text = "GAME OVER";
        }
        else
        {
            EndLabel.text = $"Congratulations. You are the winner. Correct answers - {GameManager.CorrectAnswers}. Not correct - {3 - GameManager.LivesInt}.";
        }
    }

    #endregion
}