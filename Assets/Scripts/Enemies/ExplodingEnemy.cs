using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public override void Move(Vector2 direction, Vector2 target)
    {
        //move the enemy
        Debug.Log("");
    }

    public override void Shoot()
    {
        //shoot the enemy
    }
 
    public override void Attack(float interval)
    {
        //Attack the enemy
    }

    public override void Die()
    {
        //Die, but explode!
    }
}
