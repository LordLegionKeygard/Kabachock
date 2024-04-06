using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private Eggs _eggs;

    public void AddScore()
    {
        CustomEvents.FireAddScore(_eggs.Score);
    }
}
