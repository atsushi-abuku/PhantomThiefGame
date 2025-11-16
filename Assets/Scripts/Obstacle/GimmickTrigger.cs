using Unity.VisualScripting;
using UnityEngine;
public delegate void InvokeGimmickFunc();

public class GimmickTrigger : MonoBehaviour
{

    InvokeGimmickFunc invokeGimmickFunc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<SphereCollider>().enabled = false; //もとはfalse
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInvokeGimmickFunc(InvokeGimmickFunc invokeGimmickFunc)
    {
        this.invokeGimmickFunc = invokeGimmickFunc;
    }

    public void Stanby()
    {
        this.GetComponent<SphereCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Thief")
        {
            //Debug.Log("hiit");
            invokeGimmickFunc();
        }
    }
}
