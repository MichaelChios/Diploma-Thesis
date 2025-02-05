using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] private DeviceServer phoneServer;
    [SerializeField] private string pressActionPath;
    private InputAction pressAction;

    // Button Region (screen-space coordinates)
    private readonly Rect buttonRegion = new Rect(950, 261, 480, 240);
    public Vector2 touchPosition;

    public Transform cam;
    public Transform attackPoint;
    bool ready;
    private GameObject projectile;
    private Rigidbody rb;

    [SerializeField] ActionsController actionsController;
    [SerializeField] ForceController forceController;

    // Start is called before the first frame update
    void Start()
    {
        pressAction = phoneServer.ActionAssetInstance.FindAction(pressActionPath);
    }

    void OnEnable()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        if(projectiles.Length > 0)
        {
            projectile = projectiles[projectiles.Length - 1];
        }
        rb = projectile.GetComponent<Rigidbody>();
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (actionsController.shootReady)
        {
            touchPosition = pressAction.ReadValue<Vector2>();
            if (ready && IsTouchInShootButtonRegion(touchPosition))
            {
                Shoot();
            }
        }
    }

    private bool IsTouchInShootButtonRegion(Vector2 touchPosition)
    {
        // Check if the touch position is within the button region
        return buttonRegion.Contains(touchPosition);
    }

    private void Shoot()
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
        Vector3 initialVelocity = forceDirection * forceController.force;
        var mass = rb.mass;
        projectile.GetComponent<IgnoreCollisionWithCelestials>().enabled = false;
        projectile.GetComponent<projectileGravity>().enabled = true;
        if (ready)
        {
            rb.AddForce(initialVelocity, ForceMode.Impulse);
        }
        actionsController.SetShootReady(false);
        actionsController.SetSpawnReady(true);
    }
}
