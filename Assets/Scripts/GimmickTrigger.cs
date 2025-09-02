using Unity.VisualScripting;
using UnityEngine;

public class GimmickTrigger : MonoBehaviour
{
    public delegate void GimmickFunc();
    GimmickFunc gimmickFunc;
    Vector3 triggerRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGimmickTrigger(GimmickFunc gimmickFunc, Vector3 triggerRange)
    {
        this.gimmickFunc = gimmickFunc;
        this.AddComponent<SphereCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        gimmickFunc();
    }
}
