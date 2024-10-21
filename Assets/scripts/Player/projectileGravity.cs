using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileGravity : MonoBehaviour
{
    //real value of gravitational constant is 6.67408 × 10-11
    //can increase to make thing go faster instead of increase timestep of Unity
    readonly float G = 0.0001f;
    GameObject[] celestials;
    GameObject[] projectiles;
    public Vector3 gravity;

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
    }

    // Update is called once per frame
    private void Update()
    {

    }
    void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject a in projectiles)
        {
            foreach (GameObject b in celestials)
            {
                float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);
                gravity = (b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r));
                a.GetComponent<Rigidbody>().AddForce(gravity);
            }
        }
    }
}