using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class enemyFramework : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Health")]
    public float health = 100;

    public GameObject healthBar;
    private float scale;
    private Transform healthBarFront;
    public TextMeshProUGUI healthBarText;


    [Header("basicInfo")]
    NavMeshAgent agent;
    Transform player;
    public LayerMask whatIsGround, whatIsPlayer,obstuctionMask;

    //patrol
    [Header("Patrol")]
    public bool canMove = true;
    public bool patrol = true;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange; // how far can it walk
    public float sightRange, attackRange;
    [HideInInspector]
    public bool playerInSightRange, playerInAttackRange;
    [Range(0,360)]
    public float viewAngle = 90f;


    [Header("Movement")]
    public float turnSpeed = 5f;

    [Header("Attack")]
    bool alreadyAttacked;
    public float attackCooldownTime = 1f;
    public float aimTolerance = 15f;
    float nextFireTime = 0f;
    bool aimed = false;

   

    void Start()
    {
        //healthBarFront = healthBar.transform.Find("front");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (canMove)
            agent = GetComponent<NavMeshAgent>();
        //scale = healthBarFront.localScale.x / health;

    }

    public void ChangeHealth(float amount)
    {
        // Change the health by the amount specified in the amount variable

        if (healthBar.activeSelf == false)
            healthBar.SetActive(true);

        health += amount;

        healthBarFront.transform.localScale = new Vector3(health * scale, healthBarFront.transform.localScale.y, healthBarFront.transform.localScale.z);
        healthBarText.text = health + "/1000";
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);


        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * sightRange);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * sightRange);

    }


    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // check if player is in look angle
        if (playerInSightRange)
        {
            Vector3 directionToTarget = (player.position - transform.position).normalized;
            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(directionToTarget)) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstuctionMask))
                    playerInSightRange = false;
            }
            else
                playerInSightRange = false;
        }

        if (!playerInSightRange && !playerInAttackRange && patrol && canMove) Patroling();
        if (playerInSightRange && !playerInAttackRange && canMove) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) searchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void searchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void turnTowardsPlayer()
    {

        //transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);  // rotate in only 1 direction

        aimed = Quaternion.Angle(transform.rotation, toRotation) < aimTolerance;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);\

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.rotation, turnSpeed*Time.deltaTime); //player controlled
    }

    private void AttackPlayer()
    {
        if (canMove)
            agent.SetDestination(transform.position);
        
        turnTowardsPlayer();
        //gun.GetComponent<Weapon>().EnemyShoot();
        if (nextFireTime < Time.time && aimed)
        {
            this.gameObject.SendMessageUpwards("attack",player);
            nextFireTime = attackCooldownTime + Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
