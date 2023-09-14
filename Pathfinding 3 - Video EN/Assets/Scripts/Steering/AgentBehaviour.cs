using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    const float distanceToChangeWaypoint = 5;
    const float avoidanceSpeed = 200;
    public float Speed = 5;
    public Transform Target;
    public PathMaker PathMaker;
    List<Transform> path {get{return PathMaker.Waypoints;}}
    int currentWaypoint = 0;
    Rigidbody rb;
    Sensor sensor;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        sensor = GetComponent<Sensor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float avoidance = sensor.Check();
        if(avoidance==0){
            StandardSteer();
        }else{
            AvoidSteer(avoidance);
        }
        Move();
        CheckWaypoint();
        
    }
    void Move(){
        rb.MovePosition(rb.position+ (transform.forward*Time.deltaTime*Speed));
    }
    void StandardSteer(){
        Vector3 targetDirection = path[currentWaypoint].position-rb.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.fixedDeltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    void AvoidSteer(float avoidance){
        transform.RotateAround(rb.position, transform.up, avoidanceSpeed*avoidance*Time.fixedDeltaTime);
    }
    void CheckWaypoint(){
        if(Vector3.Distance(rb.position, path[currentWaypoint].position)<distanceToChangeWaypoint){
            currentWaypoint++;
            if(currentWaypoint==path.Count)
                currentWaypoint = 0;
        }
    }
}
