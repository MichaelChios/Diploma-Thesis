using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionWithCelestials : MonoBehaviour
{
    // Assign the "Celestials" tag in the Unity Inspector or via code
    public string celestialTag = "Celestial";

    void Start()
    {
        // Find all celestial objects by tag
        GameObject[] celestials = GameObject.FindGameObjectsWithTag(celestialTag);

        // Get the collider of the target object (this object)
        Collider targetCollider = GetComponent<Collider>();

        // Loop through all celestial objects and ignore collisions with the target
        foreach (GameObject celestial in celestials)
        {
            Collider celestialCollider = celestial.GetComponent<Collider>();
            if (celestialCollider != null)
            {
                Physics.IgnoreCollision(targetCollider, celestialCollider);
            }
        }
    }
}
