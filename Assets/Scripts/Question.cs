using UnityEngine;


[CreateAssetMenu(fileName = nameof(Question), menuName = "Configs/Question Config")]
public class Question : ScriptableObject
{
    #region Variables

    public string Answer;
    public Sprite Flag;

    #endregion
}