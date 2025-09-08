using UnityEngine;

public class Needle : MonoBehaviour, IGimmickObstacle
{
    Vector3 basePosition;
    [SerializeField] GimmickTrigger trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        basePosition = transform.position;
        trigger.SetInvokeGimmickFunc(InvokeGimmick);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FaseChange()
    {
        trigger.Stanby();
    }

    public void InvokeGimmick()
    {
        this.transform.position = basePosition + this.transform.up;
    }
}
