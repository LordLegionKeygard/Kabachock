using UnityEngine;

public class DestructionHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] private AudioSource[] audioSource;
    private CellFractureImpulse cellFractureImpulse;
    private Collider eggCollider;
    private Reward reward;
    private void Awake()
    {
        cellFractureImpulse = GetComponent<CellFractureImpulse>();
        eggCollider = GetComponent<Collider>();
        reward = GetComponent<Reward>();
    }

    public virtual void TakeDamage()
    {
        audioSource[Random.Range(0,audioSource.Length)].Play();
        particle.Play();
        eggCollider.enabled = false;
        cellFractureImpulse.PrepareObject();
        reward.AddScore();
    }
}
