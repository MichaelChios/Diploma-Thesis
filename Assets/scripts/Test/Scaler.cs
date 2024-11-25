using Mirror.BouncyCastle.Crypto.Signers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scaler : MonoBehaviour
{
    //[SerializeField] DeviceServer phoneServer;
    [SerializeField] float scaleSpeed = 0.1f;
    //[SerializeField] GameObject toScale;
    //[SerializeField] string scaleActionPath;
    //InputAction scaleAction;
    //[SerializeField] PinchDetector detector;

    private void Awake()
    {
        
    }

    private void OnEnable() { }

    public void DoScale(InputAction.CallbackContext ctx)
    {
        if (!enabled)
            return;
        var move = ctx.ReadValue<Vector2>();
        Debug.Log(move);
    }
}
