using UnityEngine;

public class WaypointSystem : MonoBehaviour
{
    [SerializeField] private Waypoint[] _waypoints;

    private int _index = 0;

    public Waypoint FirstWaypoint
    {
        get
        {
            return _waypoints[0];
        }
    }

    public Waypoint LastWaypoint
    {
        get
        {
            return _waypoints[_waypoints.Length - 1];
        }
    }
    
    public Waypoint NextWaypoint
    {
        get
        {
            _index++;
            
            if (_index == _waypoints.Length)
            {
                
            }

            return _waypoints[_index];
        }
    }
}