
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireInterval = 2f;

    private float nextFireTime;

    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireInterval;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
            return;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation * Quaternion.Euler(90f, 0f, 0f)
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        TurretColorSwitch colorSwitch = GetComponent<TurretColorSwitch>();

        if (bulletScript != null && colorSwitch != null)
        {
            Debug.Log("Bullet material: " + colorSwitch.CurrentMaterial.name);

            bulletScript.Initialize(
                colorSwitch.CurrentMaterial,
                firePoint.up
            );
        }
    }
}
