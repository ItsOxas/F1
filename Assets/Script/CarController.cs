using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float throttle;
    public float steer;
    public float brake;

    public List<WheelCollider>steeringWheels;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> breakingWheels;
    public Rigidbody rb;
    

    public float torque = 690000f;
    public float steerAngle = 20f;
    public float brakesTorque = 9000f;
    public float maxSpeed = 360;
    public float speed;

    private void Start()
    {
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        speed = rb.velocity.magnitude * 3.6f;
        throttle = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
        brake = Input.GetAxis("Brake");
        

        foreach (WheelCollider wheel in throttleWheels) 
        {
            if (speed > maxSpeed)
            {
                wheel.motorTorque = 0;
            }
            else
            {
                var motor = wheel.motorTorque = torque * Time.deltaTime * throttle * 60;
                rb.AddForce(transform.forward * motor);
            }
        }
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.GetComponent<WheelCollider>().steerAngle = 20f * steer;
            wheel.transform.localEulerAngles = new Vector3(0, steer * steerAngle,0);
        }

        

        foreach (WheelCollider wheel in breakingWheels)
        {
            if (speed >= 20)
            {
                var motor = wheel.brakeTorque = brakesTorque * Time.deltaTime * brake * 60;
                rb.AddForce(-transform.forward * motor);
            }
            else
            {
                var motor = wheel.brakeTorque = brakesTorque * Time.deltaTime * brake * 60;
            }
        }
        
    }
}
