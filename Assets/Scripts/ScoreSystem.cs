using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _currentScore;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        CustomEvents.OnAddScore += ChangeScore;
    }

    private void ChangeScore(int number)
    {
        _currentScore += number;
        _scoreText.text = _currentScore.ToString();
    }

    private void OnDestroy()
    {
        CustomEvents.OnAddScore -= ChangeScore;
    }
}
