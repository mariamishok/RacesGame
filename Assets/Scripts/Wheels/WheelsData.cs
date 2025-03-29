 
using UnityEngine;


[CreateAssetMenu(fileName = "NewWheel", menuName = "ColliderWheel")]
public class WheelsData : ScriptableObject
{
    public WheelType wheelType;
    public WheelCollider wheelCollider;
    public Transform wheelTransform;
}
public enum WheelType
{
    LeftFront,
    RightFront,
    LeftBack,
    RightBack
}
