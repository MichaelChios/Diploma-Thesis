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
    [SerializeField] ActionsController actionsController;
    //public GameObject drawer;
    //public ForceController forceAdjuster;
    //public ShootController shootController;

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
                GameObject spawnedObj = Instantiate(prefab);
                spawnedObj.transform.SetParent(spawnParent);
                spawnedObj.transform.position = spawnPoint.position;
                spawnedObj.GetComponent<PinchDetector>().enabled = true;
                actionsController.SetSpawnReady(false);
                actionsController.SetShootReady(true);
                //drawer.GetComponent<DrawSpawnedTrajectory>().enabled = true;
                //forceAdjuster.GetComponent<ForceController>().enabled = true;
                //shootController.GetComponent<ShootController>().enabled = true;
            }
        }
        else
        {
            Debug.Log("You cannot spawn");
            return;
        }
    }
}
