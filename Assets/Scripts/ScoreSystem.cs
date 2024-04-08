using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _currentScore;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private AudioSource _takePoint;

    private void Awake()
    {
        CustomEvents.OnAddScore += ChangeScore;
    }

    private void ChangeScore(int number)
    {
        _currentScore += number;
        _scoreText.text = _currentScore.ToString();
        _takePoint.Play();
        _animator.SetTrigger(AnimatorStrings.AddScore);
    }

    private void OnDestroy()
    {
        CustomEvents.OnAddScore -= ChangeScore;
    }
}
