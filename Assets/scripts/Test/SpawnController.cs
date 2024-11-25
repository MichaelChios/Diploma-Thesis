using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnController : MonoBehaviour
{
    //public Rigidbody toSpawn;
    public Transform spawnPoint;
    public Transform spawnParent;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public void DoSpawn(InputAction.CallbackContext ctx)
    {
        if (!enabled)
            return;
        if (ctx.started)
        {
            // change the color of the object randomly
            //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            // spawn the object
            Instantiate(this);
            this.transform.SetParent(spawnParent);
            this.transform.position = spawnPoint.position;
        }
    }
}
