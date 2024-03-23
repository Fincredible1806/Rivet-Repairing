using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAiController : MonoBehaviour
{

    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Transform attackLocation;
    [SerializeField] private string playerName;
    public GameObject projectile;
    public int health;
    [SerializeField] private GameObject deathParticle;
    public Animator animator;
    [SerializeField] private string attackBool;
    [SerializeField] private string walkBool;
    [SerializeField] private string idleBool;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttack;
    bool alreadyAttacked;
    public float attackSpread;

    [Header("Ranges")]
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find(playerName).transform;
        agent = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {
        RangeChecks();


        // Getting what the enemy should do 
        if (!playerInSightRange && !playerInAttackRange) 
        {
            Patrol();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chasing();
        }

        if (playerInAttackRange && playerInSightRange) 
        {
            Attacking();
        }

    }

    private void RangeChecks()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    private void Patrol()
    {
        //Get a walk point

        if (!walkPointSet)
        {
            SearchForWalkPoint();
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToPoint = transform.position - walkPoint;

        //When walkpoint is reached

        if (distanceToPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchForWalkPoint()
    {
        // Calcing random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        animator.SetBool(idleBool, false);
        animator.SetBool(walkBool, true);
        animator.SetBool(attackBool, false);
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        animator.SetBool(idleBool, true);
        animator.SetBool(walkBool, false);
        animator.SetBool(attackBool, true);

        // enemy doesn't move when in range
        agent.SetDestination(transform.position);

        //Ememy looks at player
        transform.LookAt(player.transform.position, transform.up);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, attackLocation.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack * Time.deltaTime);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(deathParticle).transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
