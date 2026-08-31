using UnityEngine;

public class GemAnimation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float floatHeight = 0.25f;
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotação
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Flutuação
        float newY = startPosition.y +
                     Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}