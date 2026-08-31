using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshPro text;

    public float duration = 0.7f;
    public float moveSpeed = 1f;

    private float timer;

    void Start()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshPro>();
        }
    }

    public void SetDamage(int damage)
    {
        text.text = "-" + damage;
    }

    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}