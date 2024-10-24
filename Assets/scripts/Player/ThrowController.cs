using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThrowController : MonoBehaviour
{
    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    public float throwCooldown;

    public KeyCode throwKey = KeyCode.Mouse1;
    
    [SerializeField] Slider massSlider;
    [SerializeField] Slider throwForceSlider;
    [SerializeField] List<Rigidbody> celestialsRb = new();

    [SerializeField] Thrower thrower;
    public BaseTrajectoryRenderer trajectRenderer;

    bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Initial projectile position (starting at attackPoint)
        Vector3 startPoint = attackPoint.position;
        // Get direction of throw based on camera
        Vector3 forceDirection = cam.transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }
        // Combine forces (forward and upward forces) for initial velocity
        Vector3 initialVelocity = forceDirection * throwForceSlider.value;
        var mass = massSlider.value;

        // Draw trajectory
        trajectRenderer.Draw(startPoint, initialVelocity, mass, celestialsRb);

        if (Input.GetKeyDown(throwKey) && ready)
        {
            thrower.ThrowWithCooldown(initialVelocity, mass, throwCooldown, startPoint: attackPoint);
        }
    }
}
