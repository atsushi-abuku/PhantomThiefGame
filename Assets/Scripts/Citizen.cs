using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour, ITalker
{
    [SerializeField] List<IGimmickObstacle> gimmickObstacles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gimmickObstacles = new List<IGimmickObstacle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk()
    {

    }

    public void DisableTalk()
    {
        Destroy(gameObject);
    }
}
