using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnController : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform spawnParent;
    public GameObject prefab;
    public GameObject drawer;
    [SerializeField] ActionsController actionsController;

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
        if (actionsController.spawnReady)
        {
            if (ctx.started)
            {
                prefab = Instantiate(prefab);
                prefab.transform.SetParent(spawnParent);
                prefab.transform.position = spawnPoint.position;
                actionsController.SetSpawnReady(false);
                prefab.GetComponent<PinchDetector>().enabled = true;
                drawer.GetComponent<DrawSpawnedTrajectory>().enabled = true;
            }
        }
        else
        {
            Debug.Log("You cannot spawn");
            return;
        }
    }
}
