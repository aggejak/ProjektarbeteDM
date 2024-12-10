using UnityEngine;

public class FollowInOneDimension : MonoBehaviour
{
    public Transform target; // The object to follow
    public float speed = 5f; // Movement speed
    public bool smoothFollow = true; // Enable smooth follow

    void Update()
    {
        if (target != null)
        {
            // Get the current position of the follower
            Vector3 currentPosition = transform.position;

            // Create a new position that matches the target in one dimension (e.g., x-axis)
            Vector3 targetPosition = new Vector3(target.position.x, currentPosition.y, currentPosition.z);

            // Smooth or instant follow
            if (smoothFollow)
            {
                transform.position = Vector3.Lerp(currentPosition, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }
}