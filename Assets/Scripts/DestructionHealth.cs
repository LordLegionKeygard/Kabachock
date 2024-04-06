using UnityEngine;

public class DestructionHealth : MonoBehaviour
{
    [SerializeField] ParticleSystem _ps;
    private CellFractureImpulse _cellFractureImpulse;
    private Collider _collider;
    private Reward _reward;
    private void Awake()
    {
        _cellFractureImpulse = GetComponent<CellFractureImpulse>();
        _collider = GetComponent<Collider>();
        _reward = GetComponent<Reward>();
    }

    public virtual void TakeDamage()
    {
        // _ps.Play();
        _collider.enabled = false;
        _cellFractureImpulse.PrepareObject();
        _reward.AddScore();
    }
}
