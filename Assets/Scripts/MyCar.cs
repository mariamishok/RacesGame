using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCar : MonoBehaviour
{
    public Transform wheelTransFL;
    public Transform wheelTransFR;
    public Transform wheelTransBL;
    public Transform wheelTransBR;
    public WheelCollider wheelForwardL;
    public WheelCollider wheelForwardR;
    public WheelCollider wheelBackL;
    public WheelCollider wheelBackR;
    public float moveForce = 2000;
    public float brakeForce = 2000;
    public float maxAngleWheel = 45;
    public float currentSpeed = 0;
    public float acceleration = 0;
    public float maxSpeed = 200;

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
        if (Mathf.RoundToInt(acceleration) < maxSpeed)
        {
            acceleration += Time.deltaTime;
            currentSpeed = Mathf.Clamp(acceleration, 0, 1);
            wheelBackL.motorTorque = input.z * moveForce * currentSpeed;
            wheelBackR.motorTorque = input.z * moveForce * currentSpeed;
            wheelForwardL.motorTorque = input.z * moveForce * currentSpeed;
            wheelForwardR.motorTorque = input.z * moveForce * currentSpeed;
        }
        else
        {
            wheelBackL.motorTorque = 0;
            wheelBackR.motorTorque = 0;
            wheelForwardL.motorTorque = 0;
            wheelForwardR.motorTorque = 0;
        }
    } 
    private void WheelBrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            wheelForwardL.brakeTorque = brakeForce;
            wheelForwardR.brakeTorque = brakeForce;
            wheelBackL.brakeTorque = brakeForce;
            wheelBackR.brakeTorque = brakeForce;
        }
        else
        {
            wheelForwardL.brakeTorque = 0;
            wheelForwardR.brakeTorque = 0;
            wheelBackL.brakeTorque = 0;
            wheelBackR.brakeTorque = 0;
        }
    }
    private void WheelTurn(Vector3 input)
    {
        wheelForwardL.steerAngle = Mathf.Lerp(wheelForwardL.steerAngle, input.x * maxAngleWheel, 0.5f);
        wheelForwardR.steerAngle = Mathf.Lerp(wheelForwardR.steerAngle, input.x * maxAngleWheel, 0.5f);
    }
    private void UpdateTransformWheels(WheelCollider collider, Transform transform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        transform.position = position;
        transform.rotation = rotation;
    }
    
}
