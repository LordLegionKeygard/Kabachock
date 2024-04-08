using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] _claws;
    [SerializeField] private AudioSource[] _bites;
    [SerializeField] private AudioSource _jump;

    [Header("Footsteps")]
    [SerializeField] private AudioSource[] _otherFootsteps;
    [SerializeField] private AudioSource[] _grassFootsteps;
    [SerializeField] private Transform _rayPoint;
    [SerializeField] private LayerMask _layerMask;
    private float _rayDistance = 0.5f;
    private RaycastHit _hit;

    public void ClawsSound()
    {
        var rnd = Random.Range(0, _claws.Length);

        _claws[rnd].Play();
    }

    public void BiteSound()
    {
        var rnd = Random.Range(0, _bites.Length);

        _bites[rnd].Play();
    }

    public void JumpSound() => _jump.Play();

    public void FootStepSound()
    {
        var rnd = Random.Range(0, 4);
        if (Physics.Raycast(_rayPoint.position, Vector3.down, out _hit, _rayDistance, _layerMask))
        {
            switch (_hit.collider.tag)
            {
                case WorldGameInfo.Other:
                    _otherFootsteps[rnd].Play();
                    break;
                case WorldGameInfo.GrassTag:
                    _grassFootsteps[rnd].Play();
                    break;
            }
        }
    }
}
