using UnityEngine;
public delegate void StolenFunc();

public class Treasure : MonoBehaviour
{
    StolenFunc stolenFunc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStolenFunc(StolenFunc stolenFunc)
    {
        this.stolenFunc = stolenFunc;
    }

    private void OnCollisionEnter(Collision collision)
    {
        stolenFunc();
        Destroy(this.gameObject);
    }
}
