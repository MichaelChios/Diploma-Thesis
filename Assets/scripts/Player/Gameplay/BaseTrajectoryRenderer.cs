using UnityEngine;
public abstract class BaseTrajectoryRenderer: MonoBehaviour
{
    public abstract void Draw(Vector3 force, float mass);
}
