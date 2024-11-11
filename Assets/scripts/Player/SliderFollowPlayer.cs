using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

public class SliderFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0, 0, 0.56f);  // Offset from the player

    private void Start()
    {
        if (playerTransform == null)
        {
            // Set the playerTransform to the main camera if not set
            playerTransform = CameraCache.Main.transform;
        }
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Set the position of the slider to follow the player with the offset
            transform.position = playerTransform.position + playerTransform.forward * offset.z + playerTransform.up * offset.y + playerTransform.right * offset.x;

            // Make the slider face the player
            transform.LookAt(playerTransform);

            // Rotate 180 degrees to make the front face the player directly
            transform.Rotate(0, 180, 0);
        }
    }
}
