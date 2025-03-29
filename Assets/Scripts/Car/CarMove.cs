using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] private List<WheelsData> wheelsData; 

    [SerializeField] private float moveForce = 2000f;
    [SerializeField] private float brakeForce = 2500f;
    [SerializeField] private float maxAngleWheel = 45f; 
    [SerializeField] private float maxSpeed = 90f;

    private float currentSpeed = 0f;
    private float acceleration = 0f;
    private Vector3 axisInput;

    private void LateUpdate()
    {
        axisInput = InputKey();
        CheckSpeed();

        // �������������� ������ ������ ��������� � ��������, �������� ������ ��������� � �������� ������ ��� 4 ������� 
        foreach (WheelsData wheel in wheelsData)
        {
            UpdateTransformWheels(wheel.wheelTransform, wheel.wheelCollider);
        }
        
    }
    private void FixedUpdate()
    {
        MoveCar(axisInput);
        TurnsCar(axisInput);
        BrakeCar(); 
    } 

    private void CheckSpeed()
    {
        byte second = 60;
        byte radius = 2;
        short meters = 1000;
        foreach(WheelsData wheel in wheelsData)
        {
            currentSpeed = ((radius * Mathf.PI * wheel.wheelCollider.radius) * (wheel.wheelCollider.rpm * second)) / meters;
        }
    }
    private Vector3 InputKey()
    {
        // �������� ������������ �������� ��� ������� �� ������ W,A,S,D
        float vertical = Input.GetAxis("Vertical"); // key down - W,A  = float �� -1f  �� 1f
        float horizontal = Input.GetAxis("Horizontal");// key down - S,D  = float �� -1f  �� 1f
        return new Vector3(horizontal, 0, vertical);
    }
    
    private void TurnsCar(Vector3 axis)
    {
        // ������� ����� 
        foreach (WheelsData wheel in wheelsData)
        {
            switch (wheel.wheelType)
            {
                case WheelType.LeftFront:
                case WheelType.RightFront:
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, maxAngleWheel * axis.x, 0.5f);
                    break;

            }
        }
    }

    private void BrakeCar()
    {
        // ����������
        foreach (WheelsData wheel in wheelsData)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                wheel.wheelCollider.brakeTorque = brakeForce;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    private void MoveCar(Vector3 axis)
    {
        // ��������� ������ � �����
        foreach (WheelsData wheel in wheelsData)
        {
            if (Mathf.RoundToInt(currentSpeed) < maxSpeed)
            {
                acceleration +=  Time.deltaTime;
                acceleration = Mathf.Clamp(acceleration, 0f, 1f);
                wheel.wheelCollider.motorTorque = axis.z * (moveForce * acceleration);
            } 
            else wheel.wheelCollider.motorTorque = 0;
        }
    } 
    private void UpdateTransformWheels(Transform transform, WheelCollider collider)
    {
        collider.GetWorldPose(out Vector3 pos, out Quaternion rot); // �������� ������� GetWorldPose
        // ������� ����� 2 ��������� � ����������� out - ��� ������� ��� � ��� ��� �� ����� �������� ��� ������ ��������� ����������
        // ������� �� ����������������� ���, � ����������� �� ������� ������� � �������� 
        transform.position = pos; // �������������� ������ ������ ��� ������� 
        transform.rotation = rot; // �������������� ������ ������ ��� ������� 
    }
}
