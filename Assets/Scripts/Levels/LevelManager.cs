using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }

    public LevelData[] levels;
    public Transform tilesRoot;
    public Transform roadCamRoot;
    public AudioSource musicPlayer;
    public LevelIntroText levelIntroText;
    public float speed { get; private set; }
    public int currentLevelIndex { get; private set; }
    public LevelData currentLevel { get; private set; }
    [Space]
    public PlayerController player;
    public CameraController mainCamera;

    private bool gameStarted = false;

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
    }

    void Start()
    {
        RenderSettings.fogColor = levels[0].skyColor;
        RenderSettings.skybox.SetColor(ColorHorizon, levels[0].skyColor);
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
        }
        
    }

    public void StartGame()
    {
        gameStarted = true;
        speed = 50;
        LoadLevel();
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
            RenderSettings.skybox.SetColor(ColorHorizon, newColor);
            timer += Time.deltaTime;
            yield return null;
        }
        RenderSettings.fogColor = currentLevel.skyColor;
        RenderSettings.skybox.SetColor(ColorHorizon, currentLevel.skyColor);

        yield return new WaitForSeconds(transitionDuration);
        musicPlayer.volume = 1;
        musicPlayer.Play();

        Debug.Log("Level " + (currentLevelIndex+1) +" Started !");

        yield return new WaitForSeconds(currentLevel.duration);

        timer = 0;
        while (timer < 2)
        {
            musicPlayer.volume = 1-(timer / 2.0f);
            timer += Time.deltaTime;
            yield return null;
        }
        musicPlayer.Stop();

        

        levelCurve.curveTarget = Vector3.zero;
        curveChangeTimer = 100;
        yield return new WaitForSeconds(0.01f);

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
}
