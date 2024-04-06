using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColliderToggle : MonoBehaviour
{
    private Collider _damageCollider;

    private void Awake()
    {
        _damageCollider = GetComponent<Collider>();
        CustomEvents.OnToggleDamageCollider += ColliderToggle;
    }

    private void ColliderToggle(bool state)
    {
        _damageCollider.enabled = state;
    }

    private void OnDestroy()
    {
        CustomEvents.OnToggleDamageCollider -= ColliderToggle;
    }
}
