using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    [Space(10)]
    [Header("Stats")]
    [ReadOnly] public Game game;
    [SerializeField, ReadOnly] private int score;
    [SerializeField, ReadOnly] private int coins;
    [SerializeField, ReadOnly] private int quantityToRave;
    [ReadOnly] public bool invicible;
    private float invicibleCount;
    private int idLevel;

    [Space(10)]
    [Header("Settings")]
    [SerializeField] private int life;
    public Pecboy pecboy;
    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private List<Level> levels;
    public Level level;

    [SerializeField] private CameraShake cameraShake;

    [Space(10)]
    [Header("Canvases")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject preloadingPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private GameObject transitionPanel;

    [SerializeField] private Slider invicibleSlider;


    [SerializeField] private Text scoreText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text lifeText;

    [SerializeField] private Text gameoverScoreText;
    [SerializeField] private Text gameoverCoinsText;

    [Space(10)]
    [Header("Audios")]
    [SerializeField] private PlayListMusic playListMusic;
    [SerializeField] private AudioSource fxAudioSource;
    [SerializeField] private AudioClip changeLevel;

    #region Game code
    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }

    }

    public Highscore[] highscoresList;
    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        quantityToRave = (coins + 2);
        GameState(Game.PRELOADING);
    }

    private void Update()
    {
        if (invicibleCount > 0)
        {
            invicibleSlider.gameObject.SetActive(true);
            invicibleCount -= Time.deltaTime;
            invicibleSlider.value = invicibleCount;
            invicible = true;
        }
        else
        {
            invicibleCount = 0;
            invicibleSlider.gameObject.SetActive(false);
            if(invicible == true)
            {
                for (int i = 0; i < ghosts.Length; i++)
                {
                    ghosts[i].GhostNormal();
                }
                invicible = false;
            }
        }
    }

    public void GameState(Game state)
    {
        game = state;
        loadingPanel.SetActive(false);
        preloadingPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        gameoverPanel.SetActive(false);

        scoreText.text = "SCORE:" + score.ToString();
        coinsText.text = coins.ToString();
        lifeText.text = life.ToString() + "X";

        gameoverScoreText.text = "SCORE:" + score.ToString();
        gameoverCoinsText.text = coins.ToString();

        if (state == Game.PRELOADING)
        {
            if(PlayerSetting.instance != null)
                pecboy.SetHeadItem(PlayerSetting.instance.item.image);
            preloadingPanel.SetActive(true);
        }
        else if(state == Game.GAMEPLAY)
        {
            gameplayPanel.SetActive(true);
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].Move();
            }
        }
        else if (state == Game.GAMEOVER)
        {
            gameoverPanel.SetActive(true);
        }
        else if (state == Game.LOADING)
        {
            loadingPanel.SetActive(true);
        }
    }

    public void SetToConfigState()
    {
        GameState(Game.CONFIG);
    }

    public void SetToPauseState()
    {
        GameState(Game.PAUSED);
    }

    public void SetToGameplayState()
    {
        GameState(Game.GAMEPLAY);
    }

    public void AddCoin()
    {
        coins++;
        if(coins > quantityToRave)
            DoRave();

        coinsText.text = coins.ToString();

        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GhostFear();
        }
        invicibleCount = 5f;
    }
    public void AddPoint()
    {
        level.points -= 1;
        score += 100;
        scoreText.text = "SCORE:" + score.ToString();
        if(level.points <= 0)
        {
            StartCoroutine(ChangeLevel());
        }
    }

    public void AddLife()
    {
        life++;
    }

    public void SubtractLife()
    {
        life--;
        if(life <= 0)
            GameState(Game.GAMEOVER);
    }

    public void DoRave()
    {
        cameraShake.RandomRave(1f);
    }

    public void Gameover()
    {
        pecboy.gameObject.SetActive(false);
        life--;
        lifeText.text = life.ToString() + "X";
        if (life > 0)
            StartCoroutine(WaitToReturnPacman());
        else
        {
            GameState(Game.GAMEOVER);
        }
    }

    #endregion

    #region Coroutines
    private IEnumerator WaitToGameBegin(float time)
    {
        yield return new WaitForSeconds(time);
        GameState(Game.GAMEPLAY);
    }

    private IEnumerator ChangeLevel()
    {
        if (playListMusic.middle)
        {
            playListMusic.StopMusic();
            playListMusic.PlayNextMusic();
        }
        GameState(Game.TRANSITION);
        level.gameObject.SetActive(false);

        idLevel++;
        if (idLevel > levels.Count - 1)
            idLevel = 0;
        level = levels[idLevel]; 
   
        transitionPanel.SetActive(true);
        cameraShake.StopRotation();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ToCenter();
            ghosts[i].GhostStopReturn();
        }
        fxAudioSource.PlayOneShot(changeLevel);
        pecboy.ToInitialPosition();
        level.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        level.ActivePoints();
        transitionPanel.SetActive(false);
        quantityToRave = (coins + 2);
        yield return new WaitForSeconds(0.6f);
        float timeAux = 2.5f;
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GhostReturn(timeAux);
            timeAux += 0.25f;
        }
        GameState(Game.GAMEPLAY);
    }

    private IEnumerator WaitToReturnPacman()
    {
        pecboy.gameObject.SetActive(false);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ToCenter();
            ghosts[i].GhostStopReturn();
        }
        yield return new WaitForSeconds(3f);

        if(life > 0)
        {
            float timeAux = 2.5f;
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].GhostReturn(timeAux);
                timeAux += 0.25f;
            }
            pecboy.gameObject.SetActive(true);
            pecboy.InitialPosition();
        }
        else
        {
            GameState(Game.GAMEOVER);
        }
    }
    #endregion

    #region Canvases Buttons
    public void OnClickPauseButton()
    {
        GameState(Game.PAUSED);
    }

    public void OnClickPlayButton()
    {
        preloadingPanel.SetActive(false);
        playListMusic.PlayActualMusic();
        StartCoroutine(WaitToGameBegin(playListMusic.actualMusic.timeToBegin));
    }

    public void OnClickMenuButton()
    {
        if(PlayerSetting.instance != null)
        {
            Player playerData = PlayerSetting.instance.player;
            if (score >= playerData.highscore)
                playerData.highscore = score;
            playerData.coins += coins;
            DatabaseManager.Save("player_data", playerData);
        }
        GameState(Game.LOADING);
        SceneManager.LoadScene(10);
    }
    #endregion
}
