using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}
