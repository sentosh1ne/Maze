using UnityEngine;
using System.Collections;
using System;

public class Zombie : EnemyAI,IFlippable
{
    public void Flip()
    {
        if (currentDirection.x + transform.position.x - transform.position.x > 0)
        {
           
            transform.localScale = new Vector3(-1,1,1);
        }

        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        Flip();
    }

}
