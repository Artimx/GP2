
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private Renderer tipRenderer;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private float hitRadius = 0.5f;

    private Color bulletColor;
    private Vector3 moveDirection;
    private float timer;
    private bool initialized;

    public void Initialize(Material bulletMaterial, Vector3 direction)
    {
        if (tipRenderer != null && bulletMaterial != null)
        {
            Material[] materials = tipRenderer.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = bulletMaterial;
            }

            tipRenderer.materials = materials;
            bulletColor = bulletMaterial.color;
        }

        moveDirection = direction.normalized;
        initialized = true;
        timer = 0f;
    }

    private void Update()
    {
        if (!initialized)
            return;

        timer += Time.deltaTime;

        if (timer >= lifetime)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 previousPosition = transform.position;
        Vector3 nextPosition =
            previousPosition + moveDirection * speed * Time.deltaTime;

        Vector3 travel = nextPosition - previousPosition;
        float distance = travel.magnitude;

        if (distance > 0f)
        {
            Ray ray = new Ray(previousPosition, travel.normalized);

            if (Physics.SphereCast(
                ray,
                hitRadius,
                out RaycastHit hit,
                distance,
                Physics.DefaultRaycastLayers,
                QueryTriggerInteraction.Collide))
            {
                EnemyBase enemy =
                    hit.collider.GetComponentInParent<EnemyBase>();

                if (enemy != null)
                {
                    if (bulletColor == enemy.BodyColor)
                    {
                        enemy.TakeDamage(1f);

                        Debug.Log(
                            "Matching bullet! " +
                            enemy.name + " took 1 damage."
                        );
                    }
                    else
                    {
                        Debug.Log(
                            "Wrong color! No damage dealt to " +
                            enemy.name + "."
                        );
                    }

                    Destroy(gameObject);
                    return;
                }
            }
        }

        transform.position = nextPosition;
    }
}
