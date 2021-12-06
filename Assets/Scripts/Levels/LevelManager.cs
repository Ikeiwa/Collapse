using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public Transform tilesRoot;
    public Transform roadCamRoot;
    public AudioSource musicPlayer;
    public float speed { get; private set; }
    public int currentLevel { get; private set; }

    private bool gameStarted = false;

    private LevelCurve levelCurve;
    private LevelTile nextLevelStart;
    private LevelTile lastTile;
    private float curveChangeTimer = 0;

    private void Awake()
    {
        levelCurve = GetComponent<LevelCurve>();
    }

    void Start()
    {
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
                levelCurve.curveTarget = levels[currentLevel].GetRandomCurve();
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
        currentLevel = 0;
        musicPlayer.clip = levels[currentLevel].music;
        musicPlayer.Play();

        RenderSettings.fogColor = levels[currentLevel].skyColor;
        RenderSettings.skybox.SetColor("_ColorHorizon",levels[currentLevel].skyColor);
    }

    public void SpawnTile()
    {
        LevelTile tile = Instantiate(levels[currentLevel].tiles[Random.Range(0, levels[currentLevel].tiles.Length)],tilesRoot);
        tile.levelManager = this;
        if(lastTile)
            tile.transform.localPosition = lastTile.transform.localPosition + new Vector3(0, 0, 500);
        lastTile = tile;
    }
}
