using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    [SerializeField] private bool continuousTurning = true;// rör sig föremålet i förhållande till kameran? (onödigt att vrida varje frame om det ändå inte rör sig)

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f); // turn object to face camera
    }
    void Update()
    {
        if (continuousTurning)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f); // turn object to face camera
        }
    }
}
