using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public int current = 100;
    public int max = 20;

    private Animator animator;

    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public GameObject currentCollectible;
    public float speed = 1.0f;

    private int currentWaypoint;
    private float distanceFromTargetWaypoint;
    private Transform[] waypoints = null;

    // Use this for initialization
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        pointA = GameObject.Find("Point").transform;
        pointB = GameObject.Find("Point (1)").transform;
        pointC = GameObject.Find("Point (2)").transform;
        waypoints = new Transform[3]
            {
                pointA,
                pointB,
                pointC
            };
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if there are collectibles in the scene
        currentCollectible = GameObject.FindWithTag("Collectible");

        float nextX;
        float nextZ;
        Vector3 nextTarget;

        if (currentCollectible != null)
        {
            animator.SetBool("isBallInScene", true);
            nextX = currentCollectible.transform.position.x;
            nextZ = currentCollectible.transform.position.z;
            animator.SetFloat("targetX", nextX);
            animator.SetFloat("targetZ", nextZ);
        }
        else
        {
            animator.SetBool("isBallInScene", false);
            nextX = waypoints[currentWaypoint].transform.position.x;
            nextZ = waypoints[currentWaypoint].transform.position.z;
            animator.SetFloat("targetX", nextX);
            animator.SetFloat("targetZ", nextZ);
        }

        nextTarget = new Vector3(nextX, 1, nextZ);

        float step = speed * Time.deltaTime;
        distanceFromTargetWaypoint = Vector3.Distance(nextTarget, transform.position);
        animator.SetFloat("distanceFromWaypoint", distanceFromTargetWaypoint);
        transform.position = Vector3.MoveTowards(transform.position, nextTarget, step);
        if (animator.GetFloat("distanceFromWaypoint") < 0.001f)
        {
            SetNextPoint();
        }
    }

    public void SetNextPoint()
    {
        switch (currentWaypoint)
        {
            case 0:
                currentWaypoint = 1;
                break;
            case 1:
                currentWaypoint = 2;
                break;
            case 2:
                currentWaypoint = 0;
                break;
        }
    }
}
