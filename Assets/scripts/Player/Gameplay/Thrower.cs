using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using System.Collections;

//Check Serializable

public class Thrower: MonoBehaviour
{
    [SerializeField] int totalThrows = 0;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform projectileParent;

    public float throwCooldown = 0.1f;
    public Rigidbody throwPrefab;
    public int TotalThrows => totalThrows;

    private Coroutine cor;
    public Rigidbody ThrowWithCooldown(Vector3 force, float mass, float cooldown, Rigidbody toThrow = null)
    {
        if (cor != null)
            return null;
        var proj = Throw(force, mass, toThrow);
        
        // Check coroutines in Unity
        IEnumerator throwProjectile() {
            // this would create garbage
            // yield return new WaitForSeconds(cooldown); 
            float time = 0;
            while (time < cooldown)
                time += Time.deltaTime;
            yield return null;
            cor = null;
        }

        cor = StartCoroutine(throwProjectile());
        return proj;
    }
    public Rigidbody Throw(Vector3 force, float mass, Rigidbody toThrow = null)
    {
        if (toThrow == null)
            toThrow = Instantiate(throwPrefab);
        toThrow.transform.SetParent(projectileParent);
        toThrow.position = attackPoint.position;
        toThrow.mass = mass;
        toThrow.AddForce(force);
        totalThrows++;

        return toThrow;
    }
}
