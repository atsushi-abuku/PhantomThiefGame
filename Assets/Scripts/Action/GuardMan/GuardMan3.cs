using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuardMan3 : MonoBehaviour
{

    //public Transform[] patrolPoints;    //Patrol point
    public float patrolSpeed = 2f;
    public float patrolDistance = 6f;
    public Transform player;
    public float detectionRange = 3f;   //Enemy sight
    public float lostRange = 5f;        //Enemy sight of the enemy loses the player
    public float waitTime = 2f;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private float waitCounter = 0f;
    private Vector3 startPos;
    private Rigidbody rb;
    private int direction = 1;
    private bool isChasing = false;

    //private enum State {Stanby, Chase, Return}
    //private State currentState = State.Stanby;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        agent.speed = patrolSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        startPos = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isChasing && distanceToPlayer <= detectionRange)
        {
            isChasing = true; // 追跡開始
        }
        else if (isChasing && distanceToPlayer > lostRange)
        {
            isChasing = false; // 見失ったらパトロールへ戻る
            agent.ResetPath();
        }

        // --- 行動処理 ---
        if (isChasing)
        {
            Chase();
        }
        else
        {
            Stanby();
        }
    }

    void Chase()
    {
        agent.SetDestination(player.position);
        // 横向きモデルの向きをプレイヤー方向に合わせる
        float dirX = player.position.x - transform.position.x;
        if (Mathf.Abs(dirX) > 0.1f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(dirX);
            transform.localScale = scale;
        }
    }


    void Stanby()
    {
        {
            if (agent.hasPath) agent.ResetPath();

            // 移動処理
            Vector3 newPos = transform.position + Vector3.right * direction * patrolSpeed * Time.deltaTime;
            rb.MovePosition(newPos);


            // 一定距離を超えたら反転
            float distance = transform.position.x - startPos.x;
            if (Mathf.Abs(distance) >= patrolDistance)
            {
                direction *= -1; // 方向を逆にする

                // 見た目の向きを反転（2Dや横向きモデルの場合）
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x) * direction;
                transform.localScale = scale;
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lostRange);
    }
}
