using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
    [SerializeField] private float backgroundLength; // Length of the background
    [SerializeField] private Transform otherBackground; // The other background to align with

    private void Update()
    {
        // Move the background backward
        transform.position += Vector3.back * GameManager.worldSpeed * Time.deltaTime;

        // Check if this background has moved out of view
        if (transform.position.z <= -backgroundLength)
        {
            // Reposition it precisely at the end of the other background
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                otherBackground.position.z + backgroundLength - 0.25f
            );
        }
    }
}
