using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Rotate the object around its local X,YiZ axis at X degree per second
    public float xSpeed = 0f;
    public float ySpeed = 0f;
    public float zSpeed = 0f;
    private int degreePerSecond = 10;


    private void Update()
    {
        // Rotate the object around its local X,YiZ axis at X degree per second
        transform.Rotate(new Vector3(xSpeed, ySpeed, zSpeed) * degreePerSecond * Time.deltaTime);
    }
}
