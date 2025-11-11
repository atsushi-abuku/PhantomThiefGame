using UnityEngine;

public class Needle : GimmickObstacle, IGimmickObstacle
{
    Vector3 basePosition;
    [SerializeField] GameObject hintObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform.position;
        trigger.SetInvokeGimmickFunc(InvokeGimmick);
        hintObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FaseChange()
    {
        trigger.Stanby();
        if (visualizeFlg)
        {
            hintObject.SetActive(true);
        }
    }

    public void InvokeGimmick()
    {
        this.transform.position = basePosition + this.transform.up;
        hintObject.SetActive(false);

    }

    public void VisualizeGimmick()
    {
        visualizeFlg = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Thief")
        {
            other.GetComponent<Thief>().Damage();
        }
    }
}
