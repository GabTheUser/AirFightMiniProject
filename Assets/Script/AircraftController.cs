using UnityEngine;

public class AircraftController : MonoBehaviour
{
    // Input sensitivity for pitch and roll
    public float pitchSensitivity = 0.5f;
    public float rollSensitivity = 0.5f;

    // Rotation limits
    public float maxPitchAngle = 45f;
    public float maxRollAngle = 45f;

    // Current pitch and roll angles
    private float pitchAngle = 0f;
    private float rollAngle = 0f;

    // Force applied for lift and thrust
    public float liftForce = 10f;
    public float thrustForce = 20f;

    // Ceiling altitude to prevent infinite ascent
    public float ceilingAltitude = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Read input from WASD keys
        float pitchInput = Input.GetAxis("Vertical") * pitchSensitivity;
        float rollInput = Input.GetAxis("Horizontal") * rollSensitivity;

        // Calculate new pitch and roll angles based on input
        pitchAngle += pitchInput;
        rollAngle += rollInput;

        // Clamp pitch and roll angles within limits
        pitchAngle = Mathf.Clamp(pitchAngle, -maxPitchAngle, maxPitchAngle);
        rollAngle = Mathf.Clamp(rollAngle, -maxRollAngle, maxRollAngle);

        // Apply rotation to the aircraft
        transform.rotation = Quaternion.Euler(pitchAngle, 0f, -rollAngle);

        // Calculate current altitude of the aircraft
        float altitude = transform.position.y;

        // If the altitude exceeds the ceiling altitude, stop applying lift force
        if (altitude < ceilingAltitude)
        {
            // Apply lift force upward
            Vector3 lift = transform.up * liftForce;
            rb.AddForce(lift, ForceMode.Force);

            // Calculate forward direction based on rotation
            Vector3 forwardDirection = transform.forward;

            // Set the velocity based on the forward direction and speed
            rb.velocity = forwardDirection * thrustForce;
        }
        else
        {
            // If at or above the ceiling altitude, maintain current velocity
            rb.velocity = rb.velocity.magnitude * transform.forward;
        }
    }
}
