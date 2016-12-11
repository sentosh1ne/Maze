using UnityEngine;
using System.Collections;
using Pathfinding;
using System;
using Random = UnityEngine.Random;

/// <summary>
/// Base class for AI behavior in the game
/// </summary>
public class EnemyAI : MonoBehaviour
{

    #region Variables

    public float moveSpeed = 1f;
    public Transform target;

    //Update rate for pathfinding
    public float updateRate = 0.5f;

    //How fast we change directions while randomly moving
    public float directionChangeRate = 1f;
    [HideInInspector]
    public Path path;
    [HideInInspector]
    public bool pathEnded = false;

    //Min. distance to calculate next waypoint
    public float nextWaypointDistance = 5;

    protected bool isChasing = false;
    protected Rigidbody2D rgBody;
    protected Animator animator;
    protected Seeker seeker;
    protected ScoreManager scoreManager;

    //Current waypoint in pathfinding vector list
    private int currentWaypoint = 0;

    //Current direction for a random movement
   protected  Vector3 currentDirection;
    #endregion

    public void RandomMovement()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        Ray ray = new Ray(transform.position, randomDirection);
        RaycastHit hit;

        //Check if something is in the way
        bool isBlocked = Physics.Raycast(ray, out hit, 0.1f);

        if (isBlocked == false)
        {
            currentDirection = randomDirection;
            rgBody.velocity = currentDirection * moveSpeed;
            animator.SetTrigger("Moved");
        }

        else
        {
            if (isTerrain(hit))
            {
                currentDirection = Random.insideUnitCircle;
                return;
            }
            rgBody.velocity = currentDirection * moveSpeed;
            animator.SetTrigger("Moved");
        }
    }

    bool isTerrain(RaycastHit hit)
    {
        bool isTerrain = hit.collider.tag == "Wall";

        return isTerrain;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //Create new closest path to the сhosen target
    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(updateRate);
        StartCoroutine(UpdatePath());
    }

    // Move object through waypoints
    void ManagePath()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (pathEnded)
            {
                return;
            }

            pathEnded = true;
            return;
        }

        pathEnded = false;

        //Get direction to move based on next waypoint
        Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        direction *= moveSpeed;
        currentDirection = direction;
        rgBody.velocity = direction;
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (dist < nextWaypointDistance)
        {
            currentWaypoint++;
            return;
        }

    }

    protected virtual void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        InvokeRepeating("RandomMovement", 0.5f, directionChangeRate);

        if (scoreManager.score >= 20)
        {
            StartCoroutine(UpdatePath());
        }
    }


    void FixedUpdate()
    {
        if (scoreManager.score == 20 && isChasing == false)
        {
            CancelInvoke("RandomMovement");
            Invoke("Start", 0.1f);
            isChasing = true;
        }

        if (scoreManager.score >= 20 && isChasing)
        {
            ManagePath();
        }

    }

}
