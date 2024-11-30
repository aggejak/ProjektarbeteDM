using Unity.VisualScripting;
using UnityEngine;

public class PauseInput : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameManager gM;
    void Start()
    {
        gM = FindAnyObjectByType<GameManager>();
        gM.ChangeGameState(GameState.InGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gM.State == GameState.InGame)
            {
                gM.PauseGame();
            }
            else if (gM.State == GameState.Paused)
            {
                gM.ResumeGame();
            }
        }
    }
}