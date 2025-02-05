using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController : MonoBehaviour
{
    public bool spawnReady = true;
    public bool shootReady = false;

    public GameObject drawer;
    public ForceController forceAdjuster;
    public ShootController shootController;
    public SpawnController spawnController;

    public void SetSpawnReady(bool value)
    {
        spawnReady = value;
        if (value == true)
        {
            drawer.GetComponent<DrawSpawnedTrajectory>().enabled = false;
            forceAdjuster.GetComponent<ForceController>().enabled = false;
            shootController.GetComponent<ShootController>().enabled = false;
        }
        else
        {
            drawer.GetComponent<DrawSpawnedTrajectory>().enabled = true;
            forceAdjuster.GetComponent<ForceController>().enabled = true;
            shootController.GetComponent<ShootController>().enabled = true;
        }
    }

    public void SetShootReady(bool value)
    {
        shootReady = value;
        if (value == true)
        {
            spawnController.GetComponent<SpawnController>().enabled = false;
        }
        else
        {
            spawnController.GetComponent<SpawnController>().enabled = true;
        }
    }
}
