using TMPro;
using UnityEngine;

public class GimmickFloor : GimmickObstacle, IGimmickObstacle
{
    [SerializeField] GimmickType type;
    [SerializeField] TextMeshProUGUI hintText;
    GimmickFunc gimmickFunc;
    Transform placedTransform;
    Vector3 beforeFramePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger.SetInvokeGimmickFunc(InvokeGimmick);
        switch (type)
        {
            case GimmickType.RISE:
                hintText.text = "Å™";
                break;
            case GimmickType.FALL:
                hintText.text = "Å´";
                break;
            case GimmickType.LEFT:
                hintText.text = "Å©";
                break;
            case GimmickType.RIGHT:
                hintText.text = "Å®";
                break;
            case GimmickType.VANISH:
                hintText.text = "è¡";
                hintText.fontSize = 36;
                break;
        }
        gimmickFunc = GimmickFuncGenerator.GetInstance().Generate(GimmickType.NONE);
        hintText.enabled = false;
        placedTransform = null;
        beforeFramePosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        beforeFramePosition = this.transform.position;
        gimmickFunc(this.gameObject); 
        if (placedTransform != null) placedTransform.position += this.transform.position - beforeFramePosition;
    }

    public void FaseChange()
    {
        trigger.Stanby();
    }

    public void InvokeGimmick()
    {
        gimmickFunc = GimmickFuncGenerator.GetInstance().Generate(type);
    }

    public void VisualizeGimmick()
    {
        visualizeFlg = true;
        hintText.enabled = true;
    }

    public void ShowHint()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Foot")
        {
            placedTransform = other.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Foot")
        {
            placedTransform = null;
        }
    }
}
