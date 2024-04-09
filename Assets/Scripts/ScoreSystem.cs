using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int currentScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource takePoint;

    private void Awake()
    {
        CustomEvents.OnAddScore += ChangeScore;
    }

    private void ChangeScore(int number)
    {
        currentScore += number;
        scoreText.text = currentScore.ToString();
        takePoint.Play();
        animator.SetTrigger(AnimatorStrings.AddScore);
    }

    private void OnDestroy()
    {
        CustomEvents.OnAddScore -= ChangeScore;
    }
}
