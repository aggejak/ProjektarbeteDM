using UnityEngine;
using UnityEngine.UI;
public class SkinManager : MonoBehaviour
{
    [SerializeField] private Button mTButtoon, gDKButton, KTSButton;
    [Space]
    [SerializeField] [Tooltip("0 = MT, 1 = GDK, 2 = KTS")] private Material[] skins;
    [Space]
    [SerializeField] private int gDKScoreLimit = 50, KTSScoreLimit = 100;
    [Space]
    [SerializeField] private GameObject skinPickerMenu;

    private MeshRenderer playerMeshRenderer;

    private void Start()
    {
        playerMeshRenderer = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MeshRenderer>();
        playerMeshRenderer.material = skins[PlayerPrefs.GetInt("Skin")];
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
        playerMeshRenderer.material = skins[skin];
        Time.timeScale = 1;
        skinPickerMenu.SetActive(false);//close menu
    }
}
