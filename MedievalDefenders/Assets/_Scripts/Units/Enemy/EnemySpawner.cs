using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI waveText;
    [SerializeField] private GameObject enemies;

    [Header("Wave attributes")]
    [SerializeField] private GameObject[] prefabEnemies;
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    public float difficultyScalingFactor = 0.75f;
    public float healthBoostFactor = 0.75f;

    // evento inimigo destruido
    public static UnityEvent onEnemyDestroy = new();

    // variaveis para controle da wave
    [NonSerialized] public int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
    private bool _isSpawning = false;
    public bool autoplay = false;

    public static EnemySpawner main;


    public void ToggleAutoPlay()
    {
        autoplay = !autoplay;
    }

    private void Awake()
    {
        main = this;
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        WriteWave();
        _isSpawning = false;
        _currentWave = 1;
    }

    private void FixedUpdate()
    {
        WriteWave();
        if (!_isSpawning)
        {
            if(autoplay)
            {
                StartWave();
            }
            return;
        }
        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn >= (1 / enemiesPerSecond) && _enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
        }
        if (_enemiesAlive <= 0 && _enemiesLeftToSpawn == 0)
        {
            if(_enemiesAlive < 0)
                _enemiesAlive = 0;
            EndWave();
        }
    }

    public void WriteWave() {
        waveText.text = _currentWave.ToString();
    }

    private void EnemyDestroyed()
    {
        _enemiesAlive--;
    }

    /**
     * spawna um inimigo criando uma instancia no primeiro vertice
     */
    private void SpawnEnemy()
    {
        int enemy;
        if (_enemiesLeftToSpawn <= EnemiesPerWave() * 0.15)
        {
            enemy = 1;
        }
        else
        {
            enemy = 0;
        }

        Instantiate(prefabEnemies[enemy], LevelManager.main.vertices[0].transform.position, Quaternion.identity, enemies.transform);
        _enemiesLeftToSpawn--;
        _enemiesAlive++;
        _timeSinceLastSpawn = 0;
    }

    /**
     *  finaliza uma wave e ja prepara a proxima
     */
    private void EndWave()
    {
        _isSpawning = false;
        _timeSinceLastSpawn = 0f;
        _currentWave++;
    }

    public void StartWave()
    {
        //DebugVars("Start:");
        if (_isSpawning == true) return;
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }

    public void DebugVars(string title)
    {
        print(title
        + "\n\t_currentWave: " + _currentWave
        + "\n\t_timeSinceLastSpawn: " + _timeSinceLastSpawn
        + "\n\t_enemiesAlive: " + _enemiesAlive
        + "\n\t_enemiesLeftToSpawn: " + _enemiesLeftToSpawn
        + "\n\t_isSpawning : " + _isSpawning);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("waves", _currentWave);
    }


    /**
     * formula que calcula a quantidade de inimigos por wave
     */
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(_currentWave, difficultyScalingFactor));
    }
}
