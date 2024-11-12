using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    //Build Indexes:
    //MainMenu: 0, Shop: 1, InGame: 2
    public void Shop()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayGame() 
    {
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {
        //open settings window
    }
}
