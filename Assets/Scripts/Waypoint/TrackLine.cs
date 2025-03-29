 
using System.Collections.Generic; 
using UnityEngine;

public class TrackLine : MonoBehaviour
{
    public List<Waypoint> waypoints = new List<Waypoint>();

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Count == 0) return;

        Gizmos.color = Color.green;

       for(int i = 0; i < waypoints.Count; i++)
       {
            Gizmos.DrawLine(waypoints[i].transform.position, waypoints[(i + 1) % waypoints.Count].transform.position);
       }
    }
}
