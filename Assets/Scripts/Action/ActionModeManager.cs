using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public enum ActionFase
{
    GO,
    BACK,
    CLEARED,
    GAMEOEVR,
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
                if (thief.hp.GetValue() <= 0)
                {
                    GameOver();
                }
                timer.Update();
                break;
        }
    }

    public void GameOver()
    {
        isEnd = true;
        bgm.PlayGameover();
        actionFase = ActionFase.GAMEOEVR;
        StopMode();
        Debug.Log("gameover");
    }

    public void GameClear()
    {
        isEnd = true;
        bgm.PlayGameclear();
        actionFase = ActionFase.CLEARED;
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
            thief.FaseChange();
        }));
        
    }

    public int GetActionCollectRate()
    {
        return fieldObjectsManager.GetCollectRate();
    }

    public ActionFase GetActionFase() {
        return actionFase;
    }

}
