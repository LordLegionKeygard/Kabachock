using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFractureImpulse : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float minForce;
    [SerializeField] private float maxForce;
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;

    [Header("Objects")]
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject allCells;
    [SerializeField] private Rigidbody[] rbCells;
    private Rigidbody rb;
    private bool isDestroy = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void PrepareObject()
    {
        if (isDestroy) return;
        isDestroy = true;
        rb.isKinematic = true;
        mainObject.SetActive(false);
        allCells.SetActive(true);

        CellImpulse();
    }

    private void CellImpulse()
    {
        foreach (var rb in rbCells)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, Random.Range(minRadius, maxRadius));
        }
    }

}
