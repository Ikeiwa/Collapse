using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public int startLevel = 0;
    public LevelData[] levels;
    public Transform tilesRoot;
    public Transform lanesRoot;
    public Transform leftLane;
    public Transform rightLane;
    public Transform roadCamRoot;
    public AudioMixer audioMixer;
    public AudioSource musicPlayer;
    public AudioSource effectsPlayer;
    public LevelIntroText levelIntroText;
    public float speed { get; private set; }
    public int currentLevelIndex { get; private set; }
    public LevelData currentLevel { get; private set; }
    [Space]
    public PauseManager pauseManager;
    public Animator gameOverAnim;
    public Animator victoryAnim;
    public PlayerController player;
    public CameraController mainCamera;
    public int maxObtainLevel = 0;

    private bool gameStarted = false, levelcontentRunning = false, dead = false;

    /// <summary>
    /// Time in the level's perspective. Starts at 0 when a level starts, and increments like any timer.
    /// </summary>
    private float LocaleLevelTime = 0f;

    private Material skyMaterial;
    private LevelCurve levelCurve;
    private LevelTile nextLevelStart;
    private LevelTile lastTile;
    private float curveChangeTimer = 10;

    private static readonly int ColorHorizon = Shader.PropertyToID("_ColorHorizon");
    private static readonly float transitionDuration = 3.5f;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        levelCurve = GetComponent<LevelCurve>();

        skyMaterial = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyMaterial;
        maxObtainLevel = PlayerPrefs.GetInt("MaxLevel", 0);
    }

    void Start()
    {
        currentLevel = levels[0];
        RenderSettings.fogColor = levels[0].skyColor;
        skyMaterial.SetColor(ColorHorizon, levels[0].skyColor);
    }

    void Update()
    {
        if (gameStarted)
        {
            roadCamRoot.Translate(Vector3.forward * (speed * Time.deltaTime));

            curveChangeTimer -= Time.deltaTime;
            if (curveChangeTimer <= 0)
            {
                curveChangeTimer = Random.Range(5, 15);
                levelCurve.curveTarget = currentLevel.GetRandomCurve();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                SpawnObstacle(currentLevel.obstacles[0], Random.Range(0, 4), 2);
            }
        }

        if (tilesRoot.childCount < 4)
            SpawnTile();
    }

    private void FixedUpdate()
    {
        if (levelcontentRunning)
            LocaleLevelTime += Time.fixedDeltaTime;
        else 
            LocaleLevelTime = 0f;
        if (levelcontentRunning) {
            currentLevel.GetContent().Trigger(LocaleLevelTime);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        speed = 50;
        player.enabled = true;
        player.Reset();
        pauseManager.enabled = true;
        dead = false;
        LoadLevel(startLevel);
    }

    public void StopGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameStarted = false;
        speed = 0;
        player.enabled = false;
        levelcontentRunning = false;
        LocaleLevelTime = 0;
        Gamefield.instance.ClearGamefield();
        pauseManager.UnPause();
        pauseManager.enabled = false;
        StopAllCoroutines();
        if (dead)
        {
            dead = false;
            StartCoroutine(UtilityCoroutines.FadeTimeSpeed(1, 2));
            AudioEffectsController.instance.SetAudioSpeed(1, 2);
            AudioEffectsController.instance.SetLowPassEffect(0, 2);
        }
        StartCoroutine(EndLevelAnim(false));
    }

    public void RestartGame(int level = 0)
    {
        EventSystem.current.SetSelectedGameObject(null);
        pauseManager.enabled = true;
        levelcontentRunning = false;
        LocaleLevelTime = 0;
        Gamefield.instance.ClearGamefield();
        player.Reset();
        StopAllCoroutines();
        if (dead)
        {
            dead = false;
            Time.timeScale = 1;
            AudioEffectsController.instance.SetAudioSpeed(1);
            AudioEffectsController.instance.SetLowPassEffect(0);
        }

        if (level == -1)
            currentLevelIndex--;
        else
            currentLevelIndex = level-1;
        StartCoroutine(EndLevelAnim(true,true));
    }

    public void LoadLevel(int level = 0,bool fastload = false)
    {
        currentLevelIndex = level;
        currentLevel = levels[currentLevelIndex];
        currentLevel.LoadContent();
        curveChangeTimer = transitionDuration + 5;
        levelCurve.curveTarget = Vector3.zero;
        musicPlayer.clip = currentLevel.music;

        if (level > maxObtainLevel)
        {
            maxObtainLevel = level;
            PlayerPrefs.SetInt("MaxLevel", maxObtainLevel);
        }

        SpawnTile(true);

        StartCoroutine(LoadLevelAnim(fastload));
    }

    private IEnumerator LoadLevelAnim(bool fastload = false)
    {
        if (!fastload)
        {
            yield return new WaitForSeconds(3);
            levelIntroText.DisplayText(currentLevel.displayName, currentLevel.distance);
        }

        Color baseColor = RenderSettings.fogColor;

        float timer = 0;
        while (timer < 2)
        {
            Color newColor = Color.Lerp(baseColor, currentLevel.skyColor, timer / 2.0f);
            RenderSettings.fogColor = newColor;
            skyMaterial.SetColor(ColorHorizon, newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        RenderSettings.fogColor = currentLevel.skyColor;
        skyMaterial.SetColor(ColorHorizon, currentLevel.skyColor);

        mainCamera.SetCombatView(true);

        yield return new WaitForSeconds(transitionDuration);
        musicPlayer.volume = 1;
        musicPlayer.Play();

        Debug.Log("Level " + (currentLevelIndex + 1) + " Started !");
        levelcontentRunning = true;
    }

    private IEnumerator EndLevelAnim(bool loadNext = true, bool fastLoad = false)
    {
        StartCoroutine(UtilityCoroutines.FadeVolume(musicPlayer, 0, 2, true));

        levelCurve.curveTarget = Vector3.zero;
        curveChangeTimer = 100;

        if(fastLoad)
            yield return new WaitForSeconds(0.01f);
        else
        {
            mainCamera.SetCombatView(false);
            yield return new WaitForSeconds(2f);
        }

        if (loadNext)
        {
            if (currentLevelIndex <= levels.Length - 2)
                LoadLevel(currentLevelIndex + 1, fastLoad);
            else
            {
                victoryAnim.SetTrigger("Show");
                Gamefield.instance.ClearGamefield();
                Debug.Log("Warning : trying to load in level " + (currentLevelIndex + 1) + ", skipping call (out of bounds, max = " + (levels.Length - 1) + ")");
            }
        }

    }

    /// <summary>
    /// Loads the next level, including animations. <i>Not an influka reference.</i><br/>
    /// If called on the last level, this only does the level ending animation and doesn't load an inexistant stage.
    /// </summary>
    public void NextLevel()
    {
        levelcontentRunning = false;
        StartCoroutine(EndLevelAnim());
    }

    public void SpawnTile(bool start = false)
    {
        LevelTile tilePrefab = start ? currentLevel.startTile : currentLevel.tiles[Random.Range(0, currentLevel.tiles.Length)];

        LevelTile tile = Instantiate(tilePrefab, tilesRoot);
        tile.levelManager = this;
        if (lastTile)
            tile.transform.localPosition = lastTile.transform.localPosition + new Vector3(0, 0, 500);
        lastTile = tile;
    }

    public ObstacleBase SpawnObstacle(ObstacleBase obstacle, float position, float time = 5)
    {
        ObstacleBase obstacleInst = Instantiate(obstacle, new Vector3(0, 0, 10000), Quaternion.identity, lanesRoot);
        obstacleInst.time = time;
        obstacleInst.position = Mathf.Lerp(leftLane.localPosition.x, rightLane.localPosition.x, position / 3.0f);
        return obstacleInst;
    }

    public ObstacleBase SpawnObstacleRandom(float position, float time = 5)
    {
        if (currentLevel.obstacles.Length == 0)
            return null;

        ObstacleBase selectedObstacle = currentLevel.obstacles[Random.Range(0, currentLevel.obstacles.Length)];
        return SpawnObstacle(selectedObstacle, position, time);
    }

    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
        AudioEffectsController.instance.SetAudioSpeed(speed);
        AudioEffectsController.instance.SetLowPassEffect(1 - speed);
    }

    public void SetGameSpeed(float speed, float duration, AnimationCurve curve = null)
    {
        StartCoroutine(UtilityCoroutines.FadeTimeSpeed(speed, duration, curve));
        AudioEffectsController.instance.SetAudioSpeed(speed, duration, curve);
        AudioEffectsController.instance.SetLowPassEffect(1 - speed, duration, curve);
    }

    public void Death()
    {
        dead = true;
        StartCoroutine(UtilityCoroutines.FadeTimeSpeed(0, 2));
        AudioEffectsController.instance.SetAudioSpeed(0, 2);
        AudioEffectsController.instance.SetLowPassEffect(1, 2);
        pauseManager.UnPause();
        pauseManager.enabled = false;
        gameOverAnim.SetTrigger("Show");
    }

    
}
