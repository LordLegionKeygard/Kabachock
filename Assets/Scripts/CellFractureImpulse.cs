using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFractureImpulse : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;

    [Header("Objects")]
    [SerializeField] private GameObject _mainObject;
    [SerializeField] private GameObject _allCells;
    [SerializeField] private Rigidbody[] _rbCells;
    private Rigidbody _rb;
    private bool _isDestroy = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void PrepareObject()
    {
        if (_isDestroy) return;
        _isDestroy = true;
        _rb.isKinematic = true;
        _mainObject.SetActive(false);
        _allCells.SetActive(true);

        CellImpulse();
    }

    private void CellImpulse()
    {
        foreach (var rb in _rbCells)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(Random.Range(_minForce, _maxForce), transform.position, Random.Range(_minRadius, _maxRadius));
        }
    }

}
