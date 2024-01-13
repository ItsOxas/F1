using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float throttle;
    public float steer;

    public List<WheelCollider>steeringWheels;
    public List<WheelCollider> throttleWheels;
    public Rigidbody rb;

    public float torque = 690000f;
    public float steerAngle = 20f;

    private void Start()
    {
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");

        foreach (WheelCollider wheel in throttleWheels) 
        {
            wheel.motorTorque = torque * Time.deltaTime * throttle;
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = 20f * steer;
            wheel.transform.localEulerAngles = new Vector3(0, steer * steerAngle,0);
        }
    }
}
