using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5;

    float horizontalInput;
    public float horizontalMultiplier = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Movement along the x-axis in 2D
        Vector2 horizontalMove = new Vector2(horizontalInput * speed * horizontalMultiplier * Time.fixedDeltaTime, 0);

        // Update the rigidbody position
        rb.MovePosition(rb.position + horizontalMove);
    }
}
