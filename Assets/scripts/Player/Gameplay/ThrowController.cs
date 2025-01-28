using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;

public class ThrowController : MonoBehaviour, IMixedRealityGestureHandler
{
    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    public float throwCooldown;

    public KeyCode throwKey = KeyCode.Mouse1;
    
    [SerializeField] MassPinchSliderRemapper massSlider;
    [SerializeField] ThrowForcePinchSliderRemapper throwForceSlider;
    [SerializeField] List<Rigidbody> celestialsRb = new();

    [SerializeField] Thrower thrower;
    public BaseTrajectoryRenderer trajectRenderer;

    bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    private void OnEnable()
    {
        // Instruct Input System that we would like to receive all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityGestureHandler>(this);
    }

    private void OnDisable()
    {
        // Instruct Input System to disregard all input events of type IMixedRealityGestureHandler
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityGestureHandler>(this);
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
        Vector3 initialVelocity = forceDirection * throwForceSlider.TFRemappedValue;

        var mass = massSlider.MRemappedValue;

        // Draw trajectory
        trajectRenderer.Draw(startPoint, initialVelocity, mass, celestialsRb);
    }

    public void OnGestureStarted(InputEventData eventData)
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
        Vector3 initialVelocity = forceDirection * throwForceSlider.TFRemappedValue;

        var mass = massSlider.MRemappedValue;

        if (ready)
            thrower.ThrowWithCooldown(initialVelocity, mass, throwCooldown, startPoint: attackPoint);
    }

    public void OnGestureUpdated(InputEventData eventData)
    {

    }

    public void OnGestureCompleted(InputEventData eventData)
    {

    }

    public void OnGestureCanceled(InputEventData eventData)
    {

    }
}
