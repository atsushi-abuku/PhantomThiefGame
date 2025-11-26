using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolChaseReturnAttackHP : MonoBehaviour, IGuardMan
{
    public NavMeshAgent agent;
    public Transform player;

    public Vector3 leftPoint;
    public Vector3 rightPoint;
    private bool movingRight = true;

    public float findDistance = 3f;
    public float loseDistance = 4f;
    public float attackDistance = 1f;

    public float patrolSpeed = 2f;
    public float chaseSpeed =  2.5f;

    // HPダメージ方式（プレイヤースクリプト不要）
    public float playerHP = 5f;
    public float attackDamage = 1f;
    public float attackCooldown = 1.2f;
    private float cooldownTimer = 0f;

    private enum State { Patrol, Chase, Return, Attack }
    [SerializeField] private State state = State.Patrol;

    private Vector3 homePosition;

    private void Start()
    {
        agent.updateRotation = false; // scaleを反転させないため自動回転OFF
        homePosition = transform.position;
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Patrol: Patrol(); break;
            case State.Chase: Chase(); break;
            case State.Return: ReturnToHome(); break;
            case State.Attack: Attack(); break;
        }

        LookDirection(); // 回転処理（scaleを触らない）
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;
        Vector3 target = movingRight ? rightPoint : leftPoint;
        SetDestinationOnce(target);

        if (Vector3.Distance(transform.position, target) < 1f)
            movingRight = !movingRight;

        if (Vector3.Distance(transform.position, player.position) < findDistance)
            state = State.Chase;
    }

    void Chase()
    {
        agent.speed = chaseSpeed;
        SetDestinationOnce(player.position);

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < attackDistance)
            state = State.Attack;
        else if (dist > loseDistance)
            state = State.Return;
    }

    void ReturnToHome()
    {
        agent.speed = patrolSpeed;
        SetDestinationOnce(homePosition);

        if (Vector3.Distance(transform.position, homePosition) < 1f)
            state = State.Patrol;
    }

    public void Stanby()
    {
        this.enabled = true;
        this.gameObject.SetActive(true);
    }
    public void Attack()
    {
        agent.isStopped = true;  // 攻撃時停止
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);

        if (cooldownTimer <= 0f)
        {
            playerHP -= attackDamage;
            cooldownTimer = attackCooldown;
            playerHP--;
            player.GetComponent<Thief>().Damage();
            Debug.Log("Player HP: " + playerHP);

        }

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > attackDistance)  // 離れたら再び追跡へ
        {
            agent.isStopped = false;
            state = State.Chase;
        }
    }

    void SetDestinationOnce(Vector3 pos)
    {
        if (!agent.hasPath || agent.destination != pos)
            agent.SetDestination(pos);
        agent.isStopped = false;
    }

    void LookDirection()
    {
        Vector3 dir;

        if (state == State.Attack)
            return; // 攻撃中は LookAt に任せる

        if (state == State.Chase)
            dir = player.position - transform.position;
        else if (state == State.Return)
            dir = homePosition - transform.position;
        else
        {
            Vector3 patrolTarget = movingRight ? rightPoint : leftPoint;
            dir = patrolTarget - transform.position;
        }

        dir.y = 0;
        if (dir.sqrMagnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 8f);
        }
    }
}

