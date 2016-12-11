using UnityEngine;
using System.Collections;
using System;

public class Mummy : EnemyAI,IFlippable {

    protected override void Start ()
    {
        moveSpeed *= 2;
        base.Start();
	}

    public void Flip()
    {
        if (currentDirection.x + transform.position.x - transform.position.x > 0)
        {

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        Flip();
    }
}
