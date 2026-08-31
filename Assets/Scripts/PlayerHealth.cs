using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLife = 100;
    public int currentLife;
    public HealthBar healthBar;

    private SpriteRenderer sprite;

    void Start()
    {
        currentLife = maxLife;
        healthBar.SetMaxHealth(maxLife);

        sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        StartCoroutine(turnred());

        if (currentLife < 0)
            currentLife = 0;

        healthBar.SetHealth(currentLife);

        if (currentLife <= 0)
        {
            Die();
        }
    }

    IEnumerator turnred()
    {
        sprite.color = Color.blue;

        yield return new WaitForSeconds(0.1f);

        sprite.color = Color.white;
    }

    void Die()
    {
        SceneManager.LoadScene("GameOver");
    }
}