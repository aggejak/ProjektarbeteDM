using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform feetTransform;
    [SerializeField] private int jumpStrength = 30; // [SerializeField] lets you initialise private variables through the inspector
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        feetTransform = transform.GetChild(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        if (IsGrounded())
        {
            rb.linearVelocityY = jumpStrength;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.Raycast(feetTransform.position, -feetTransform.up); // obs! Player layer must be on physics layer "ignoreRaycast" for this to work

        if (ray.collider != null && ray.collider.CompareTag("Ground"))// will only detect collision with objects tagged "Ground"
        {
            if (feetTransform.position.y - ray.transform.position.y <= 1)// if feet touch the ground
            {
                return true;
            }
        }
        return false;
    }
}
