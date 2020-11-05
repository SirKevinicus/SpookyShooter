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

    public GameObject interactUI;

    // REFERENCES
    public Player player;
    private Camera boothCam;
    private ScoreManager scoreManager;
    public Gun scifiGun;
    private TargetSpawner spawner;

    // STATE
    [Header("State")]
    private GalleryLevel currentLevel;
    public int currentLevelNum;
    private bool beatLevel = false;
    public bool playerInside = false;
    public bool playerInRange = false;
    public int startingAmmo;

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

        // GUN
        scifiGun = GetComponentInChildren<Gun>();
        AmmoHolder scifiAmmoHolder = new AmmoHolder(AmmoTypes.scifi, scifiGun.maxAmmoHolderAmt, scifiGun.startAmmoHolderAmt);
        scifiGun.Initialize(scifiAmmoHolder);
        player.PickUpAmmoHolder(scifiAmmoHolder);

        boothCam = GetComponentInChildren<Camera>();
        boothCam.gameObject.SetActive(false);

        scoreManager = FindObjectOfType<ScoreManager>();
        spawner = GetComponentInChildren<TargetSpawner>();


        shootingUI.SetActive(false);
        beatLevelUI.SetActive(false);
        loseLevelUI.SetActive(false);
        winGameUI.SetActive(false);
        interactUI.SetActive(false);

        currentLevelNum = startingLevel;
        LoadLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            player = other.GetComponent<Player>();
            playerInRange = true;
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            player = null;
            playerInRange = false;
            interactUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) & playerInRange)
        {
            if (!playerInside)
                StartShootingGallery();
            else
                EndShootingGallery();
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
        interactUI.SetActive(false);

        player.UseGun(scifiGun);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayCircusMusic();

        StartCoroutine(StartLevel());

        onStartGallery?.Invoke();
    }

    private void NextLevel()
    {
        if(currentLevelNum < levels.Length) currentLevelNum++;
        LoadLevel();
    }

    private void LoadLevel()
    {
        currentLevel = levels[currentLevelNum - 1];
        currentLevel.onBeatLevel += BeatLevel;
        currentLevel.onLoseLevel += LoseLevel;
        levelText.text = "Level " + currentLevelNum;
    }

    public IEnumerator StartLevel()
    {
        // TARGETS
        spawner.DeleteTargets();

        // UI
        if (crosshairs != null) crosshairs.SetActive(true);
        Cursor.visible = false;
        shootingUI.SetActive(true);

        startingAmmo = scifiGun.GetTotalAmmo();

        scoreManager.ResetScore();
        beatLevel = false;


        yield return new WaitForSeconds(waitTimeBeforeLevelStarts);

        currentLevel.StartLevel(spawner);
        scifiGun.canShoot = true;
    }

    public void RetryLevel()
    {
        scoreManager.ResetScore();
        loseLevelUI.SetActive(false);

        scifiGun.SetAmmo(startingAmmo);
        player.UpdateGunInfo();

        StartCoroutine(StartLevel());
    }

    public void BeatLevel()
    {
        beatLevel = true;
        onBeatLevel?.Invoke();

        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        shootingUI.SetActive(false);
        startingAmmo = 0; // spend the ammo if you win the game
        scifiGun.canShoot = false;

        // If last level, then show winGame
        if (currentLevelNum == levels.Length)
        {
            winGameUI.SetActive(true);
            finalScoreText_game.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " POINTS";
        }
        else
        {
            beatLevelUI.SetActive(true);
            finalScoreText_win.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " POINTS";
        }

        NextLevel();
    }

    public void LoseLevel()
    {
        if (crosshairs != null) crosshairs.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        loseLevelUI.SetActive(true);
        shootingUI.SetActive(false);

        scifiGun.canShoot = false;

        finalScoreText_lose.text = "" + scoreManager.score + "/" + levels[currentLevelNum - 1].maxScore + " TARGETS HIT";
    }

    public void EndShootingGallery()
    {
        playerInside = false;
        player.EnableMovement();

        if (!beatLevel) scifiGun.SetAmmo(startingAmmo);
        boothCam.gameObject.SetActive(false);

        // UI
        Cursor.lockState = CursorLockMode.Locked;
        shootingUI.SetActive(false);
        beatLevelUI.SetActive(false);
        interactUI.SetActive(true);

        // AUDIO
        BackgroundMusic music = FindObjectOfType<BackgroundMusic>();
        if (music != null) music.PlayAmbient();

        levels[currentLevelNum - 1].StopAllCoroutines();
        scoreManager.ResetScore();

        player.StopUsingGun();

        onEndGallery?.Invoke();
    }
}
