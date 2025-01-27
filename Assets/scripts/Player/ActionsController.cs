using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsController : MonoBehaviour
{
    public bool spawnReady = true;
    public bool shootReady = false;

    public void SetSpawnReady(bool value)
    {
        spawnReady = value;
    }

    public void SetShootReady(bool value)
    {
        shootReady = value;
    }
}
