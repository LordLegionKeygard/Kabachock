using UnityEngine;

public class PlayerAttackColliderToggle : MonoBehaviour
{
    private Collider damageCollider;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        CustomEvents.OnToggleDamageCollider += ColliderToggle;
    }

    private void ColliderToggle(bool state)
    {
        damageCollider.enabled = state;
    }

    private void OnDestroy()
    {
        CustomEvents.OnToggleDamageCollider -= ColliderToggle;
    }
}
