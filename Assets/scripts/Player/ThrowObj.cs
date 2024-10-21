using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThrowObj : MonoBehaviour
{
    readonly float G = 0.0001f;
    public Transform cam;
    public Transform attackPoint;
    public GameObject objToThrow;
    private GameObject projectile;
    [SerializeField] GameObject sun;
    private Vector3 gravity;

    public int totalThrows;
    public float throwCooldown;

    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    [SerializeField] Slider massSlider;
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField]
    [Range(1, 20)] private int LinePoints = 10;
    [SerializeField]
    [Range(0.1f, 0.2f)] private float TimeBetweenPoints = 0.1f;

    bool ready;

    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Draw trajectory
        DrawTrajectory();

        if (Input.GetKeyDown(throwKey) && ready && totalThrows > 0)
        {
            Throw();
        }
    }

    private void DrawTrajectory()
    {
        LineRenderer.enabled = true;
        LineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPositon = attackPoint.position;
        Vector3 startVelocity = throwForce * cam.transform.forward / massSlider.value;
        int i = 0;
        LineRenderer.SetPosition(i, startPositon);
        float m1 = massSlider.value;
        float m2 = sun.GetComponent<Rigidbody>().mass;
        float r = Vector3.Distance(startPositon, sun.transform.position);
        gravity = (sun.transform.position - startPositon).normalized * (G * (m1 * m2) / (r * r));
        for (float time = 0; time < LinePoints; time += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPositon + startVelocity * time + gravity * time * time / 2f;
            LineRenderer.SetPosition(i, point);
        }

    }

    public void Throw()
    {
        ready = false;

        // Instantiate object to throw
        projectile = Instantiate(objToThrow, attackPoint.position, cam.rotation);

        // Set the parent of the instantiated object to the mixed reality scene content
        projectile.transform.SetParent(GameObject.Find("MixedRealitySceneContent").transform);

        // Get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // Set mass equal to the slider value
        projectileRb.mass = massSlider.value;

        // Calculate direction (it's not shooting straight)
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 5000f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        ready = true;
    }
}
