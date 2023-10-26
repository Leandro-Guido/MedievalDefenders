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
    [SerializeField] private float timeBetweenWaves = 3f;
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

    // DEBUG
    private bool _debugWave = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // comeca a primeira wave
        StartCoroutine(StartWave());
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
        StartCoroutine(StartWave());
    }

    /**
     * coroutine que comeca a wave apos o WaitForSeconds()
     */
    private IEnumerator StartWave()
    {
        if(_debugWave) 
            Debug.Log("new wave (" + EnemiesPerWave() + ") incoming in " + timeBetweenWaves + " seconds");
        yield return new WaitForSeconds(timeBetweenWaves);
        _isSpawning = true;
        _enemiesLeftToSpawn = EnemiesPerWave();
    }

    /**
     * formula que calcula a quantidade de inimigos por wave
     */
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(_currentWave, difficultyScalingFactor));
    }
}
