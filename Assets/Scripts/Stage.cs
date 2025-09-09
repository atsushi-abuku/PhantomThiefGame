using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

enum StageFase
{
    GO,
    BACK
}

public class Stage : MonoBehaviour
{
    private Timer timer;
    [SerializeField] float limitTime;
    [SerializeField] StageFase stageFase;
    private List<IGimmickObstacle> gimmickObstacles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new Timer(limitTime, FaseChange);
        stageFase = StageFase.GO;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaseChange()
    {
        stageFase = StageFase.BACK;
        foreach(IGimmickObstacle gObstacle in gimmickObstacles)
        {
            gObstacle.FaseChange();
        }
    }
}
