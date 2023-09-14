using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    [SerializeField] private  float _speed;
    [SerializeField] private float _avoidSpeed = 200f;
    [SerializeField] Transform _target;
    [SerializeField] PathMaker _pathMaker;
    Rigidbody _rb;
    Sensor _sensor;
    List<Transform> _path {get{return _pathMaker.waypoints;}}
    const float _distanceToChangeWaypoint = 5f;
    int _currentWaypoint;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _sensor = GetComponent<Sensor>();
    }

    
    void FixedUpdate()
    {
        float avoid = _sensor.Check();
        if(avoid == 0) StandardSteer();
        else AvoidSteer(avoid);
        Move();
        CheckWaypoint();
    }

    private void AvoidSteer(float avoidDir)
    {
        transform.RotateAround(transform.position, transform.up, _avoidSpeed * Time.fixedDeltaTime * avoidDir);
    }

    private void StandardSteer()
    {
        Vector3 targetDirection = _path[_currentWaypoint].position - _rb.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.fixedDeltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void CheckWaypoint()
    {
        if(Vector3.Distance(_rb.position, _path[_currentWaypoint].position) < _distanceToChangeWaypoint)
        {
            _currentWaypoint++;
            if(_currentWaypoint == _path.Count) _currentWaypoint = 0;
        }
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + (transform.forward * _speed * Time.deltaTime));
    }
}
