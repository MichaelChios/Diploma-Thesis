using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerationController : MonoBehaviour
{
    [SerializeField] DeviceServer phoneServer;
    [SerializeField] string linAccelerateActionPath;
    InputAction linAccelerateAction;
    private Rigidbody rb;
    public float sensitivity = 5000f;
    public float damping = 5f;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        
    }

    private void Start()
    {
        linAccelerateAction = phoneServer.ActionAssetInstance.FindAction(linAccelerateActionPath);
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {
        GetAcceleration(linAccelerateAction.ReadValue<Vector3>());
    }

    private void GetAcceleration(Vector3 acceleration)
    {
        if (!enabled)
            return;

        //// Convert acceleration to world space
        //Vector3 movement = ConvertToWorldSpace(acceleration);

        //// Smooth the movement to avoid jitter
        //movement = Vector3.Lerp(currentVelocity, movement * sensitivity * Time.deltaTime, Time.deltaTime * damping);

        //// Apply movement to the object's position
        //transform.position += movement;

        //// Update current velocity
        //currentVelocity = movement;
        transform.Translate(acceleration.x, 0, -acceleration.z);
    }

    private Vector3 ConvertToWorldSpace(Vector3 acceleration)
    {
        // Map device acceleration to world space
        Vector3 worldAcceleration = new Vector3(acceleration.x, 0, acceleration.y);
        worldAcceleration = transform.TransformDirection(worldAcceleration);
        worldAcceleration.y = 0;
        return worldAcceleration;
    }

    private void ApplyDamping()
    {
        // Reduce velocity over time for smoother slowing
        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, damping * Time.deltaTime);
    }
}
