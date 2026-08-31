using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;

    [Header("Movimento")]
    public float speed = 3f;

    [Header("Separação dos inimigos")]
    public float separationDistance = 1f;
    public float separationForce = 2f;

    private Animator anim;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObject =
                GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        // Direção até o jogador
        Vector2 direction =
            (player.position - transform.position).normalized;

        // Calcula a força para afastar dos outros inimigos
        Vector2 separation = CalculateSeparation();

        // Junta as duas forças
        Vector2 finalDirection =
            direction + separation * separationForce;

        // Normaliza para não ficar rápido demais
        finalDirection.Normalize();

        // Movimento
        rb.linearVelocity = finalDirection * speed;

        // Animação
        if (anim != null)
        {
            anim.SetBool("Walk", true);
        }

        // Virar o sprite
        if (sprite != null)
        {
            if (player.position.x > transform.position.x)
                sprite.flipX = false;
            else
                sprite.flipX = true;
        }
    }

    Vector2 CalculateSeparation()
    {
        Vector2 separation = Vector2.zero;

        Collider2D[] enemies =
            Physics2D.OverlapCircleAll(
                transform.position,
                separationDistance
            );

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject == gameObject)
                continue;

            if (!enemy.CompareTag("Enemy"))
                continue;

            Vector2 difference =
                transform.position - enemy.transform.position;

            float distance = difference.magnitude;

            if (distance > 0)
            {
                // Quanto mais perto, maior a força
                float strength =
                    1f - (distance / separationDistance);

                separation +=
                    difference.normalized * strength;
            }
        }

        return separation;
    }

    void OnDisable()
    {
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            separationDistance
        );
    }
}