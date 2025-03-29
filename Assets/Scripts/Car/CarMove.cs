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

        // инициализируем каждое колесо трансформ и колайдер, передаем каждый трансформ и колайдер колеса все 4 клолеса 
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
        // получаем напвравлени€ движени€ при нажатии на кнопки W,A,S,D
        float vertical = Input.GetAxis("Vertical"); // key down - W,A  = float от -1f  до 1f
        float horizontal = Input.GetAxis("Horizontal");// key down - S,D  = float от -1f  до 1f
        return new Vector3(horizontal, 0, vertical);
    }
    
    private void TurnsCar(Vector3 axis)
    {
        // поворот колес 
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
        // торможение
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
        // движкение вперед и назад
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
        collider.GetWorldPose(out Vector3 pos, out Quaternion rot); // вызываем функцию GetWorldPose
        // котора€ имеет 2 аргумента с опператором out - это говорит нам о том что мы можем передать ему пустые локальные переменные
        // которые он проинициализируем сам, в резуальтате мы получим позицию и вращение 
        transform.position = pos; // инициализируем каждое колесо его позицию 
        transform.rotation = rot; // инициализируем каждое колесо его поворот 
    }
}
