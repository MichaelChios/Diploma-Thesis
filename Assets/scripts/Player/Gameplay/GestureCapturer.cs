using Microsoft.MixedReality.OpenXR;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class GestureCapturer : MonoBehaviour, IMixedRealityGestureHandler
{
    GestureRecognizer recognizer = new GestureRecognizer(GestureSettings.Tap);

    public void OnGestureCanceled(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        Debug.Log("Gesture Completed");
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnGestureUpdated(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
