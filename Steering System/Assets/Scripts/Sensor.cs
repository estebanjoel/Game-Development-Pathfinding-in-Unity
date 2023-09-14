using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    const float _sensorLength = 2.5f;
    const float _frontSensorStartingPoint = 1;
    const float _frontSideSendorStartingPoint = 0.5f;
    const float _frontSensorAngle = 25f;
    public float Check()
    {
        Vector3 frontPosition;
        RaycastHit hit;
        float avoidDirection = 0;
        frontPosition = transform.position + (transform.forward * _frontSensorStartingPoint);
        if(DrawSensors(frontPosition, Vector3.forward, _sensorLength * 2, out hit))
        {
            if(hit.normal.x < -0.1) avoidDirection = -0.25f;
            else if(hit.normal.x > 0.1) avoidDirection = 0.25f;
            else avoidDirection = 0;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        avoidDirection -= FrontSideSensors(frontPosition, out hit, 1);
        avoidDirection -= FrontSideSensors(frontPosition, out hit, -1);
        return avoidDirection;
    }

    bool DrawSensors(Vector3 sensorPosition, Vector3 direction, float length, out RaycastHit hit)
    {
        if(Physics.Raycast(sensorPosition, direction, out hit, length))
        {
            if(hit.transform.gameObject.tag == "Obstacle")
            {
                Debug.DrawLine(transform.position, hit.point, Color.black);
                return true;
            }
            return false;
        }
        return false;
    }

    float FrontSideSensors(Vector3 frontPosition, out RaycastHit hit, float sensorDirection)
    {
        float avoidDirection = 0;
        Vector3 sensorPosition = frontPosition + transform.position + (transform.right * _frontSideSendorStartingPoint * sensorDirection);
        Vector3 sensorAngle = Quaternion.AngleAxis(_frontSensorAngle * sensorDirection, transform.up) * transform.forward;
        if(Physics.Raycast(frontPosition, transform.forward, out hit, _sensorLength))
        {
            avoidDirection = 1;
            Debug.DrawLine(transform.position, hit.point, Color.black);
        }
        if(Physics.Raycast(frontPosition, sensorAngle, out hit, _sensorLength))
        {
            avoidDirection = 0.5f;
            Debug.DrawLine(transform.position, hit.point, Color.black);
        }
        return avoidDirection;
    }
}
