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
    public float attackRange = 2f;
    public float waitTime = 2f;
    public float attackCooldown = 2f;
    public int attackDamage = 1;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float waitCounter = 0f;
    private float attackTimer = 0f;
    private Vector3 startPos;
    private Rigidbody rb;
    private bool movingRight = true;

    private bool isAttacking = false;

    private enum State {Stanby, Chase, Attack, Return}
    private State currentState = State.Stanby;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;

        if (patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[0].position);
    }

    // Update is called once per frame

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        attackTimer += Time.deltaTime;

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

                else if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else
                {
                    Chase();
                }
                break;

            case State.Attack:
                AttackLogic(distanceToPlayer);
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
        if (patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                GoToNextPatrolPoint();
                waitCounter = 0f;
            }
        }
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
        if (isAttacking) return; // 攻撃中は移動しない
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }
    void AttackLogic(float distanceToPlayer)
    {
        // プレイヤーが離れたら再び追跡
        if (distanceToPlayer > attackRange + 0.5f)
        {
            isAttacking = false;
            agent.isStopped = false;
            currentState = State.Chase;
            return;
        }

        // 攻撃間隔処理
        if (!isAttacking && attackTimer >= attackCooldown)
        {
            StartAttack();
        }

    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;
        agent.isStopped = true; // 完全停止
    }

    // 攻撃アニメーションの最後に呼ぶ（Animation Event）
    public void EndAttack()
    {
        isAttacking = false;
        agent.isStopped = false;
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

