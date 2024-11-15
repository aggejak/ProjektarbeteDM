using UnityEngine;

public class SpriteTurner : MonoBehaviour
{
    [SerializeField] private bool continuousTurning = true;// r�r sig f�rem�let i f�rh�llande till kameran? (on�digt att vrida varje frame om det �nd� inte r�r sig)

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
