using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardMan : MonoBehaviour, IGuardMan
{
    // Reigion 1
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;
    //private Vector3 pos;
    //public float num = 1;

    [SerializeField] HitBoxAttack hitBoxAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //pos = transform.position;

        // （ポイント）マイナスをかけることで逆方向に移動する。
        //transform.Translate(transform.right * Time.deltaTime * 3 * num);

        //if (pos.x > 2)
       // {
         //   num = -1;
        //}
       // if (pos.x < 5)
       // {
       //     num = 1;
       // }
 
    }

    public void Stanby()
    {
        this.enabled = true;
        this.gameObject.SetActive(true);
        agent.SetDestination(target.position);
    }





    public void Attack()
    {

    }
}
 
