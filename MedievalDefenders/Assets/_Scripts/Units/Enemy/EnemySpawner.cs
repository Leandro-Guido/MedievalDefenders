using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave attributes")]
    [SerializeField] private GameObject [] prefabEnemies;
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    // evento inimigo destruido
    public static UnityEvent onEnemyDestroy = new();

    // objeto para conter inimigos
    [Header("Enemies")]
    [SerializeField] private GameObject enemies;

    // variaveis para controle da wave
    private int _currentWave = 1;
    private float _timeSinceLastSpawn;
    private int _enemiesAlive;
    private int _enemiesLeftToSpawn;
    private bool _isSpawning = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        _isSpawning = false;
        _currentWave = 1;
    }

    private void Update()
    {
        if (!_isSpawning) return;
        _timeSinceLastSpawn += Time.deltaTime;
        if (_timeSinceLastSpawn >= (1 / enemiesPerSecond) && _enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
        }
        if (_enemiesAlive == 0 && _enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
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
        if (_currentWave % 2 == 0)
            enemy = 0;
        else
            enemy = 1;

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
        if (_isSpawning == true) return;
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }

    public void DebugVars(string title) {
        print(title
        + "\n\t_currentWave: " + _currentWave
        + "\n\t_timeSinceLastSpawn: " + _timeSinceLastSpawn
        + "\n\t_enemiesAlive: " + _enemiesAlive
        + "\n\t_enemiesLeftToSpawn: " + _enemiesLeftToSpawn
        + "\n\t_isSpawning : " + _isSpawning);
    }

    /**
     * formula que calcula a quantidade de inimigos por wave
     */
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(_currentWave, difficultyScalingFactor));
    }
}
