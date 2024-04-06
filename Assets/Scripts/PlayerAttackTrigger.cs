using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out DestructionHealth destructionHealth))
        {
            destructionHealth.TakeDamage();
        }
    }
}
