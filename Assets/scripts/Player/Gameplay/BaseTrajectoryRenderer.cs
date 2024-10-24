using System.Collections.Generic;
using UnityEngine;
public abstract class BaseTrajectoryRenderer: MonoBehaviour
{
    public void Draw(Rigidbody body, Vector3 force, List<Rigidbody> celestials) {
        Draw(body.transform.position, force, body.mass, celestials);
    }
    public abstract void Draw(Vector3 startPoint, Vector3 force, float mass, List<Rigidbody> celestials);
}
