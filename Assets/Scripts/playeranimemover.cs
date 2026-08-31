using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Define se o player está andando com base na velocidade do Rigidbody
        bool isMoving = Mathf.Abs(_rb.linearVelocity.x) > 0.1f;
        _animator.SetBool("isWalking", isMoving);
    }
}
