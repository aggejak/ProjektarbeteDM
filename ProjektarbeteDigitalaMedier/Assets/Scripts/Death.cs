using UnityEngine;

public class Death : MonoBehaviour
{
    GameManager gM;
    private void Awake()
    {
        gM = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player")) 
        {
            gM.ChangeGameState(GameState.GameOver);        
        }
        Debug.Log("Death! from: " + gameObject.name + " player is: " + collision.name);
    }
}