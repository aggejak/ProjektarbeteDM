using UnityEngine;
using UnityEngine.UI;
public class SkinManager : MonoBehaviour
{
    [SerializeField] private Button mTButtoon, gDKButton, KTSButton;
    [Space]
    [SerializeField] [Tooltip("0 = MT, 1 = GDK, 2 = KTS")] private Sprite[] skins;
    [Space]
    [SerializeField] private int gDKScoreLimit = 50, KTSScoreLimit = 100;
    [Space]
    [SerializeField] private GameObject skinPickerMenu;

    private SpriteRenderer playerSR;

    private void Start()
    {
        playerSR = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        playerSR.sprite = skins[PlayerPrefs.GetInt("Skin")];
        gDKButton.interactable = PlayerPrefs.GetInt("HighScore") >= gDKScoreLimit;
        KTSButton.interactable = PlayerPrefs.GetInt("HighScore") >= KTSScoreLimit;

        if (skinPickerMenu.activeSelf == false)
        {
            Time.timeScale = 0;
            skinPickerMenu.SetActive(true);
            PlayerPrefs.SetInt("HasPickedSkin", 1); // 0 = false, 1 = true
        }
    }
    public void SetSkin(int skin)
    {
        // 0 = MT, 1 = GDK, 2 = KTS
        PlayerPrefs.SetInt("Skin", skin);
        playerSR.sprite = skins[skin];
        Time.timeScale = 1;
        skinPickerMenu.SetActive(false);//close menu
    }
}
