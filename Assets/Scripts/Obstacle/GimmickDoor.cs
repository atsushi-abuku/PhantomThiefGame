using TMPro;
using UnityEngine;

public class GimmickDoor : GimmickObstacle, IGimmickObstacle
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FaseChange()
    {
        
    }

    public void InvokeGimmick()
    {
    }

    public void VisualizeGimmick()
    {
        visualizeFlg = true;
        this.gameObject.SetActive(false);
    }

    public void ShowHint()
    {
        
    }
}
