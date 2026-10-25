
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    [Header("Target Detection")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform cannonNozzle;

    public Transform CurrentTarget { get; private set; }

    private void Update()
    {
        FindNearestEnemy();
        RotateTowardsTarget();
    }

    private void FindNearestEnemy()
    {
        EnemyBase[] enemies = FindObjectsByType<EnemyBase>(
            FindObjectsSortMode.None
        );

        Transform nearestEnemy = null;
        float nearestDistance = detectionRange;

        foreach (EnemyBase enemy in enemies)
        {
            float distance = Vector3.Distance(
                targetPoint.position,
                enemy.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        CurrentTarget = nearestEnemy;

        if (CurrentTarget != null)
        {
            Debug.Log("Target detected: " + CurrentTarget.name);
        }
    }

    private void RotateTowardsTarget()
    {
        if (CurrentTarget == null)
            return;

        Vector3 direction = CurrentTarget.position - targetPoint.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        cannonNozzle.rotation = Quaternion.RotateTowards(
            cannonNozzle.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}

