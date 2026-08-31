using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    public GameObject xpGemPrefab;
    public GameObject damageTextPrefab;

    private SpriteRenderer sprite;

    void Start()
    {
        currentHealth = maxHealth;

        // Pega o SpriteRenderer do inimigo
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Fica vermelho quando toma dano
        StartCoroutine(DamageFlash());

        // Mostrar o dano
        if (damageTextPrefab != null)
        {
            GameObject damageText = Instantiate(
                damageTextPrefab,
                transform.position + Vector3.up,
                Quaternion.identity
            );

            DamageText text = damageText.GetComponent<DamageText>();

            if (text != null)
            {
                text.SetDamage(damage);
            }
        }

        Debug.Log("Inimigo recebeu " + damage + " de dano. Vida: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DamageFlash()
    {
        if (sprite == null)
            yield break;

        sprite.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        sprite.color = Color.white;
    }

    void Die()
    {
        // Soltar XP
        if (xpGemPrefab != null)
        {
            Instantiate(
                xpGemPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}