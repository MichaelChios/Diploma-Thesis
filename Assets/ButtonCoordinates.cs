using UnityEngine;

public class ButtonCoordinates : MonoBehaviour
{
    [SerializeField] private RectTransform buttonRectTransform; // Drag and drop the button's RectTransform here

    private void Start()
    {
        if (buttonRectTransform == null)
        {
            Debug.LogError("Please assign the RectTransform of your button in the Inspector.");
            return;
        }

        // Get the button's world-space corners
        Vector3[] worldCorners = new Vector3[4];
        buttonRectTransform.GetWorldCorners(worldCorners);

        Debug.Log($"Button Region (Screen-Space):");
        for (int i = 0; i < worldCorners.Length; i++)
        {
            // Convert world-space corners to screen-space
            Vector3 screenCorner = Camera.main.WorldToScreenPoint(worldCorners[i]);
            Debug.Log($"Corner {i}: {screenCorner}");
        }
    }
}
