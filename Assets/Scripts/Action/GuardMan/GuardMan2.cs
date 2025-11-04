using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardMan2 : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;    //Patrol point
    public float patrolSpeed = 2f;
    public float patrolDistance = 3f;
    public float detectionRange = 3f;   //Enemy sight
    public float lostRange = 5f;        //Enemy sight of the enemy loses the player
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float waitCounter = 0f;
    private Vector3 startPos;
    private Rigidbody rb;
    private bool movingRight = true;

    private enum State {Stanby, Chase, Return}
    private State currentState = State.Stanby;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        switch (currentState)
        {
            case State.Stanby:
                if (distanceToPlayer < detectionRange)
                {
                    currentState = State.Chase;
                }
                else
                {
                     Stanby();
                }
                break;

            case State.Chase:
                if (distanceToPlayer > lostRange)
                {
                    currentState = State.Return;
                }
                else
                {
                    Chase();
                }
                break;
            case State.Return:
                if (Vector3.Distance(transform.position, startPos) < 0.5f)
                {
                    currentState = State. Stanby;
                    GoToNextPatrolPoint();
                }
                else
                {
                    ReturnToStart();
                }
                break;
        }

    }

    void  Stanby()
    {
        float move = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector3(move * patrolSpeed, rb. linearVelocity.x);

        // パトロール範囲の端で方向転換
        if (movingRight && transform.position.x > startPos.x + patrolDistance)
            movingRight = false;
        else if (!movingRight && transform.position.x < startPos.x - patrolDistance)
            movingRight = true;

    }

    void GoToNextPatrolPoint()
    {
        float move = movingRight ? 1 : -1;
        rb.linearVelocity = new Vector3(move * patrolSpeed, rb.linearVelocity.x);

        // パトロール範囲の端で方向転換
        if (movingRight && transform.position.x > startPos.x + patrolDistance)
            movingRight = false;
        else if (!movingRight && transform.position.x < startPos.x - patrolDistance)
            movingRight = true;
    }

    void Chase()
    {
        agent.SetDestination(player.position);
    }

    void ReturnToStart()
    {
        agent.SetDestination(startPos);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lostRange);
    }
}

