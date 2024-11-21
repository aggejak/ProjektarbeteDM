using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    void Update()
    {
        // transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f); // turn object to face camera

        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        //directionToCamera.y = 0;

        if (directionToCamera != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(directionToCamera);
        }
    }
}
