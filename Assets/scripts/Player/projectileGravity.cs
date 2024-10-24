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

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
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
        foreach (GameObject celestial in celestials)
        {
            float m1 = this.gameObject.GetComponent<Rigidbody>().mass;
            float m2 = celestial.GetComponent<Rigidbody>().mass;
            float r = Vector3.Distance(this.gameObject.transform.position, celestial.transform.position);
            this.gameObject.GetComponent<Rigidbody>().AddForce((celestial.transform.position - this.gameObject.transform.position).normalized * (G * (m1 * m2) / (r * r)));
        }
    }
}