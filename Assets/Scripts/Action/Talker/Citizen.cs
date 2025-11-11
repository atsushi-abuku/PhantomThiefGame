using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Citizen : MonoBehaviour, ITalker
{
    [SerializeField] private int id;
    [SerializeField] List<IGimmickObstacle> gimmickObstacles;
    private bool isTalkabled;
    [SerializeField] string TextFileName;

    void Awake()
    {
        gimmickObstacles = new List<IGimmickObstacle>();
        isTalkabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Talk()
    {
        if (isTalkabled)
        {
            StageManager.GetInstance().ModeChangeTalking(this);
        }
    }

    public void TeachGimmick()
    {
        foreach (IGimmickObstacle gimmickObstacle in gimmickObstacles)
        {
            gimmickObstacle.VisualizeGimmick();
        }
    }

    public void DisableTalk()
    {
        isTalkabled = false;
        GetComponent<SphereCollider>().enabled = false;
        gameObject.SetActive(false);
    }

    public string GetTalkingFileName()
    {
        return TextFileName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Thief>() != null)
        {
            other.GetComponent<Thief>().thiefTalker.SetTalkableCharacter(this);
            isTalkabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Thief>() != null)
        {
            isTalkabled = false;
        }
    }
    public void SetGimmickObstacle(GimmickObstacle gimmickObstacle)
    {
        if(gimmickObstacle.GetTakerId() == id)
        {
            gimmickObstacles.Add(gimmickObstacle.GetComponent<IGimmickObstacle>());
        }
    }
}
