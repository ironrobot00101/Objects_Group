using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : PlayableObject
{
    [SerializeField] private Camera cam;
    [SerializeField] protected float speed;

    [SerializeField] private float weaponDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletPowerupSpeed = 0.5f;
    [SerializeField] private Bullet bulletPreFab;
    [SerializeField] private Bomb bombPreFab;
    [SerializeField] float maxHealthSet;
    [SerializeField] float currentHealthSet;

    //firepoint
    [SerializeField] private Transform firePoint;

    private int currentScore;
    private int highscore = 0;

    [SerializeField]
    private AudioManager audioManager;

    private ScoreManager scoreManager;

    public Action<float> OnHealthUpdate;

    private Rigidbody2D playerRB;

    private bool isPowerupActive;
    private bool isShooting = false;
    public bool _hasBomb = false;

    // bomb stuff
    public bool isActiveInventory;
    [SerializeField] private float bombFuseTime = 2;
    [SerializeField] private float bombShootSpeed;
    private ParticleSystem explosion;
    private void FixedUpdate()
    {

    }

    public void SetPowerupFeatures()
    {
        isPowerupActive = true;
    }

    public void ResetPowerupsToNull()
    {
        isPowerupActive = false;
    }

    private void Awake()
    {
    }

    private void Start()
    {
        health = new Health(maxHealthSet, 0.5f, currentHealthSet);
        playerRB = GetComponent<Rigidbody2D>();
        health.RegenHealth();

        //Set The Player Weapon
        Debug.Log(weaponDamage + "is your wepaon damage");
        weapon = new Weapon("Player Weapon", weaponDamage, bulletSpeed);
        OnHealthUpdate?.Invoke(health.GetHealth());
        Debug.Log("players health is "+health.GetHealth());
        

        //set the score, health, highscore
        //health
        ScoreManager.health = health.GetHealth();
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        //only can be held while powerup is active
        if (Input.GetKey(KeyCode.Mouse0) && isPowerupActive && !isShooting)
        {
            isShooting = true;
            StartCoroutine(ShootHold());
        }
        if (_hasBomb)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("bomb is being shot");
                Shoot(bombPreFab, firePoint);
                _hasBomb = false;
                ScoreManager.bombsInventory--;
            }
        }
    }

    public override void Shoot()
    {
        //where is this supposed to be called from?
        weapon.Shoot(bulletPreFab, this, "Enemy");
        audioManager.PlaySFXAudio("player_laser_shoot");
    }

    public override void Attack(Transform target)
    {

    }

    public override void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        audioManager.PlaySFXAudio("player_hit_bullet");
        Debug.Log(health.GetHealth() + "is the players health");
        //update scoreboard
        ScoreManager.health = health.GetHealth();
        if (Mathf.RoundToInt(health.GetHealth()) <= 0)
        {
            Die();
            if (Mathf.RoundToInt(health.GetHealth()) < 0)
            {
                ScoreManager.health = 0;
            }
        } 
    }

    public IEnumerator ShootHold()
    {
        while (isPowerupActive && Input.GetKey(KeyCode.Mouse0)) 
        {
            yield return new WaitForSeconds(bulletPowerupSpeed);
            weapon.Shoot(bulletPreFab, this, "Enemy");
            audioManager.PlaySFXAudio("player_laser_shoot");
        }
        isShooting = false;
        
    }

    public void Shoot(Bomb _bomb, Transform _firePoint)
    {
        //instantiate,move
        Bomb tempBomb = Instantiate(_bomb, new Vector3(_firePoint.transform.position.x, _firePoint.transform.position.y, -0.11f), _firePoint.transform.rotation);
        tempBomb.isActiveInventory = false;
        tempBomb.thrown = true;
        //play sound
        //audioManager.PlaySFXAudio("bomb_shoot");
        //Debug.Log("Weapon is shooting");
        StartCoroutine(BlowUp(tempBomb));
    }

    public IEnumerator BlowUp(Bomb tempBomb)
    {
        //play fuse sound
        if (_hasBomb)
        {
            explosion = tempBomb.GetComponentInChildren<ParticleSystem>();
            audioManager.PlaySFXAudio("bomb_fuse");
            yield return new WaitForSeconds(bombFuseTime);
            //blowup!
            audioManager.PlaySFXAudio("bomb_explode");
            explosion.Play();
            //kill all enemies
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(g);
            }
            yield return new WaitForSeconds(0.5f);
            Destroy(tempBomb.gameObject);
        }
        
        //inflict damage
    }






}
