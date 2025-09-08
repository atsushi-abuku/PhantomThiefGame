using Unity.VisualScripting;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public delegate void GimmickTriggerFunc();
    GimmickTriggerFunc gimmickTriggerFunc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInvokeGimmickFunc(GimmickTriggerFunc gimmickTriggerFunc)
    {
        this.gimmickTriggerFunc = gimmickTriggerFunc;
        this.AddComponent<SphereCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        gimmickTriggerFunc();
    }
}
