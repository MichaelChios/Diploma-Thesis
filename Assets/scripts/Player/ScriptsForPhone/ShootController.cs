using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] DeviceServer phoneServer;
    [SerializeField] string shootActionPath;
    InputAction shootAction;
    private void Awake()
    {
        
    }
    private void Start()
    {
        shootAction = phoneServer.ActionAssetInstance.FindAction(shootActionPath);
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        Shoot();
    }

    private void Update()
    {
        Shoot();
    }
    private void Shoot()
    {
        if (!enabled)
            return;
        Debug.Log("Shoot");
    }
}
