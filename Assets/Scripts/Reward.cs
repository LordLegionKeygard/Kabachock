using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private Eggs egg;

    public void AddScore()
    {
        CustomEvents.FireAddScore(egg.Score);
    }
}
