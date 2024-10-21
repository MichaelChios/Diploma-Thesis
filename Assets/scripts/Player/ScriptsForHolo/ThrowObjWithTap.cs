using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;

public class ThrowObjWithTap : MonoBehaviour, IMixedRealityInputActionHandler
{
    public Transform cam;
    public Transform attackPoint;
    public GameObject objToThrow;

    public int totalThrows;
    public float throwCooldown;

    public float throwForce;
    public float throwUpwardForce;

    private bool ready;

   [SerializeField] private MixedRealityInputAction tapAction = MixedRealityInputAction.None;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        // The throwing logic will be handled in the OnPointerClick method
    }

    // When a tap gesture is detected, throw the object
    public void OnActionStarted(BaseInputEventData eventData)
    {
        Debug.Log("OnActionStarted");
        // Cast to InputEventData to check for specific actions
        InputEventData inputEventData = eventData as InputEventData;

        // Check if the action is the tap (select) action
        if (inputEventData != null && inputEventData.MixedRealityInputAction == tapAction)
        {
            Debug.Log("OnActionStarted - tapAction");
            // Ensure that we're ready and have throws left
            if (ready && totalThrows > 0)
            {
                Throw();  // Trigger the throw
            }
        }
    }

    // Called when the tap gesture (air tap) is completed
    public void OnActionEnded(BaseInputEventData eventData)
    {
        // Optionally handle logic for when the tap action ends (if needed)
    }

    private void Throw()
    {
        ready = false;

        // Instantiate object to throw
        GameObject projectile = Instantiate(objToThrow, attackPoint.position, cam.rotation);

        // Set the parent of the instantiated object to the mixed reality scene content
        projectile.transform.SetParent(GameObject.Find("MixedRealitySceneContent").transform);

        // Get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Calculate direction (it's not shooting straight)
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 5000f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        ready = true;
    }

    // Implementing the empty methods from the IMixedRealityInputActionHandler to avoid compiler warnings
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }

    public void OnPointerUp(MixedRealityPointerEventData eventData) { }

    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
