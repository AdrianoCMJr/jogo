using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackRate = 1f;
    public float attackRange = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackRate)
        {
            timer = 0;
            AttackNearestEnemy();
        }
    }

    void AttackNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return;

        GameObject nearestEnemy = null;
        float shortestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().SetTarget(nearestEnemy.transform);
        }
    }
}