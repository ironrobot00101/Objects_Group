using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float damage;

    [SerializeField] private bool isEnemyBullet;



    private string targetTag;

    private void Start()
    {
        //this doesn't work because it's 3D
        //transform.LookAt(GameObject.FindWithTag("Player").transform);
    }

    public void SetBullet(float _damage, string _targetTag, float speed, bool _isEnemyBullet)
    {
        this.damage = _damage;
        this.speed = speed;
        this.tag = _targetTag;
        isEnemyBullet = _isEnemyBullet;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && isEnemyBullet == false)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //Debug.Log(enemy.health.GetHealth() + " is health, and " + enemy.name + "is name for this enemy");
            enemy.GetDamage(damage);
            Destroy(gameObject);
        } else if (collision.CompareTag("Player") && isEnemyBullet == true)
        {
            Player player = collision.GetComponent<Player>();
            player.GetDamage(damage);
            Destroy(gameObject);
            
        }
    }
    private void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Damage(IDamageable damageable)
    {
        if (damageable != null)
        {
            damageable.GetDamage(damage);
            //LevelLoader.Score++;
            Destroy(gameObject); 
        }
        Debug.Log("Bullet damaged target");
        //damage target
    }
}
