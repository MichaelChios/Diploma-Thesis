using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public Transform world;
    public float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        world = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the world
        world.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0), Space.World);
    }
}
