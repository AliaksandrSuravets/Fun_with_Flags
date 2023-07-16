using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Variables

    public Button[] AnswerButtons;
    public static int CorrectAnswers;
    public TMP_Text CorrectAnswersLabel;
    public Button ExitButton;
    public Image FlagImage;
    public Sprite[] Lives;
    public Image LivesImage;
    public static int LivesInt;
    public TMP_Text QuestionLabel;
    public List<Question> Questions = new List<Question>();
    public Button RemoveWrongAnswerButton;
    private int _correctAnswers;
    private string _currentCountry;
    private Question _currentQuestion;
    private bool _isStartCheck;
    private int _lives;
    private string _playerAnswer;
    private readonly List<int> _wrongAnswersIndex = new List<int>();

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        ExitButton.onClick.AddListener(SceneManagerHelper.LoadMenuScene);
        RemoveWrongAnswerButton.onClick.AddListener(RemoveWrongAnswer);
        StartGame();
    }

    private void Update()
    {
        if (!_isStartCheck)
        {
            return;
        }

        CheckAnswer(_playerAnswer);
        LivesInt = _lives;
        CorrectAnswers = _correctAnswers;
        if (_correctAnswers == 10 || _lives == 0)
        {
            Invoke("LoadEndScene", 2f);
        }
    }

    #endregion

    #region Private methods

    private void AskQuestion()
    {
        foreach (Button t in AnswerButtons)
        {
            t.gameObject.SetActive(true);
        }

        Shuffle(Questions);
        QuestionLabel.text = "What country does this flag belong to?";
        QuestionLabel.color = Color.white;
        if (Questions.Count > 0)
        {
            _currentQuestion = Questions[0];
            Questions.RemoveAt(0);
            _currentCountry = _currentQuestion.Answer;
            FlagImage.sprite = _currentQuestion.Flag;
            List<string> answers = new List<string>();
            for (int index = 0; index < AnswerButtons.Length - 1; index++)
            {
                Question Question = Questions[index];
                answers.Add(Question.Answer);
            }

            answers.Add(_currentCountry);
            Shuffle(answers);
            for (int i = 0; i < AnswerButtons.Length; i++)
            {
                if (answers[i] != _currentCountry)
                {
                    _wrongAnswersIndex.Add(i);
                }

                AnswerButtons[i].GetComponentInChildren<TMP_Text>().text = answers[i];
                AnswerButtons[i].onClick.AddListener(() => GetPlayerAnswer());
            }
        }
        else
        {
            SceneManagerHelper.LoadEndScene();
        }
    }

    private void CheckAnswer(string answer)
    {
        if (answer == _currentCountry)
        {
            _correctAnswers++;
            CorrectAnswersLabel.text = $"Correct answers {_correctAnswers}/10";
            QuestionLabel.text = "Correct";
            QuestionLabel.color = Color.green;
        }
        else
        {
            QuestionLabel.text = "Wrong";
            QuestionLabel.color = Color.red;
            _lives--;
            if (_lives > 0)
            {
                LivesImage.sprite = Lives[_lives - 1];
            }
        }

        foreach (Button t in AnswerButtons)
        {
            t.gameObject.SetActive(false);
        }

        if (_correctAnswers != 10 && _lives > 0)
        {
            Invoke("AskQuestion", 2f);
        }

        _isStartCheck = false;
    }

    private void GetPlayerAnswer()
    {
        _playerAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text;
        _isStartCheck = true;
        _wrongAnswersIndex.Clear();
    }

    private void LoadEndScene()
    {
        SceneManagerHelper.LoadEndScene();
    }

    private void RemoveWrongAnswer()
    {
        Shuffle(_wrongAnswersIndex);
        for (int i = 0; i < 2; i++)
        {
            AnswerButtons[_wrongAnswersIndex[i]].gameObject.SetActive(false);
        }

        RemoveWrongAnswerButton.gameObject.SetActive(false);
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T randomElement = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = randomElement;
        }
    }

    private void StartGame()
    {
        _lives = 3;
        LivesImage.sprite = Lives[_lives - 1];
        _correctAnswers = 0;
        _isStartCheck = false;
        Shuffle(Questions);
        AskQuestion();
    }

    #endregion
}