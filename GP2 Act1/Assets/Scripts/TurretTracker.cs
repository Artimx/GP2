using UnityEngine;

public class TurretTracker : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 10f;

    void Update()
    {
        // If there is no target, find another drone
        if (target == null)
        {
            FindNextDrone();
        }

        // Still no drone available
        if (target == null)
        {
            return;
        }

        Vector3 directionToTarget =
            (target.position - transform.position).normalized;

        Quaternion targetRotation =
            Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        float alignment =
            Vector3.Dot(transform.forward, directionToTarget);

        if (alignment > 0.98f)
        {
            Debug.DrawLine(
                transform.position,
                target.position,
                Color.green
            );
        }
    }

    void FindNextDrone()
    {
        OrbitalTarget[] drones =
            FindObjectsByType<OrbitalTarget>(FindObjectsSortMode.None);

        if (drones.Length > 0)
        {
            target = drones[0].transform;
        }
    }
}