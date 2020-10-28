using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootingGallery : MonoBehaviour
{
    [Header("Options")]
    public float boothWidth;
    public float waitTimeBeforeLevelStarts = 2f;

    [Header("Levels")]
    public GalleryLevel[] levels;

    [Header("UI")]
    public GameObject shootingUI;
    public GameObject crosshairs;
    public TextMeshProUGUI levelText;

    public GameObject winLevelUI;
    public TextMeshProUGUI finalScoreText_win;

    public GameObject loseLevelUI;
    public TextMeshProUGUI finalScoreText_lose;

    public GameObject winGameUI;
    public TextMeshProUGUI finalScoreText_game;

    // REFERENCES
    private Player player;
    private Camera boothCam;
    private ScoreManager scoreManager;
    private Gun boothGun;
    private TargetSpawner spawner;

    // STATE
    private GalleryLevel currentLevel;
    private int currentLevelNum;
    public bool playerInside = false;
    public bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        boothGun = GetComponentInChildren<Gun>();
        boothCam = GetComponentInChildren<Camera>();
        player = FindObjectOfType<Player>();
        scoreManager = ScoreManager.instance;
        spawner = GetComponentInChildren<TargetSpawner>();

        boothCam.gameObject.SetActive(false);

        shootingUI.SetActive(false);
        winLevelUI.SetActive(false);
        loseLevelUI.SetActive(false);
        winGameUI.SetActive(false);

        currentLevelNum = 1;
        levelText.text = "Level 1";
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((player = other.GetComponent<Player>()) != null)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if((player = other.GetComponent<Player>()) != null)
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

    private void StartShootingGallery()
    {
        playerInside = true;
        player.DisableMovement();

        boothCam.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        shootingUI.SetActive(true);

        player.UseGun(boothGun);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayCircusMusic();

        StartCoroutine(StartLevel(currentLevelNum));
    }

    public IEnumerator StartLevel(int levelNum)
    {
        yield return new WaitForSeconds(waitTimeBeforeLevelStarts);

        currentLevel = levels[levelNum - 1];
        currentLevel.StartLevel(spawner);
        currentLevel.onBeatLevel += BeatLevel;
        currentLevel.onLoseLevel += LoseLevel;
        levelText.text = "Level " + currentLevelNum;
        if (crosshairs != null) crosshairs.SetActive(true);
    }

    public void RetryLevel()
    {
        scoreManager.ResetScore();
        StartLevel(currentLevelNum);
    }

    public void NextLevel()
    {
        if(currentLevelNum < levels.Length)
            currentLevelNum++;

        StartCoroutine(StartLevel(currentLevelNum));
    }

    public void BeatLevel()
    {
        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shootingUI.SetActive(false);

        // If last level, then show winGame
        if (currentLevelNum == levels.Length)
        {
            winGameUI.SetActive(true);
            finalScoreText_game.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
        }
        else
        {
            winLevelUI.SetActive(true);
            finalScoreText_win.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
        }
    }

    public void LoseLevel()
    {
        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseLevelUI.SetActive(true);
        shootingUI.SetActive(false);

        finalScoreText_lose.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
    }

    private void EndShootingGallery()
    {
        playerInside = false;
        player.EnableMovement();

        boothCam.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        shootingUI.SetActive(false);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayAmbient();

        levels[currentLevelNum - 1].StopAllCoroutines();
        ScoreManager.instance.ResetScore();

        player.StopUsingGun();
    }
}
