using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    public float rotateSpeed;
    public float horizontalInput;

    void Update() {

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotateSpeed * horizontalInput * Time.deltaTime);
    }


}
