using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveFloor : GimmickObstacle, IGimmickObstacle
{
    [SerializeField] Transform beginTransform;
    [SerializeField] Transform endTransform;
    [SerializeField] float speed;
    [SerializeField] GimmickType type;
    [SerializeField] TextMeshProUGUI hintText;

    GimmickFunc gimmickFunc;
    Vector3 moveVec;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger.SetInvokeGimmickFunc(InvokeGimmick);
        moveVec = (beginTransform.position - endTransform.position)/ 240f * speed;

        switch (type)
        {
            case GimmickType.RISE:
                hintText.text = "ª";
                break;
            case GimmickType.FALL:
                hintText.text = "«";
                break;
            case GimmickType.VANISH:
                hintText.text = "Á";
                hintText.fontSize = 36;
                break;
        }
        gimmickFunc = GimmickFuncGenerator.GetInstance().Generate(GimmickType.NONE);
        hintText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ShowHint();
        Move();
        BeginEndPositionPutBack();
        gimmickFunc(this.gameObject);
    }

    private void Move()
    {
        if(Vector3.Dot(endTransform.position - transform.position, beginTransform.position - transform.position) > 0)
        {
            moveVec *= -1;
        }
        this.transform.position += moveVec;
    }

    private void BeginEndPositionPutBack()
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
        gimmickFunc = GimmickFuncGenerator.GetInstance().Generate(type);
        if(type != GimmickType.NONE)
        {
            moveVec = Vector3.zero;
        }
    }

    public void VisualizeGimmick()
    {
        visualizeFlg = true;
        hintText.enabled = true;
    }

    public void ShowHint()
    {
        if (visualizeFlg)
        {

        }
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
