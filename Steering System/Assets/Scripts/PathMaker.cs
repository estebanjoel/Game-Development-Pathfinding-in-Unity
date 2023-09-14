using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker : MonoBehaviour
{
    [SerializeField] List<Transform> _waypoints;
    public List<Transform> waypoints{get{return _waypoints;}}
    [ContextMenu("Create Path")]
    void CreatePath()
    {
        _waypoints = new List<Transform>();
        _waypoints.AddRange(GetComponentsInChildren<Transform>());
        _waypoints.Remove(this.transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(_waypoints != null && _waypoints.Count > 0)
        {
            for(int i = 1; i < _waypoints.Count; i++)
            {
                Gizmos.DrawLine(_waypoints[i-1].position, _waypoints[i].position);
            }
            Gizmos.DrawLine(_waypoints[_waypoints.Count-1].position, _waypoints[0].position);
        }
    }
}
