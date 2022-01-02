using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

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
    public PlayerController player;
    public CameraController mainCamera;

    private bool gameStarted = false;

    private Material skyMaterial;
    private LevelCurve levelCurve;
    private LevelTile nextLevelStart;
    private LevelTile lastTile;
    private float curveChangeTimer = 10;

    private static readonly int ColorHorizon = Shader.PropertyToID("_ColorHorizon");
    private static readonly float transitionDuration = 5;

    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

        instance = this;
        levelCurve = GetComponent<LevelCurve>();

        skyMaterial = new Material(RenderSettings.skybox);
        RenderSettings.skybox = skyMaterial;
    }

    void Start()
    {
        RenderSettings.fogColor = levels[0].skyColor;
        skyMaterial.SetColor(ColorHorizon, levels[0].skyColor);
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            roadCamRoot.Translate(Vector3.forward * (speed * Time.deltaTime));

            if (tilesRoot.childCount < 4)
                SpawnTile();

            curveChangeTimer -= Time.deltaTime;
            if (curveChangeTimer <= 0)
            {
                curveChangeTimer = Random.Range(5, 15);
                levelCurve.curveTarget = currentLevel.GetRandomCurve();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                SpawnObstacle(currentLevel.obstacles[0],Random.Range(0,4),2);
            }
        }
    }

    public void StartGame()
    {
        gameStarted = true;
        speed = 50;
        LoadLevel(0);
    }

    public void LoadLevel(int level = 0)
    {
        currentLevelIndex = level;
        currentLevel = levels[currentLevelIndex];
        curveChangeTimer = transitionDuration+5;
        levelCurve.curveTarget = Vector3.zero;
        musicPlayer.clip = currentLevel.music;

        SpawnTile(true);

        StartCoroutine(LevelTimer());
    }

    IEnumerator LevelTimer()
    {
        yield return new WaitForSeconds(3);
        levelIntroText.DisplayText(currentLevel.displayName, currentLevel.distance);

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

        Debug.Log("Level " + (currentLevelIndex+1) +" Started !");

        yield return new WaitForSeconds(currentLevel.duration);

        StartCoroutine(UtilityCoroutines.FadeVolume(musicPlayer, 0, 2, true));

        mainCamera.SetCombatView(false);
        levelCurve.curveTarget = Vector3.zero;
        curveChangeTimer = 100;
        yield return new WaitForSeconds(2f);

        LoadLevel(currentLevelIndex+1);
    }

    public void SpawnTile(bool start = false)
    {
        LevelTile tilePrefab = start ? currentLevel.startTile : currentLevel.tiles[Random.Range(0, currentLevel.tiles.Length)];

        LevelTile tile = Instantiate(tilePrefab, tilesRoot);
        tile.levelManager = this;
        if(lastTile)
            tile.transform.localPosition = lastTile.transform.localPosition + new Vector3(0, 0, 500);
        lastTile = tile;
    }

    public void SpawnObstacle(ObstacleBase obstacle, float position, float time = 5)
    {
        ObstacleBase obstacleInst = Instantiate(obstacle, new Vector3(0, 0, 10000), Quaternion.identity, lanesRoot);
        obstacleInst.time = time;
        obstacleInst.position = Mathf.Lerp(leftLane.localPosition.x, rightLane.localPosition.x, position/3.0f);
    }

    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
        AudioEffectsController.instance.SetAudioSpeed(speed);
        AudioEffectsController.instance.SetLowPassEffect(1-speed);
    }

    public void SetGameSpeed(float speed, float duration, AnimationCurve curve = null)
    {
        StartCoroutine(UtilityCoroutines.FadeTimeSpeed(speed, duration, curve));
        AudioEffectsController.instance.SetAudioSpeed(speed, duration, curve);
        AudioEffectsController.instance.SetLowPassEffect(1- speed, duration, curve);
    }

    public void Death()
    {
        StartCoroutine(UtilityCoroutines.FadeTimeSpeed(0, 2));
        AudioEffectsController.instance.SetAudioSpeed(0, 2);
        AudioEffectsController.instance.SetLowPassEffect(1, 2);
        gameStarted = false;
    }
}
