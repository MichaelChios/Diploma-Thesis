using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PinchDetector : MonoBehaviour
{
    [SerializeField] DeviceServer phoneServer;
    [SerializeField] string primFingerPosActionPath;
    [SerializeField] string secFingerPosActionPath;
    [SerializeField] string secTouchConPath;
    [SerializeField] float scaleSpeed = 0.01f;
    InputAction primFingerPosAction;
    InputAction secFingerPosAction;
    InputAction secTouchConAction;
    private Coroutine zoomCoroutine;
    private Rigidbody rb;

    private void OnEnable()
    {

    }

    private void Start()
    {
        phoneServer = FindObjectOfType<DeviceServer>();
        primFingerPosAction = phoneServer.ActionAssetInstance.FindAction(primFingerPosActionPath);
        secFingerPosAction = phoneServer.ActionAssetInstance.FindAction(secFingerPosActionPath);
        secTouchConAction = phoneServer.ActionAssetInstance.FindAction(secTouchConPath);
        secTouchConAction.started += _ => ZoomStart(); // _ => is a lambda expression
        secTouchConAction.canceled += _ => ZoomEnd();
        rb = GetComponent<Rigidbody>();
    }

    private void ZoomStart()
    {
        zoomCoroutine = StartCoroutine(PinchDetect());
    }

    private void ZoomEnd()
    {
        StopCoroutine(zoomCoroutine);
    }

    IEnumerator PinchDetect()
    {
        float previousDistance = 0f, currDistance = 0f;
        Vector3 targetScale = transform.localScale;
        while (true)
        {
            currDistance = Vector2.Distance(primFingerPosAction.ReadValue<Vector2>(), secFingerPosAction.ReadValue<Vector2>());
            if (currDistance > previousDistance)
            {
                // Zoom in
                targetScale += Vector3.one * scaleSpeed;
                rb.mass += 1;
            }   
            else if (currDistance < previousDistance)
            {
                // Zoom out
                targetScale -= Vector3.one * scaleSpeed;
                rb.mass -= 1;
            }
            targetScale = new Vector3(Mathf.Clamp(targetScale.x, 1f, 10f), Mathf.Clamp(targetScale.y, 1f, 10f), Mathf.Clamp(targetScale.z, 1f, 10f));
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 10f);
            rb.mass = Mathf.Clamp(rb.mass, 1f, 10f);
            previousDistance = currDistance;
            yield return null;
        }
    }
}

