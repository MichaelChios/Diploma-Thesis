using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ThrowForcePinchSliderRemapper : MonoBehaviour
{
    public PinchSlider pinchSlider;   // Reference to the PinchSlider component
    public float inputMin = 0f;       // Original slider minimum
    public float inputMax = 1f;       // Original slider maximum
    public float outputMin = 10f;     // New minimum range
    public float outputMax = 100f;    // New maximum range

    public float TFRemappedValue { get; private set; }  // Public property to access the remapped value

    private void Start()
    {
        if (pinchSlider == null)
        {
            pinchSlider = GetComponent<PinchSlider>();
        }

        // Register the event listener for value changes
        pinchSlider.OnValueUpdated.AddListener(OnSliderValueUpdated);
    }

    private void OnSliderValueUpdated(SliderEventData eventData)
    {
        // Get the current slider value
        float currentValue = eventData.NewValue;

        // Remap the slider value to the new range
        TFRemappedValue = Remap(currentValue, inputMin, inputMax, outputMin, outputMax);

        // Log the value for debugging
        Debug.Log($"Original Value: {currentValue}, Remapped Value: {TFRemappedValue}");
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnDestroy()
    {
        // Clean up event listener
        pinchSlider.OnValueUpdated.RemoveListener(OnSliderValueUpdated);
    }
}
