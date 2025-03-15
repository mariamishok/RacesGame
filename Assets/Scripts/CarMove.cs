using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public WheelCollider wheelForwardL;
    public WheelCollider wheelForwardR;
    public WheelCollider wheelBackL;
    public WheelCollider wheelBackR;
    public float moveForce = 2000;
    public float brakeForce = 2000;
    public float maxAngleWheel = 45;
    public Transform wheelTransFL;
    public Transform wheelTransFR;
    public Transform wheelTransBL;
    public Transform wheelTransBR;

    private void LateUpdate()
    {
        UpdateTransformWheels(wheelForwardL, wheelTransFL);
        UpdateTransformWheels(wheelForwardR, wheelTransFR);
        UpdateTransformWheels(wheelBackL, wheelTransBL);
        UpdateTransformWheels(wheelBackR, wheelTransBR);
    }
    private void FixedUpdate()
    {
        Vector3 input = InputAxis();
        CarEngin(input);
        WheelTurn(input);
        WheelBrake();

    }
    private Vector3 InputAxis()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new Vector3(x, 0, z);
    }
    private void CarEngin(Vector3 input)
    {
        wheelBackL.motorTorque = input.z * moveForce;
        wheelBackR.motorTorque = input.z * moveForce; 
    }
    private void WheelBrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            wheelForwardL.brakeTorque = brakeForce;
            wheelForwardR.brakeTorque = brakeForce;
        }
        else
        {
            wheelForwardL.brakeTorque = 0;
            wheelForwardR.brakeTorque = 0;
        }
    }
    private void WheelTurn(Vector3 input)
    {
        wheelForwardL.steerAngle = input.x * maxAngleWheel;
        wheelForwardR.steerAngle = input.x * maxAngleWheel;
    }
    private void UpdateTransformWheels(WheelCollider collider, Transform transform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        transform.position = position;
        transform.rotation = rotation;
    }
    
}
