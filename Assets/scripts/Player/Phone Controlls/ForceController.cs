using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForceController : MonoBehaviour
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

    private Coroutine adjustForceCor;

    [SerializeField] ActionsController actionsController;

    void Start()
    {
        pressAction = phoneServer.ActionAssetInstance.FindAction(pressActionPath);
        pressAction.started += _ => AdjustStart();
        pressAction.canceled += _ => AdjustEnd();
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
        }
    }

    void Update()
    {

    }

    private bool IsTouchInForceButtonRegion(Vector2 touchPosition)
    {
        // Check if the touch position is within the button region
        return buttonRegion.Contains(touchPosition);
    }

    IEnumerator AdjustForce()
    {
        while (true)
        {
            touchPosition = pressAction.ReadValue<Vector2>();
            if (IsTouchInForceButtonRegion(touchPosition))
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
