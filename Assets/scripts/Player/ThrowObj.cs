using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThrowObj : MonoBehaviour
{
    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    public GameObject objToThrow;

    public int totalThrows;
    public float throwCooldown;
    public Thrower thrower;

    public KeyCode throwKey = KeyCode.Mouse1;
    private float throwForce;
    public float throwUpwardForce;

    [SerializeField] Slider massSlider;
    [SerializeField] Slider throwForceSlider;
    [SerializeField] Transform parent;

    // Line renderer for trajectory
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField]
    [Range(10, 200)] private int LinePoints = 150;  // Start with 150 points along the trajectory
    [SerializeField]
    [Range(0.01f, 0.2f)] private float TimeBetweenPoints = 0.05f;  // 0.05 seconds between each point
    [SerializeField] List<Rigidbody> celestialsRb = new();

    bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Draw trajectory
        DrawTrajectory();

        if (Input.GetKeyDown(throwKey) && ready && totalThrows > 0)
        {
            Throw();
        }
    }

    private void DrawTrajectory()
    {
        // Initialize LineRenderer positions
        LineRenderer.positionCount = LinePoints;

        // Initial projectile position (starting at attackPoint)
        Vector3 startPoint = attackPoint.position;

        // Get direction of throw based on camera
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 5000f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Combine forces (forward and upward forces) for initial velocity
        Vector3 initialVelocity = forceDirection * throwForceSlider.value + transform.up * throwUpwardForce;

        // Temporary variable to track current velocity (updated with gravity over time)
        Vector3 currentVelocity = initialVelocity;

        // Simulate the projectile's path and calculate points along the trajectory
        Vector3 currentPosition = startPoint;

        for (int i = 0; i < LinePoints; i++)
        {
            float time = i * TimeBetweenPoints;

            // Update position based on velocity and time
            currentPosition += currentVelocity * TimeBetweenPoints;

            // Calculate gravitational effect from celestial bodies
            Vector3 totalGravity = Vector3.zero;

            foreach(Rigidbody celestialRb in celestialsRb)
            {
                float m1 = massSlider.value;
                float m2 = celestialRb.mass;
                float distance = Vector3.Distance(currentPosition, celestialRb.transform.position);

                // Apply gravitational force similar to the first script
                Vector3 gravityForce = (celestialRb.transform.position - currentPosition).normalized * (G * (m1 * m2) / (distance * distance));
                totalGravity += gravityForce;
            }

            // Update velocity based on gravity
            currentVelocity += totalGravity * TimeBetweenPoints;

            // Optional: Stop trajectory if a collision is detected
            if (Physics.Raycast(currentPosition, currentVelocity.normalized, out RaycastHit hitInfo, TimeBetweenPoints))
            {
                LineRenderer.positionCount = i + 1;
                LineRenderer.SetPosition(i, hitInfo.point);
                break;
            }

            // Set the position in the LineRenderer
            LineRenderer.SetPosition(i, currentPosition);
        }
    }


    private void Throw()
    {
        ready = false;

        // Instantiate object to throw in mixed reality scene content
        GameObject projectile = Instantiate(objToThrow, attackPoint.position, cam.rotation, parent);

        // Get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Set mass equal to the slider value
        projectileRb.mass = massSlider.value;

        // Set throw force equal to the slider value
        throwForce = throwForceSlider.value;

        // Calculate direction (it's not shooting straight)
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        ready = true;
    }
}
