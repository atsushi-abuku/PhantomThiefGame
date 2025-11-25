using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum ActionFase
{
    GO,
    BACK
}

public class ActionModeManager : MonoBehaviour, IGameMode
{
    private Timer timer;
    [SerializeField] Thief thief;
    [SerializeField] float limitTime;
    [SerializeField] ActionFase actionFase;
    [SerializeField] GameObject fieldObjects;
    [SerializeField] CameraFollow cameraFollow;

    private BGM bgm;
    private FieldObjectsManager fieldObjectsManager;
    private bool isEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgm = GetComponent<BGM>();
        fieldObjectsManager = new FieldObjectsManager(fieldObjects);
        fieldObjectsManager.SetTreasureFunc(FaseChange);
        fieldObjectsManager.SetExitFunc(GameClear);
        isEnd = false;
        timer = new Timer(limitTime, GameOver);
        StartMode();
    }

    public void StartMode()
    {
        //Thief‚ÌInput‚ð—LŒø‚É
        thief.EnableInput();
        isEnd = false;
    }

    public void StopMode()
    {
        //Thief‚ÌInput‚ð–³Œø‚É
        thief.DisableInput();
    }

    public bool CheckIsEnd()
    {
        return isEnd;
    }

    // Update is called once per frame
    void Update()
    {
        switch (actionFase)
        {
            case ActionFase.BACK:
                timer.Update();
                break;
        }
    }

    public void GameOver()
    {
        isEnd = true;
        bgm.PlayGameover();
        Debug.Log("gameover");
    }

    public void GameClear()
    {
        isEnd = true;
        bgm.PlayGameclear();
        Debug.Log("gameclear");
    }

    public void FaseChange()
    {
        thief.DisableInput();
        StartCoroutine(bgm.PlayTreasureThenAlert(() =>
        {
            bgm.PlayEscape();
            thief.EnableInput();
            actionFase = ActionFase.BACK;
            fieldObjectsManager.FaseChange();
            cameraFollow.actionFase = actionFase;
        }));
        
    }

    public int GetActionCollectRate()
    {
        return fieldObjectsManager.GetCollectRate();
    }
}
