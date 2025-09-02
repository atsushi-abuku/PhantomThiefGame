using UnityEngine;

public class Needle : MonoBehaviour, IGimmickObstacle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeGimmick()
    {
        GameObject hitboxObject = HitBoxGenerator.GetInstance().Generate(transform.position,new Vector3(1,1,1));
        hitboxObject.transform.parent = transform;
    }
}
