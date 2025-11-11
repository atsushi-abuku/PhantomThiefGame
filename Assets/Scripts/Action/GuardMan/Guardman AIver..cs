using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrolChaseSideStable : MonoBehaviour
{
    [Header("パトロール設定")]
    public float moveDistance = 7f;
    public float speed = 2f;

    [Header("追跡設定")]
    public Transform player;
    public float chaseRange = 3f;
    public float loseRange = 4f;

    private Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 startPos;
    private int direction = 1;
    private bool isChasing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false; // NavMeshAgentは移動させない
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        startPos = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isChasing && distanceToPlayer <= chaseRange)
            isChasing = true;
        else if (isChasing && distanceToPlayer > loseRange)
            isChasing = false;

        if (isChasing)
            ChasePlayer();
        else
            Patrol();
    }

    void Patrol()
    {
        Vector3 newPos = transform.position + Vector3.right * direction * speed * Time.deltaTime;
        rb.MovePosition(newPos);

        float distance = transform.position.x - startPos.x;
        if (Mathf.Abs(distance) >= moveDistance)
        {
            direction *= -1;
            Flip(direction);
        }
    }

    void ChasePlayer()
    {
        // 経路だけNavMeshに計算させて、自分で移動
        agent.SetDestination(player.position);

        if (agent.path.corners.Length > 1)
        {
            Vector3 target = agent.path.corners[1];
            Vector3 dir = (target - transform.position).normalized;
            rb.MovePosition(transform.position + dir * speed * Time.deltaTime);

            Flip(dir.x > 0 ? 1 : -1);
        }
    }

    void Flip(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, loseRange);
    }
}
