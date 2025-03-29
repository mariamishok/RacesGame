
using Unity.VisualScripting;
using UnityEngine;

public class MoveOnTrack : MonoBehaviour
{
    public TrackLine trackLine;
    private Autopilot carAI;
     
    private Transform transformCar;
    public float thresholdDistance;
    private int nextPoint;

    private void Awake()
    {
        carAI = GetComponent<Autopilot>();
        transformCar = GetComponent<Transform>();
    }
    private void Start()
    {
        FindStartPoint();
    }
    private void LateUpdate()
    {
        FollowNextPoint();
    }
    private void FindStartPoint() 
    {
        float minDistance = float.MaxValue;
        int startPoint = -1;
        for (int i = 0; i < trackLine.waypoints.Count; i++)
        {
            float distance = Vector3.Distance(trackLine.waypoints[i].transform.position, transformCar.position);
            if(distance < minDistance)
            {
                startPoint = i;
                minDistance = distance;
            }
        }
        nextPoint = startPoint;
    }
    private void  FollowNextPoint()
    {
        Vector3 point = trackLine.waypoints[nextPoint].transform.position;
        Vector3 targetPoint = new Vector3(point.x, transformCar.position.y, point.z); 
         Vector3 positionToTarget  = transformCar.InverseTransformPoint(targetPoint);
         
        float distanceToTarget = positionToTarget.magnitude;
        if(distanceToTarget <= thresholdDistance)
        {
            nextPoint = (nextPoint + 1) % trackLine.waypoints.Count;
        } 
        carAI.turning = positionToTarget.x / distanceToTarget;
        carAI.moving = 1f;
    }
}
