using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public Vector3 RotationAxis = Vector3.up;
    public float RotationSpeed = 20;

    
    private void Start()
    {
    }

    void Update()
    {
       
        Rotate();
    }

    void Rotate()
    {
        float s = RotationSpeed * Time.deltaTime;
        transform.Rotate(RotationAxis.x * s, RotationAxis.y * s, RotationAxis.z * s, Space.Self);
    }
}

