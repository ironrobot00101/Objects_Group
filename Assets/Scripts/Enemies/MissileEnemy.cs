using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy
{
    [SerializeField] private float attackTime;

    [SerializeField] protected float bulletSpeed = 10;

    [SerializeField] protected Bullet bulletPreFab;

    [SerializeField] protected float bulletDamage;

    private bool tooCloseToPlayer;

    protected override void Start()
    {

        base.Start();
        health = new Health(200, 0, 200);

        weapon = new Weapon("Missile Enemy Weapon", bulletDamage, bulletSpeed);
        tooCloseToPlayer = false;
    }

    protected void Awake()
    {
        StartCoroutine(ShootMissile(attackTime));
    }
    protected override void Update()
    {
        base.Update();
        //move while not in the player perimeter
        if (!tooCloseToPlayer)
        {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            transform.right = direction;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player_Perimeter"))
        {
            //have entered the perimeter and need to stop moving towards player
            tooCloseToPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player_Perimeter"))
        {
            //have entered the perimeter and need to stop moving towards player
            tooCloseToPlayer = false;
        }
    }
    public void SetMissileEnemy(float _attackTime)
    {
        attackTime = _attackTime;
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
        //move the enemy
        Debug.Log("");
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public override void Shoot()
    {
        //shoot the player

    }

    public override void Attack(float interval)
    {
        //Attack the enemy

        ShootMissile(interval);

    }

    private IEnumerator ShootMissile(float _interval)
    {
        while (gameObject != null)
        {
            yield return new WaitForSecondsRealtime(_interval);
            weapon.ShootMissile(bulletPreFab, "Player", GetComponent<MissileEnemy>());
        }
    }

    public override void Die()
    {
        base.Die();
        ScoreManager.score += 50;
    }


    

}
