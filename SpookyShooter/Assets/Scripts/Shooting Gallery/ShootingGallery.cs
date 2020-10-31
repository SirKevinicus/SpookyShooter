using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootingGallery : MonoBehaviour
{
    [Header("Options")]
    public float boothWidth;
    public int startingLevel;
    public float waitTimeBeforeLevelStarts = 2f;

    [Header("Levels")]
    public GalleryLevel[] levels;

    [Header("UI")]
    public GameObject shootingUI;
    public GameObject crosshairs;
    public TextMeshProUGUI levelText;

    public GameObject beatLevelUI;
    public TextMeshProUGUI finalScoreText_win;

    public GameObject loseLevelUI;
    public TextMeshProUGUI finalScoreText_lose;

    public GameObject winGameUI;
    public TextMeshProUGUI finalScoreText_game;

    // REFERENCES
    public Player player;
    private Camera boothCam;
    private ScoreManager scoreManager;
    public Gun boothGun;
    private TargetSpawner spawner;

    // STATE
    [Header("State")]
    private GalleryLevel currentLevel;
    public int currentLevelNum;
    public bool playerInside = false;
    public bool playerInRange = false;

    // EVENTS
    public delegate void OnBeatLevel();
    public event OnBeatLevel onBeatLevel;

    public delegate void OnStartGallery();
    public event OnStartGallery onStartGallery;

    public delegate void OnEndGallery();
    public event OnEndGallery onEndGallery;

    // Start is called before the first frame update
    void Awake()
    {
        player = FindObjectOfType<Player>();

        boothGun = GetComponentInChildren<Gun>();
        boothGun.Initialize(player.toygunAmmoHolder);

        boothCam = GetComponentInChildren<Camera>();
        scoreManager = ScoreManager.instance;
        spawner = GetComponentInChildren<TargetSpawner>();

        boothCam.gameObject.SetActive(false);

        shootingUI.SetActive(false);
        beatLevelUI.SetActive(false);
        loseLevelUI.SetActive(false);
        winGameUI.SetActive(false);

        currentLevelNum = startingLevel;
        levelText.text = "Level " + currentLevelNum;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            player = null;
            playerInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) & playerInRange)
        {
            if(!playerInside)
            {
                StartShootingGallery();
            }
            else
            {
                EndShootingGallery();
            }
        }
    }

    public void StartShootingGallery()
    {
        playerInside = true;
        player.DisableMovement();

        boothCam.gameObject.SetActive(true);

        // UI
        Cursor.lockState = CursorLockMode.None;
        shootingUI.SetActive(true);

        player.UseGun(boothGun);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayCircusMusic();

        StartCoroutine(StartLevel(currentLevelNum));

        onStartGallery?.Invoke();
    }

    public IEnumerator StartLevel(int levelNum)
    {
        if (crosshairs != null) crosshairs.SetActive(true);
        Cursor.visible = false;

        currentLevel = levels[levelNum - 1];
        currentLevel.onBeatLevel += BeatLevel;
        currentLevel.onLoseLevel += LoseLevel;
        levelText.text = "Level " + currentLevelNum;

        scoreManager.ResetScore();

        yield return new WaitForSeconds(waitTimeBeforeLevelStarts);

        currentLevel.StartLevel(spawner);
        boothGun.canShoot = true;
    }

    public void RetryLevel()
    {
        scoreManager.ResetScore();
        loseLevelUI.SetActive(false);
        StartLevel(currentLevelNum);
    }

    public void NextLevel()
    {
        if(currentLevelNum < levels.Length)
            currentLevelNum++;

        beatLevelUI.SetActive(false);
        shootingUI.SetActive(true);

        StartCoroutine(StartLevel(currentLevelNum));
    }

    public void BeatLevel()
    {
        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shootingUI.SetActive(false);
        boothGun.canShoot = false;

        // If last level, then show winGame
        if (currentLevelNum == levels.Length)
        {
            winGameUI.SetActive(true);
            finalScoreText_game.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
        }
        else
        {
            beatLevelUI.SetActive(true);
            finalScoreText_win.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
        }

        onBeatLevel?.Invoke();
    }

    public void LoseLevel()
    {
        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseLevelUI.SetActive(true);
        shootingUI.SetActive(false);

        boothGun.canShoot = false;

        finalScoreText_lose.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
    }

    public void EndShootingGallery()
    {
        playerInside = false;
        player.EnableMovement();

        boothCam.gameObject.SetActive(false);

        // UI
        Cursor.lockState = CursorLockMode.Locked;
        shootingUI.SetActive(false);
        beatLevelUI.SetActive(false);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayAmbient();

        levels[currentLevelNum - 1].StopAllCoroutines();
        ScoreManager.instance.ResetScore();

        player.StopUsingGun();

        onEndGallery?.Invoke();
    }
}
