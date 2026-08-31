using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Bônus")]
    public int bonusHealth = 0;
    public int bonusDamage = 0;
    public float bonusSpeed = 0f;

    // Aumentar vida
    public void AddHealth()
    {
        PlayerHealth health = GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.maxLife += 20;
            health.currentLife += 20;

            Debug.Log("Vida aumentou! Vida máxima: " + health.maxLife);
        }
    }

    // Aumentar dano
    public void AddDamage()
    {
        bonusDamage += 5;

        Debug.Log("Dano aumentou! Bônus: +" + bonusDamage);
    }

    // Aumentar velocidade
    public void AddSpeed()
    {
        PlayerMovement movement = GetComponent<PlayerMovement>();

        if (movement != null)
        {
            movement.speed += 0.5f;

            Debug.Log("Velocidade aumentou! Velocidade: " + movement.speed);
        }
    }
}