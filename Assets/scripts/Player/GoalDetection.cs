using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    public string projectileTag = "Projectile";  // Tag for identifying the projectile
    private int goals = 0;
    [SerializeField] TMP_Text scoreText;

    // Called when an object enters the ring's trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the "Projectile" tag
        if (other.gameObject.CompareTag(projectileTag))
        {
            // Print a message when the projectile passes through the ring
            Debug.Log("Projectile passed through the ring!");
            goals++;
            scoreText.text = goals.ToString();
            Destroy(other.gameObject, 0.1f);  // Destroy the projectile object
        }
    }
}
