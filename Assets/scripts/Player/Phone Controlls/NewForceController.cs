using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewForceController : MonoBehaviour
{
    [SerializeField] private DeviceServer phoneServer;
    [SerializeField] private string pressActionPath;

    private InputAction pressAction;
    public float force = 10f; // Current force value
    private bool isIncreasing = true; // Determines if the force is increasing or decreasing
    private float step = 10f; // Step value for increasing/decreasing force

    // Button Region (screen-space coordinates)
    private readonly Rect buttonRegion = new Rect(207, 261, 480, 240);
    public Vector2 touchPosition;

    [SerializeField] ActionsController actionsController;

    // Start is called before the first frame update
    void Start()
    {
        pressAction = phoneServer.ActionAssetInstance.FindAction(pressActionPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (actionsController.shootReady)
        {
            touchPosition = pressAction.ReadValue<Vector2>();
            if (IsTouchInForceButtonRegion(touchPosition))
            {
                AdjustForce();
                touchPosition = Vector2.zero;
            }
        }
    }

    private bool IsTouchInForceButtonRegion(Vector2 touchPosition)
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
