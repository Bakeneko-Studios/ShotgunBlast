using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class enemyFramework : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Basic Info")]
    public float health = 100;
    public float empHealth = 100;
    readonly float maxEmphealth = 100;
    public float empResistance = 0f;
    [SerializeField] private bool stunned;
    public float stunTime = 5f;

    public GameObject healthBar;
    public GameObject empHealthBar;
    private float scale;
    private Transform healthBarFront;
    private Transform empHealthBarFront;
    //public TextMeshProUGUI healthBarText;
    NavMeshAgent agent;
    Transform player;
    public LayerMask whatIsGround, whatIsPlayer,obstuctionMask;
    public Rigidbody rb;

    [Header("Animations (optional)")]//optional
    public Animator anim;
    public string wakeAnimation;
    public string deathAnimation; 
    public string attackAnimation;
    private bool awake = false; // need to run animation first


    [Header("Movement & Attack")]
    public float turnSpeed = 5f;
    bool alreadyAttacked;
    public float attackCooldownTime = 1f;
    public float aimTolerance = 15f;
    float nextFireTime = 0f;
    bool aimed = false;
    public GameObject attackScript; // object that contains the attack script ex. turret or melee, seperated so trigger colliders dont interfere
    public float attackRange;

    //patrol
    [Header("Patrol")]
    public bool canMove = true;
    private Vector3 walkPoint;
    bool walkPointSet;
    public bool alwaysSeePlayer = true;

    //hide if always see player since enemy doesnt need to search
    [HideInInspector] public bool patrol = true;
    [HideInInspector]  public float walkPointRange; // how far can it walk
    [HideInInspector] public float sightRange;
    [HideInInspector] public bool playerInSightRange, playerInAttackRange;
    [HideInInspector] [Range(0,360)] public float viewAngle = 90f;


    IEnumerator waitLife(float life)
    {
        yield return new WaitForSeconds(life);
        Destroy(this.gameObject);
    }
    IEnumerator wakeUp()
    {
        // Play the animation for getting suck in
        anim.Play(wakeAnimation);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        awake = true;
    }
    IEnumerator emp()
    {
        stunned=true;
        yield return new WaitForSeconds(stunTime);
        stunned=false;
    }
    void empHeal()
    {
        if(!stunned && empHealth<maxEmphealth) empHealth++;
    }
    public void setAwake()
    {
        awake = true;
    }

    void Start()
    {
        if (attackScript == null)
            attackScript = this.gameObject;

        if (healthBar != null)
        {
            healthBarFront = healthBar.transform.Find("front");
            scale = healthBarFront.localScale.x / health;
        }
        if(empHealthBar != null)
        {
            empHealthBarFront = empHealthBar.transform.Find("front");
        }
        player = movement.instance.transform;
        if (canMove)
            agent = GetComponent<NavMeshAgent>();
        if (wakeAnimation != "")
        {
            //anim.Play(wakeAnimation);
            StartCoroutine("wakeUp");
        }
        else
        {
            awake = true;
        }
        InvokeRepeating("empHeal",10f,1f);
    }

    public void ChangeHealth(float amount)
    {
        // Change the health by the amount specified in the amount variable

        if (healthBar != null)
            if (healthBar.activeSelf == false)
                healthBar.SetActive(true);

        health += amount;
        if (healthBarFront != null)
            healthBarFront.transform.localScale = new Vector3(health * scale, healthBarFront.transform.localScale.y, healthBarFront.transform.localScale.z);
        //healthBarText.text = health + "/1000";
        if (health <= 0)
        {
            if (healthBar != null)
                healthBar.SetActive(false);
            
            if (deathAnimation != "")
                anim.Play(deathAnimation);
            StartCoroutine("waitLife", 2); // destory object after certain time
            agent.enabled = false;
            enabled = false;
        }

    }
    public void ChangeEMPHealth(float amount)
    {
        if (empHealthBar != null)
            if (empHealthBar.activeSelf == false)
                empHealthBar.SetActive(true);
        
        if(amount>=0) empHealth += amount;
        else empHealth += amount*empResistance;
        if (empHealthBarFront != null)
            empHealthBarFront.transform.localScale = new Vector3(maxEmphealth - (empHealth * scale), empHealthBarFront.transform.localScale.y, empHealthBarFront.transform.localScale.z);
        if (empHealth<=0)
        {
            empHealth=0;
            StartCoroutine(emp());
        }
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (!alwaysSeePlayer)
        {

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, sightRange);


            Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -viewAngle / 2);
            Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, viewAngle / 2);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * sightRange);
            Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * sightRange);

        }
    }


    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    // Update is called once per frame
    void Update()
    {
        if (!awake || stunned) return;

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

        if (!playerInSightRange && !playerInAttackRange && patrol && canMove && !alwaysSeePlayer) Patroling();
        if ((playerInSightRange || alwaysSeePlayer) && !playerInAttackRange && canMove) ChasePlayer();
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
            if (attackAnimation != "")
                anim.Play(attackAnimation);

            attackScript.SendMessageUpwards("attack",player);
            nextFireTime = attackCooldownTime + Time.time;
        }
    }

    
}


#if UNITY_EDITOR
[CustomEditor(typeof(enemyFramework))]
public class enemyFramework_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // for other non-HideInInspector fields
        
        enemyFramework script = (enemyFramework)target;

        // draw checkbox for the bool
        //script.alwaysSeePlayer = EditorGUILayout.Toggle("Always See Player", script.alwaysSeePlayer);

        if (!script.alwaysSeePlayer) // if bool is true, dont show other fields
        {
            script.patrol = EditorGUILayout.Toggle("Patrol?", script.patrol);
            script.walkPointRange = EditorGUILayout.FloatField("Walk Point Range", script.walkPointRange);
            script.sightRange = EditorGUILayout.FloatField("Sight Range", script.sightRange);
            script.viewAngle = EditorGUILayout.FloatField("View Angle", script.viewAngle);
        }
    }
}
#endif