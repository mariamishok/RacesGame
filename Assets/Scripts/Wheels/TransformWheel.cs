
using UnityEngine;

public class TransformWheel : MonoBehaviour
{
    private Transform wheelTransform;

    public WheelsData wheelsData;

    private void Awake()
    {
        wheelTransform = GetComponent<Transform>();
    }
    private void Start()
    {
        wheelsData.wheelTransform = wheelTransform;
    }
}
