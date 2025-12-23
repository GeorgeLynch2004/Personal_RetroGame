using System;
using UnityEngine;

public class HealthBarHover : MonoBehaviour
{
    private Transform parent;
    private Vector3 position;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        parent = transform.parent;
    }

    private void FixedUpdate()
    {
        position = new Vector3(parent.position.x, parent.position.y , parent.position.z);
        transform.position = position + (offset * (parent.localScale.y/2));
    }
}
