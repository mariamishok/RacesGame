 
using UnityEngine;

public class ColliderWheel : MonoBehaviour
{
    private WheelCollider wheelCollider;
    public WheelsData wheelsData;

    private void Awake()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }
    private void Start()
    {
        wheelsData.wheelCollider = wheelCollider;
    }
}
