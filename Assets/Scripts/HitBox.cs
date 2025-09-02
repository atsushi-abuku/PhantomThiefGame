using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Thief")
        {
            //other.GetComponent<Thief>().Damage();
        }
    }

    public void SetHitRange(Vector3 hitRange)
    {
        GetComponent<BoxCollider>().size = hitRange;
    }
}
