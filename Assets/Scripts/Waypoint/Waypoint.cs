
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public float sizeSphere = 0.5f;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, sizeSphere);
    }
}
