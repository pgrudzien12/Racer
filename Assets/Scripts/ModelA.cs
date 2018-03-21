using System;
using UnityEngine;

public class ModelA : MonoBehaviour
{
    private IInput input;
    private const float seeAngle = 30.0f;
    private float accelerateForce = 0;
    private float speed = 0;

    public float carLength = 0.5f;
    public float maxTurnAngle = 30.0f;
    public float maxSpeed = .5f;
    public float carMass = 10.0f;
    public float resistaanceForce = .3f;
    public float braekForceMultiplier = 4.0f;

    // Use this for initialization
    void Start()
    {
        input = new WsadInput();
    }

    void FixedUpdate()
    {
        Vector3 end1 = transform.position + DriveDirection;
        Vector3 end2 = transform.position + LeftAngleDireciton;
        Vector3 end3 = transform.position + RightAngleDireciton;

        var forwardHit = Physics2D.Raycast(transform.position, DriveDirection);
        var leftHit = Physics2D.Raycast(transform.position, LeftAngleDireciton);
        var rightHit = Physics2D.Raycast(transform.position, RightAngleDireciton);

        Debug.DrawLine(transform.position, forwardHit.point, Color.green);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.green);

        Debug.Log(forwardHit.distance);

        UpdatePhysics();
    }

    private void UpdatePhysics()
    {
        var deltaTime = Time.deltaTime;
        float accelerateForce = AccelerateForce();

        var acceleration = accelerateForce / carMass;
        speed += acceleration * deltaTime;
        var resistance = (resistaanceForce / carMass) * deltaTime;

        speed += ResistanceSign(speed) * resistance;
        speed = Mathf.Min(speed, maxSpeed);
        //speed = Mathf.Max(speed, 0);
        if (Mathf.Approximately(0, speed))
            speed = 0;

        var alpha = input.Horizontal * maxTurnAngle;
        var qTurn = Quaternion.AngleAxis(alpha, Vector3.back);

        var carVector = DriveDirection * carLength;
        var speedVector = qTurn * DriveDirection * speed;

        // calculate turn
        var faceVector = carVector + speedVector;
        var carTurn = Vector3.SignedAngle(DriveDirection.normalized, faceVector.normalized, Vector3.back);

        transform.position = transform.position + speedVector;
        transform.Rotate(Vector3.back, carTurn);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        speed /= 2.0f;
    }

    private Vector3 DriveDirection
    {
        get { return transform.up; }
    }

    private Vector3 LeftAngleDireciton
    {
        get { return Quaternion.AngleAxis(seeAngle, Vector3.forward) * DriveDirection; }
    }

    private Vector3 RightAngleDireciton
    {
        get { return Quaternion.AngleAxis(-seeAngle, Vector3.forward) * DriveDirection; }
    }

    private float ResistanceSign(float speed)
    {
        if (Mathf.Approximately(0, speed))
            return 0;
        return speed > 0 ? -1 : 1;
    }

    private float AccelerateForce()
    {
        var accelerateForce = input.Vertical;
        if (accelerateForce * speed < 0)
            accelerateForce *= braekForceMultiplier;

        return accelerateForce;
    }
}
