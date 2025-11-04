using UnityEngine;
using UnityEngine.AI;

public class  GuardManvercomplete : MonoBehaviour
{
    public Transform player;              // プレイヤー参照
    public float detectRange = 2f;        // 検知距離
    public float loseRange = 3f;          // 見失い距離
    public float attackRange =  0.5f;
    public float patrolDistance = 5f;     // 左右移動距離
    public float patrolSpeed = 1f;        // パトロール時速度
    public float chaseSpeed = 1.2f;       // 追跡速度
    public float returnSpeed = 1.2f;        // 復帰速度
    public float attackCooldown = 1.2f;  // 攻撃間隔（秒）

    private NavMeshAgent agent;
    private Vector3 startPos;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 targetPos;

    private bool movingRight = true;
    private float lastAttackTime = 0f;

    private enum State { Patrol, Chase, Return, Attack }
    private State currentState = State.Patrol;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        startPos = transform.position;
        leftPos = startPos - transform.right * patrolDistance;
        rightPos = startPos + transform.right * patrolDistance;

        targetPos = rightPos;
        agent.speed = patrolSpeed;
        agent.SetDestination(targetPos);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distanceToPlayer <= detectRange)
                {
                    currentState = State.Chase;
                    agent.speed = chaseSpeed;
                }
                break;

            case State.Chase:
                ChasePlayer(distanceToPlayer);
                break;


            case State.Attack:
                AttackPlayer(distanceToPlayer);
                break;

            case State.Return:
                ReturnToStart();
                if (!agent.pathPending && agent.remainingDistance < 0.2f)
                {
                    // 元の位置に戻ったらパトロール再開
                    currentState = State.Patrol;
                    agent.speed = patrolSpeed;
                    targetPos = rightPos;
                    agent.SetDestination(targetPos);
                }
                break;
        }
    }

    void Patrol()
    {
        // パトロール用スピード
        agent.speed = patrolSpeed;

        // 目的地到達チェック
        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            movingRight = !movingRight;
            targetPos = movingRight ? rightPos : leftPos;
            agent.SetDestination(targetPos);

            // 向きを変える
            FlipTowards(targetPos.x - transform.position.x);
        }
    }

    void ChasePlayer(float distance)
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        // 向きをプレイヤー方向に
        FlipTowards(player.position.x - transform.position.x);

        if (distance <= attackRange)
        {
            // 攻撃状態へ
            currentState = State.Attack;
            agent.isStopped = true; // 攻撃中は停止
        }
        else if (distance > loseRange)
        {
            currentState = State.Return;
            agent.speed = returnSpeed;
            agent.SetDestination(startPos);
        }
    }

    void AttackPlayer(float distance)
    {
        // プレイヤー方向を向く
        FlipTowards(player.position.x - transform.position.x);

        // 攻撃クールダウン
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            // 攻撃範囲外になったら追跡へ戻る
            if (distance > attackRange + 0.5f)
            {
                currentState = State.Chase;
                agent.isStopped = false;
            }
        }


    }

    void ReturnToStart()
    {
        agent.isStopped = false;
        agent.SetDestination(startPos);
        FlipTowards(startPos.x - transform.position.x);

        if (!agent.pathPending && agent.remainingDistance < 0.2f)
        {
            currentState = State.Patrol;
            agent.speed = patrolSpeed;
            targetPos = rightPos;
            agent.SetDestination(targetPos);
        }
    }


    void FlipTowards(float dirX)
    {
        if (dirX == 0) return;
        Vector3 scale = transform.localScale;
        scale.x = dirX > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }



}