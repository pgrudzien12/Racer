using UnityEngine;

public class RacerController : MonoBehaviour
{
    private const float seeAngle = 30.0f;
    WsadCarController controller;
    public float carLength = 0.5f;
    // Use this for initialization
    void Start()
    {
        controller = new WsadCarController(this.gameObject, carLength);
    }

    void FixedUpdate()
    {
        controller.FixedUpdate();

        Vector3 end1 = transform.position + DriveDirection;
        Vector3 end2 = transform.position + LeftAngleDireciton;
        Vector3 end3 = transform.position + RightAngleDireciton;

        var forwardHit = Physics2D.Raycast(transform.position, DriveDirection);
        var leftHit = Physics2D.Raycast(transform.position, LeftAngleDireciton);
        var rightHit = Physics2D.Raycast(transform.position, RightAngleDireciton);

        Debug.DrawLine(transform.position, forwardHit.point, Color.green);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.green);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("colision 2d with " + coll.gameObject.name);
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
}
