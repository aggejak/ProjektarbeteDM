using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    void Update()
    {
         transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f); // turn object to face camera
    }
}
