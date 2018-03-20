using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WsadCarController
{
    private float accelerateForce = 0;
    private float speed = 0;
    public float maxTurnAngle = 30.0f;
    public float maxSpeed = .5f;
    public float carMass = 10.0f;
    public float resistaanceForceMultiplier = .3f;
    public float breakMultiplierForce = 4.0f;
    private float carLength;

    private GameObject car;

    public WsadCarController(GameObject car, float length)
    {
        this.car = car;
        this.carLength = length;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        var deltaTime = Time.deltaTime;
        float accelerateForce = AccelerateForce();
        
        var acceleration = accelerateForce / carMass;
        speed += acceleration * deltaTime;
        var resistance = (resistaanceForceMultiplier / carMass) * deltaTime;

        speed += ResistanceSign(speed) * resistance;
        speed = Mathf.Min(speed, maxSpeed);
        //speed = Mathf.Max(speed, 0);
        if (Mathf.Approximately(0, speed))
            speed = 0;

        var alpha = Input.GetAxis("Horizontal") * maxTurnAngle;
        var qTurn = Quaternion.AngleAxis(alpha, Vector3.back);

        var carVector = DriveDirection * carLength;
        var speedVector = qTurn * DriveDirection * speed;

        // calculate turn
        var faceVector = carVector + speedVector;
        var carTurn = Vector3.SignedAngle(DriveDirection.normalized, faceVector.normalized, Vector3.back);

        car.transform.position = car.transform.position + speedVector;
        car.transform.Rotate(Vector3.back, carTurn);

        //Debug.Log("carTurn = " + carTurn + " acceleration = " + acceleration + " accelerateForce = " + accelerateForce + " speed = " + speed);
    }

    private float ResistanceSign(float speed)
    {
        if (Mathf.Approximately(0, speed))
            return 0;
        return speed > 0 ? -1 : 1;
    }

    private float AccelerateForce()
    {
        var accelerateForce = Input.GetAxis("Vertical");
        if (accelerateForce * speed < 0)
            accelerateForce *= breakMultiplierForce;

        return accelerateForce;
    }

    private Vector3 DriveDirection
    {
        get { return car.transform.up; }
    }
}
