using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 heading;
    public float speed = 5f;
    public Vector2 velocity;
    public Camera cam;
    public InputActionReference thrust;
    public float acceleration = 6.0f;
    public InputActionReference cw;
    public InputActionReference ccw;
    public float rotationspeed = 10.0f;

    void Start()
    {
        heading = Vector2.up;
        velocity = Vector2.zero;

        if (thrust != null)
        {
            thrust.action.Enable();
        }
        if (cw != null)
        {
            cw.action.Enable();
        }
        if (ccw != null)
        {
            ccw.action.Enable();
        }
    }

    void Update()
    {
        bool rotatecw = (cw && cw.action.IsPressed());
        bool rotateccw = (ccw && ccw.action.IsPressed());   
        int sign = (rotatecw ? -1 : 1);
        float dt = Time.deltaTime;

        if(rotatecw || rotateccw)
        {
            float angle = sign * rotationspeed * dt;
            Quaternion zRot = Quaternion.AngleAxis(angle, Vector3.forward);
            heading = zRot * heading;
            heading.Normalize();
            transform.rotation = Quaternion.LookRotation(Vector3.forward, heading);

        }

        if (thrust && thrust.action.IsPressed())
        {
            velocity += heading * acceleration * dt;
        }

        transform.position += (Vector3)(velocity * speed * dt);

        CorrectCameraPosition();
    }

    void CorrectCameraPosition()
    {
        if (cam != null)
        {
            Vector3 min = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
            Vector3 max = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (transform.position.x < min.x)
                transform.position = new Vector3(max.x, transform.position.y, transform.position.z);

            if (transform.position.x > max.x)
                transform.position = new Vector3(min.x, transform.position.y, transform.position.z);

            if (transform.position.y < min.y)
                transform.position = new Vector3(transform.position.x, max.y, transform.position.z);

            if (transform.position.y > max.y)
                transform.position = new Vector3(transform.position.x, min.y, transform.position.z);
        }
    }
    public void OnEnable()
    {
        if (thrust)
        {
            thrust.action.Enable();
        }
        if (cw)
        {
            cw.action.Enable();
        }
        if (ccw)
        {
            ccw.action.Enable();
        }
    }
    public void OnDisable()
    {
        if (thrust)
        {
            thrust.action.Disable();
        }
        if (cw)
        {
            cw.action.Disable();
        }
        if (ccw)
        {
            ccw.action.Disable();
        }
    }
}