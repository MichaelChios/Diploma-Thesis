using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererTrajectory: BaseTrajectoryRenderer
{
    readonly float G = 0.0001f;
    private LineRenderer lineRenderer;

    [SerializeField]
    [Range(10, 200)] private int linePoints = 150;
    [SerializeField]
    [Range(0.01f, 0.2f)] private float betweenPoints = 0.05f;  // 0.05 seconds between each point

    private void Awake() => lineRenderer = GetComponent<LineRenderer>();
    public override void Draw(Vector3 startPoint, Vector3 force, float mass, List<Rigidbody> celestials)
    {
        // Initialize LineRenderer positions
        lineRenderer.positionCount = linePoints;

        // Temporary variable to track current velocity (updated with gravity over time)
        Vector3 currentVelocity = force;

        // Simulate the projectile's path and calculate points along the trajectory
        Vector3 currentPosition = startPoint;

        for (int i = 0; i < linePoints; i++)
        {
            float time = i * betweenPoints;

            // Update position based on velocity and time
            currentPosition += currentVelocity * betweenPoints;

            // Calculate gravitational effect from celestial bodies
            Vector3 totalGravity = Vector3.zero;

            foreach (Rigidbody celestialRb in celestials)
            {
                float m2 = celestialRb.mass;
                float distance = Vector3.Distance(currentPosition, celestialRb.transform.position);

                // Apply gravitational force similar to the first script
                Vector3 gravityForce = (celestialRb.transform.position - currentPosition).normalized * (G * (mass * m2) / (distance * distance));
                totalGravity += gravityForce;
            }

            // Update velocity based on gravity
            currentVelocity += totalGravity * betweenPoints;

            // Optional: Stop trajectory if a collision is detected
            if (Physics.Raycast(currentPosition, currentVelocity.normalized, out RaycastHit hitInfo, 1.25f * betweenPoints))
            {
                lineRenderer.positionCount = i + 1;
                lineRenderer.SetPosition(i, hitInfo.point);
                Debug.Log("CRAASHED");
                break;
            }

            // Set the position in the LineRenderer
            lineRenderer.SetPosition(i, currentPosition);
        }
    }
}
