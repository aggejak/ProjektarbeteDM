using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.back * GameManager.worldSpeed * Time.deltaTime;
    }
}
