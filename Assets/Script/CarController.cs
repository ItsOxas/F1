using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{


    public List<WheelCollider>steeringWheels;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> brakingWheels;
    public List<GameObject> wheelMesh;
    public Rigidbody rb;
    public InputManager im;
    public GameObject centerOfmass;
    

    public float torque = 690000f;
    public float steerAngle = 20f;
    public float brakesTorque = 9000f;
    public float maxSpeed = 360;
    public float speed;
    public float radius = 6f;
    public float downForce = 50;
    

    private void Start()
    {
            rb = GetComponent<Rigidbody>();
            im = GetComponent<InputManager>();
            rb.centerOfMass = centerOfmass.transform.localPosition;
    }

    void FixedUpdate()
    {
        speed = rb.velocity.magnitude * 3.6f;   
        

        foreach (WheelCollider wheel in throttleWheels) 
        {
            if (speed > maxSpeed)
            {
                wheel.motorTorque = 0;
            }
            else
            {
                var motor = wheel.motorTorque = torque * im.throttle;
                rb.AddForce(transform.forward * motor);
            }
        }
        Steering();

        foreach (WheelCollider wheel in brakingWheels)
        {
                var motor = wheel.brakeTorque = brakesTorque * im.brake;
        }
        downforceFun();
        wheelAnimations();


        
    }
    private void Steering()
    {
        if (im.steer > 0)
        {
            steeringWheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * im.steer;
            steeringWheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * im.steer;
        }
        else if (im.steer < 0)
        {
            steeringWheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * im.steer;
            steeringWheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * im.steer;
        }
        else
        {
            steeringWheels[0].steerAngle = 0;
            steeringWheels[1].steerAngle = 0;
        }
    }

    private void downforceFun()
    {
        rb.AddForce(-transform.up * downForce * rb.velocity.magnitude);
    }

    private void wheelAnimations()
    {
        foreach (GameObject mesh in wheelMesh)
        {
            mesh.transform.Rotate(mesh.transform.right * rb.velocity.magnitude / (2 * Mathf.PI * 0.32f));
        }
    }
}
