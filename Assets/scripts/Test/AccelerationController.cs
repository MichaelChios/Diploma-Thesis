using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AccelerationController : MonoBehaviour
{
    [SerializeField] DeviceServer phoneServer;
    [SerializeField] string accelerateActionPath;
    InputAction accelerateAction;
    private Vector3 velocity = Vector3.zero;
    public float sensitivity = 1.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private void Start()
    {
        accelerateAction = phoneServer.ActionAssetInstance.FindAction(accelerateActionPath);
    }

    private void OnEnable()
    {

    }

    public void OnAccelerate(InputAction.CallbackContext ctx)
    {
        GetAcceleration(ctx.ReadValue<Vector3>());
    }

    private void Update()
    {
        GetAcceleration(accelerateAction.ReadValue<Vector3>());
    }

    private void GetAcceleration(Vector3 acceleration)
    {
        if (!enabled)
            return;
        velocity += acceleration * Time.deltaTime;
        Vector3 movement = velocity * sensitivity * Time.deltaTime;
        transform.position += movement;
    }
}
