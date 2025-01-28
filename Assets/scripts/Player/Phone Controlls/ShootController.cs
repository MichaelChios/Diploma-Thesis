using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] private DeviceServer phoneServer;
    [SerializeField] private string pressActionPath;

    private InputAction pressAction;
    public float force = 10f; // Current force value
    private bool isIncreasing = true; // Determines if the force is increasing or decreasing
    private float step = 10f; // Step value for increasing/decreasing force

    // Button Region (screen-space coordinates)
    private readonly Rect buttonRegion = new Rect(691, 341, 851, 421);
    public Vector2 touchPosition;

    private Coroutine adjustForceCor;

    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    bool ready;
    private GameObject projectile;
    private Rigidbody rb;

    [SerializeField] ActionsController actionsController;

    void Start()
    {
        pressAction = phoneServer.ActionAssetInstance.FindAction(pressActionPath);
        pressAction.started += _ => AdjustStart();
        pressAction.canceled += _ => AdjustEnd();
        projectile = GameObject.FindGameObjectWithTag("Projectile");
        rb = projectile.GetComponent<Rigidbody>();
        ready = true;
    }

    private void AdjustStart()
    {
        if (adjustForceCor == null && actionsController.shootReady)
        {
            adjustForceCor = StartCoroutine(AdjustForce());
        }
    }

    private void AdjustEnd()
    {
        if (adjustForceCor != null)
        {
            StopCoroutine(adjustForceCor);
            adjustForceCor = null;
            Shoot();
        }
    }

    void Update()
    {

    }

    private bool IsTouchInButtonRegion(Vector2 touchPosition)
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
        Vector3 initialVelocity = forceDirection * force;
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

    IEnumerator AdjustForce()
    {
        while (true)
        {
            touchPosition = pressAction.ReadValue<Vector2>();
            if (IsTouchInButtonRegion(touchPosition))
            {
                // Scale the step with Time.deltaTime for smoother adjustment
                float scaledStep = step * Time.deltaTime;
                if (isIncreasing)
                {
                    force += scaledStep;
                    if (force >= 100f)
                    {
                        force = 100f;
                        isIncreasing = false; // Switch direction
                    }
                }
                else
                {
                    force -= scaledStep;
                    if (force <= 10f)
                    {
                        force = 10f;
                        isIncreasing = true; // Switch direction
                    }
                }
            }
            yield return null;
        }
    }
}
