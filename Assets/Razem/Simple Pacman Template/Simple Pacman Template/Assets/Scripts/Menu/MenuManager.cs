using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject principalPanel;
    public GameObject shopPanel;
    public GameObject loadingPanel;

    public GameObject pecboy;

    public Menu menu;

    public ShopManager shopManager;

    public Text principalCoinText;
    public Text principalHighscoreText;
    public Text shopCoinText;

    public Player playerData;
    public bool loadData = true;

    public CameraShake cameraShake;
    public bool changeSize;
    public float shakeCount;
    public int adsInterstitialShow;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (loadData)
        {
            playerData = (Player)DatabaseManager.Load("player_data");
            if (playerData == null)
            {
                playerData = new Player();
                playerData.coins = 150;
                playerData.highscore = 0;
                playerData.idEquipedItem = 0;
                playerData.idPurchasedItems.Add(0);
                DatabaseManager.Save("player_data", playerData);
            }
        }
        else
        {
            DatabaseManager.DeleteSave("player_data");
        }
    }

    private void Start()
    {
        adsInterstitialShow = PlayerPrefs.GetInt("interstitial_ads", 0);
        MenuState(menu);
    }

    public void MenuState(Menu state)
    {
        menu = state;

        principalPanel.SetActive(false);
        shopPanel.SetActive(false);
        loadingPanel.SetActive(false);
        pecboy.SetActive(false);
        UpdateUITextInfo();

        if (state == Menu.PRINCIPAL)
        {
            pecboy.SetActive(true);
            principalPanel.SetActive(true);
        }
        else if (state == Menu.SHOP)
        {
            pecboy.SetActive(true);
            shopPanel.SetActive(true);
        }
        else if (state == Menu.LOADING)
        {
            loadingPanel.SetActive(true);
            SceneManager.LoadScene("Game");
        }
    }

    public void UpdateUITextInfo()
    {
        principalHighscoreText.text = $"Highscore: {playerData.highscore}";
        principalCoinText.text = playerData.coins.ToString();
        shopCoinText.text = playerData.coins.ToString();
    }

    public void OnClickShopButton()
    {
        MenuState(Menu.SHOP);
    }

    public void OnClickBackButton()
    {
        MenuState(Menu.PRINCIPAL);
    }

    public void OnClickPlayButton()
    {
        PlayerSetting.instance.player = playerData;
        PlayerSetting.instance.item = shopManager.itemActual;
        loadingPanel.SetActive(true);
        MenuState(Menu.LOADING);
    }

}
