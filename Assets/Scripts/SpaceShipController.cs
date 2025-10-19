using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 5f; 
    public float rotationSpeed = 200f;  

    [Header("Input")]
    public InputActionReference thrust;
    public InputActionReference cw;
    public InputActionReference ccw;

    private Vector2 velocity;
    private float currentRotation;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        velocity = Vector2.zero;
        currentRotation = 0f;

        if (thrust != null) thrust.action.Enable();
        if (cw != null) cw.action.Enable();
        if (ccw != null) ccw.action.Enable();
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
        WrapAroundScreen();
    }

    void HandleRotation()
    {
        float rotationInput = 0f;

        if (cw != null && cw.action.IsPressed())
            rotationInput = -1f;
        else if (ccw != null && ccw.action.IsPressed())
            rotationInput = 1f;

        currentRotation += rotationInput * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }

    void HandleMovement()
    {
        Vector2 heading = transform.up;

        if (thrust != null && thrust.action.IsPressed())
        {
            velocity += heading * acceleration * Time.deltaTime;

            if (velocity.magnitude > maxSpeed)
            {
                velocity = velocity.normalized * maxSpeed;
            }
        }
        else
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, deceleration * Time.deltaTime);
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    void WrapAroundScreen()
    {
        if (cam == null) return;

        Vector3 viewportPos = cam.WorldToViewportPoint(transform.position);

        if (viewportPos.x < 0) viewportPos.x = 1;
        else if (viewportPos.x > 1) viewportPos.x = 0;

        if (viewportPos.y < 0) viewportPos.y = 1;
        else if (viewportPos.y > 1) viewportPos.y = 0;

        transform.position = cam.ViewportToWorldPoint(viewportPos);
    }

    void OnEnable()
    {
        if (thrust != null) thrust.action.Enable();
        if (cw != null) cw.action.Enable();
        if (ccw != null) ccw.action.Enable();
    }

    void OnDisable()
    {
        if (thrust != null) thrust.action.Disable();
        if (cw != null) cw.action.Disable();
        if (ccw != null) ccw.action.Disable();
    }
}