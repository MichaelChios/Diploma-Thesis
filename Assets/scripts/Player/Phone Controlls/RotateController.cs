using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateController : MonoBehaviour
{
    private void Awake()
    {

    }
    private void OnEnable() { }

    public void DoRotate(InputAction.CallbackContext ctx)
    {
        if (!enabled)
            return;
        var rotation = ctx.ReadValue<Quaternion>();
        transform.rotation = rotation;
    }
}
