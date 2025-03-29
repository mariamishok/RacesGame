using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreOfMas : MonoBehaviour
{
    public Transform CentreMas;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.centerOfMass = Vector3.Scale(CentreMas.localPosition, CentreMas.localScale);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rb.worldCenterOfMass, 1);
    }
}
