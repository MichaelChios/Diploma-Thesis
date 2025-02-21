using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Mirror;

public class MRShootButton : MonoBehaviour, IMixedRealityTouchHandler
{
    public Transform cam;
    public Transform attackPoint;
    public float throwCooldown;
    [SerializeField] MassPinchSliderRemapper massSlider;
    [SerializeField] ThrowForcePinchSliderRemapper throwForceSlider;
    [SerializeField] Thrower thrower;
    bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
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

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {

    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {

    }
}
