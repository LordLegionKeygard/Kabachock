using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] claws;
    [SerializeField] private AudioSource[] bites;
    [SerializeField] private AudioSource jump;

    [Header("Footsteps")]
    [SerializeField] private AudioSource[] otherFootsteps;
    [SerializeField] private AudioSource[] grassFootsteps;
    [SerializeField] private Transform rayPoint;
    [SerializeField] private LayerMask layerMask;
    private float rayDistance = 0.5f;
    private RaycastHit _hit;

    public void ClawsSound()
    {
        var rnd = Random.Range(0, claws.Length);

        claws[rnd].Play();
    }

    public void BiteSound()
    {
        var rnd = Random.Range(0, bites.Length);

        bites[rnd].Play();
    }

    public void JumpSound() => jump.Play();

    public void FootStepSound()
    {
        var rnd = Random.Range(0, 4);
        if (Physics.Raycast(rayPoint.position, Vector3.down, out _hit, rayDistance, layerMask))
        {
            switch (_hit.collider.tag)
            {
                case WorldGameInfo.Other:
                    otherFootsteps[rnd].Play();
                    break;
                case WorldGameInfo.GrassTag:
                    grassFootsteps[rnd].Play();
                    break;
            }
        }
    }
}
