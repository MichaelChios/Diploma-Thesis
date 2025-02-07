using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Mirror;

public class PracticeButton : MonoBehaviour, IMixedRealityTouchHandler
{
    public GameObject gameObj;
    private Renderer rnd;

    // Start is called before the first frame update
    void Start()
    {
        rnd = gameObj.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchCompleted(HandTrackingInputEventData eventData)
    {

    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        // Change the color of the object when touched randomly
        rnd.material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData)
    {

    }
}
