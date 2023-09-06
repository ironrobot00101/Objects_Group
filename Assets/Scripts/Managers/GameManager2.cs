
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    [Header("Game Enemies")]
    [SerializeField] private GameObject meleeEnemyPreFab;
    [SerializeField] private GameObject shootingEnemyPreFab;
    [SerializeField] private GameObject missileEnemyPreFab;
    //powerups and items
    [SerializeField] private GameObject shootingPowerupPreFab;
    [SerializeField] private GameObject bombPreFab;
    //
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private Transform[] powerupSpawnPoints;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;
    [SerializeField] private float powerupSpawnRate;
    [SerializeField] private float bombSpawnRate;

    private GameObject tempEnemy;
    private GameObject tempPowerup;
    private bool isEnemySpawning;

    static float shootingEnemyBulletSpeed = 3.03f;
    static float missileEnemyBulletSpeed = 7.14f;
    static float shootingEnemyDamage = 3.03f;
    static float missileEnemyDamage = 7.14f;

    private Weapon meleeWeapon = new Weapon("Melee", 10, 0);
    private Weapon shootingWeapon = new Weapon("Shooting Enemy Weapon", shootingEnemyDamage, shootingEnemyBulletSpeed);
    private Weapon missileWeapon = new Weapon("Missile Enemy Weapon", missileEnemyDamage, missileEnemyBulletSpeed);

    //Singleton

    private static GameManager2 instance;

    //this is used if any information from the game manager needs to be accessed
    public static GameManager2 GetInstance()
    {
        
        return instance;
    }

    //this is a tool that prevents additional copies of the game manager to be created
    void SetSingleton()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {
        isEnemySpawning = true;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnBomb());
        //start the main theme music
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayMusicAudio("music_main");
    }

    private void Update()
    {
        
    }

    //Create a random enemy out of the screen

    void CreateEnemy()
    {
        tempEnemy = Instantiate(meleeEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = meleeWeapon;

        
    }

    void CreateShootingEnemy()
    {
        //spawn shooting
        tempEnemy = Instantiate(shootingEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon = shootingWeapon;
        tempEnemy.GetComponent<ShootingEnemy>().SetShootingEnemy(5f);
    }

    void CreateMissileEnemy()
    {
        //spawn shooting
        tempEnemy = Instantiate(missileEnemyPreFab);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
        tempEnemy.GetComponent<Enemy>().weapon =  missileWeapon;
        tempEnemy.GetComponent<MissileEnemy>().SetMissileEnemy(2f);
    }

    IEnumerator SpawnEnemy()
    {
        while (isEnemySpawning)
        {
            CreateEnemy();
            CreateShootingEnemy();
            CreateMissileEnemy();
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (isEnemySpawning)
        {
            tempPowerup = Instantiate(shootingPowerupPreFab);
            tempPowerup.transform.position = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Length)].position;
            yield return new WaitForSeconds(powerupSpawnRate);
        }
    }

    IEnumerator SpawnBomb()
    {
        while (isEnemySpawning)
        {
            tempPowerup = Instantiate(bombPreFab);
            tempPowerup.transform.position = powerupSpawnPoints[Random.Range(0, powerupSpawnPoints.Length)].position;
            yield return new WaitForSeconds(bombSpawnRate);
        }
    }



}
