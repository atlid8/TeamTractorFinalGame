using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform target;
    public float initialForwardAngle = 0; // initial angle of your "gun barrel"
    public float maxRotationSpeed = 60;
    public float threshold = 4;

    private float angleToTarget; // Destination angle
    private float _currentAngle = 0; // Current angle
    private float signToTarget;

    void Start() {
        if (!target){
            target = GameObject.Find("Player").transform;
        }
    }
    void Update() {
        RotateGradually2D ();
    }
    public void RotateGradually2D()
    {
        angleToTarget = Mathf.Atan2 (target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        signToTarget = Mathf.Sign (angleToTarget - _currentAngle);
        if (Mathf.Abs(angleToTarget - _currentAngle) > threshold) {
            _currentAngle += signToTarget * maxRotationSpeed * Time.deltaTime;
        } else {
            _currentAngle = angleToTarget;
        }
        transform.eulerAngles = new Vector3(0, 0, _currentAngle - initialForwardAngle);
    }
}
