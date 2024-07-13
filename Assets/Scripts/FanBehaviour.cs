using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2.0f;
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed);
    }
}
