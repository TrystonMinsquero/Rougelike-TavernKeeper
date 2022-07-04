using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f;

    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        if(rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveInput)
    {
        rb.velocity = moveInput * moveSpeed;
    }
}
