using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform feetTransform;
  
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
    }
}
