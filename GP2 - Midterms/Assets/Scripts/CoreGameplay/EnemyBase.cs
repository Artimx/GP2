
using System.Collections;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Enemy Settings")]
    [SerializeField] protected float maxHealth = 1f;
    [SerializeField] protected float moveSpeed = 3f;

    [Header("Hit Flash Settings")]
    [SerializeField] private float hitFlashDuration = 0.1f;

    private Renderer[] enemyRenderers;
    private Material[][] originalMaterials;
    private Color[][] originalColors;
    private Coroutine hitFlashCoroutine;

    protected float currentHealth;
    protected Transform target;

    public Color BodyColor
    {
        get
        {
            if (originalColors == null ||
                originalColors.Length == 0 ||
                originalColors[0] == null ||
                originalColors[0].Length == 0)
            {
                return Color.clear;
            }

            return originalColors[0][0];
        }
    }

    protected virtual void Awake()
    {
        currentHealth = maxHealth;

        // Find all renderers on this enemy and its children.
        enemyRenderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[enemyRenderers.Length][];
        originalColors = new Color[enemyRenderers.Length][];

        // Store the original materials and their colors.
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            Material[] materials = enemyRenderers[i].materials;
            originalMaterials[i] = materials;
            originalColors[i] = new Color[materials.Length];

            for (int j = 0; j < materials.Length; j++)
            {
                originalColors[i][j] = materials[j].color;
            }
        }
    }

    public void SetTarget(Transform targetPoint)
    {
        target = targetPoint;
    }

    protected virtual void Update()
    {
        if (target == null)
            return;

        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            GameOverManager gameOverManager =
                FindFirstObjectByType<GameOverManager>();

            if (gameOverManager != null)
            {
                gameOverManager.GameOver();
            }

            return;
        }

        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            transform.rotation =
                Quaternion.LookRotation(direction) *
                Quaternion.Euler(0f, 180f, 0f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log(
            gameObject.name +
            " took damage! Health: " + currentHealth
        );

        if (currentHealth > 0f && enemyRenderers.Length > 0)
        {
            if (hitFlashCoroutine != null)
            {
                StopCoroutine(hitFlashCoroutine);
            }

            hitFlashCoroutine = StartCoroutine(FlashWhite());
        }

        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashWhite()
    {
        // Flash all materials on all child renderers.
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            for (int j = 0; j < originalMaterials[i].Length; j++)
            {
                if (originalMaterials[i][j] != null)
                {
                    originalMaterials[i][j].color = Color.white;
                }
            }
        }

        yield return new WaitForSeconds(hitFlashDuration);

        // Restore every material's original color.
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            for (int j = 0; j < originalMaterials[i].Length; j++)
            {
                if (originalMaterials[i][j] != null)
                {
                    originalMaterials[i][j].color =
                        originalColors[i][j];
                }
            }
        }

        hitFlashCoroutine = null;
    }
}
