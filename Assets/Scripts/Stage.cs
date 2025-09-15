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
    [SerializeField] Thief thief;
    [SerializeField] float limitTime;
    [SerializeField] StageFase stageFase;
    [SerializeField] GameObject fieldObjects;
    private Dictionary<string, bool> eventFlgs;
    private Field field;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        field = new Field(fieldObjects);
        field.SetTreasureFunc(FaseChange);

        timer = new Timer(limitTime, GameOver);
        stageFase = StageFase.GO;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stageFase)
        {
            case StageFase.BACK:
                Debug.Log(timer.GetRemainingTime());
                timer.Update();
                break;
        }
    }

    public void GameOver()
    {
        Debug.Log("gameover");
    }

    public void FaseChange()
    {
        stageFase = StageFase.BACK;
        field.FaseChange();
    }
}
