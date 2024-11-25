using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchDetector : MonoBehaviour
{
    [SerializeField] DeviceServer phoneServer;
    [SerializeField] string primFingerPosActionPath;
    [SerializeField] string secFingerPosActionPath;
    [SerializeField] string secTouchConPath;
    [SerializeField] string accPath;
    [SerializeField] float scaleSpeed = 0.01f;
    InputAction primFingerPosAction;
    InputAction secFingerPosAction;
    InputAction secTouchConAction;
    InputAction accAction;

    Touchscreen touchScreen;
    private void Awake()
    {

    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        primFingerPosAction = phoneServer.ActionAssetInstance.FindAction(primFingerPosActionPath);
        secFingerPosAction = phoneServer.ActionAssetInstance.FindAction(secFingerPosActionPath);
        //secTouchConAction = phoneServer.ActionAssetInstance.FindAction(secTouchConPath);
        accAction = phoneServer.ActionAssetInstance.FindAction(accPath);
        touchScreen = InputSystem.GetDevice<Touchscreen>();

    }

    //public void OnDetect(InputAction.CallbackContext ctx)
    //{
    //    if (ctx.started)
    //    {
    //        Debug.Log("Pinch detected");
    //        //PinchDetect(primFingerPosAction.ReadValue<Vector2>(), secFingerPosAction.ReadValue<Vector2>());
    //    }
    //}

    //private void Update()
    //{
    //    PinchDetect(primFingerPosAction.ReadValue<Vector2>(), secFingerPosAction.ReadValue<Vector2>());
    //    //Debug.Log(accAction.ReadValue<int>());

    //}

    public void checkPrim(InputAction.CallbackContext ctx)
    {
        if (!enabled)
        {
            return;
        }
        Debug.Log(ctx.ReadValue<Vector2>());
    }

    private void PinchDetect(Vector2 pos1, Vector2 pos2)
    {
        if (!enabled)
            return;
        //Debug.Log(secTouchConAction.ReadValue<int>());
        //if (pos1 != null && pos2 != null)
        //{
        float previousDistance = 0f, currDistance = 0f;
        currDistance = Vector2.Distance(pos1, pos2);
        Debug.Log(pos1);
        Debug.Log(pos2);

        if (currDistance > previousDistance)
        {
            // Zoom out
            Debug.Log("Zoom out");
            //transform.localScale += Vector3.one * scaleSpeed;
        }
        else if (currDistance < previousDistance)
        {
            // Zoom in
            Debug.Log("Zoom in");
            //transform.localScale -= Vector3.one * scaleSpeed;
        }
        previousDistance = currDistance;
        }
    //}
}

