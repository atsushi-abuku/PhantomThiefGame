using UnityEngine;

public class GuardManAnimation : MonoBehaviour
{
    EnemyPatrolChaseReturnAttackHP guardMan;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        guardMan = GetComponent<EnemyPatrolChaseReturnAttackHP>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", guardMan.agent.speed);
        if(guardMan.agent.isStopped || !guardMan.enabled)
        {
            animator.SetFloat("speed", 0);
        }
    }
}
