using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAStar : MonoBehaviour
{
    #region Fields
    public const float DistanceToChangeWaypoint = 2f;
    public AStar aStar;
    public float Speed = 2f;
    public Vector2Int CurrentPosition;
    public Transform Objective;
    bool _followingPath;
    List<TileLogic> _path;
    Rigidbody _rb;
    int _currentWaypoint;
    #endregion
    #region Unity Events
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();    
    }

    void FixedUpdate()
    {
        if(!_followingPath) return;
        Move();
        CheckWaypoint();
    }
    #endregion

    #region Methods
    public void FollowPath()
    {
        CancelInvoke();
        InvokeRepeating("BuildPath", 0, 2);
    }
    void BuildPath()
    {
        TileLogic currentTile = Board.Instance.WorldPositionToTile(_rb.position);
        TileLogic targetTile = Board.Instance.WorldPositionToTile(Objective.position);
        _currentWaypoint = 0;
        aStar.Search(currentTile, targetTile);
        _path = aStar.BuildPath(targetTile);
        _followingPath = true;
    }
    void Move()
    {
        Vector3 targetDirection = _path[_currentWaypoint].WorldPosition - _rb.position;
        _rb.MovePosition(_rb.position + (targetDirection * Time.fixedDeltaTime * Speed));
    }

    void CheckWaypoint()
    {
        if(Vector3.Distance(_rb.position, _path[_currentWaypoint].WorldPosition) < DistanceToChangeWaypoint)
        {
            CurrentPosition = _path[_currentWaypoint].Position;
            _currentWaypoint++;
            if(_currentWaypoint == _path.Count)
            {
                _followingPath = false;
            }
        }
    }
    #endregion
    
}
