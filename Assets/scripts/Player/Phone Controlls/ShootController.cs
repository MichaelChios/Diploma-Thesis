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

    void Start()
    {
        pressAction = phoneServer.ActionAssetInstance.FindAction(pressActionPath);
    }

    void Update()
    {
        if (pressAction != null)
        {
            touchPosition = pressAction.ReadValue<Vector2>();
            if (IsTouchInButtonRegion(touchPosition))
            {
                Debug.Log("Touch in button region"); // Problem: The button is pressed when im not pressing it.
                AdjustForce();
            }
        }
    }

    private bool IsTouchInButtonRegion(Vector2 touchPosition)
    {
        // Check if the touch position is within the button region
        return buttonRegion.Contains(touchPosition);
    }

    private void AdjustForce()
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
}
