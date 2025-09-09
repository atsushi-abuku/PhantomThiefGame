using Unity.VisualScripting;
using UnityEngine;

public class MoveFloor : MonoBehaviour, IGimmickObstacle
{
    [SerializeField] Transform beginTransform;
    [SerializeField] Transform endTransform;
    [SerializeField] GimmickTrigger trigger;
    [SerializeField] float speed;
    [SerializeField] GimmickType type;
    Vector3 moveVec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger.SetInvokeGimmickFunc(InvokeGimmick);
        moveVec = (beginTransform.position - endTransform.position)/ 240f * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        beginEndPositionPutBack();
    }

    private void Move()
    {
        if(Vector3.Dot(endTransform.position - transform.position, beginTransform.position - transform.position) > 0)
        {
            moveVec *= -1;
        }
        this.transform.position += moveVec;
    }

    private void beginEndPositionPutBack()
    {
        beginTransform.position -= moveVec;
        endTransform.position -= moveVec;
    }


    public void FaseChange()
    {
        trigger.Stanby();
    }

    public void InvokeGimmick()
    {
        GimmickFuncGenerator.GetInstance().Generate(type)(this.gameObject);
        moveVec = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Thief")
        {
            Transform topParent = collision.transform;
            while (topParent.parent != null)
            {
                topParent = topParent.parent;
                Debug.Log(topParent);
            }
            topParent.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Thief")
        {
            Transform topParent = collision.transform;
            while (topParent.parent != this.transform && topParent.parent != null)
            {
                topParent = topParent.parent;
            }
            topParent.parent = null;
        }
    }
}
