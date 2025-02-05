using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawnedTrajectory : MonoBehaviour
{
    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    [SerializeField] List<Rigidbody> celestialsRb = new();
    public BaseTrajectoryRenderer trajectRenderer;
    public ForceController forceController;
    private GameObject projectile;
    private Rigidbody rb;
    float throwForce;

    private void OnEnable()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        if(projectiles.Length > 0)
        {
            projectile = projectiles[projectiles.Length - 1];
            rb = projectile.GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Initial projectile position (starting at attackPoint)
        Vector3 startPoint = attackPoint.position;
        // Get direction of throw based on camera
        Vector3 forceDirection = cam.transform.forward;
        // Force value
        throwForce = forceController.force;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }
        // Combine forces (forward and upward forces) for initial velocity
        Vector3 initialVelocity = forceDirection * throwForce;

        var mass = rb.mass;

        // Draw trajectory
        trajectRenderer.Draw(startPoint, initialVelocity, mass, celestialsRb);
    }
}
