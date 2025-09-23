using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class StageManager : MonoBehaviour
{
    Stack<IGameMode> modeStack;
    [SerializeField] TalkingModeManager talkingModeManager;
    [SerializeField] ActionModeManager actionModeManager;
    private Dictionary<string, bool> eventFlgs;


    private static StageManager _instance;

    public static StageManager GetInstance()
    {
        return _instance;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _instance = this.GetComponent<StageManager>();
        modeStack = new Stack<IGameMode>();
        modeStack.Push(actionModeManager);
    }

    // Update is called once per frame
    void Update()
    {
        if (modeStack.Peek().CheckIsEnd())
        {
            StopMode();
            modeStack.Pop();
        }
    }

    public void StopMode()
    {
        talkingModeManager.StopMode();
        actionModeManager.StopMode();
    }

    public void ModeChangeTalking(string talkingFileName)
    {
        talkingModeManager.LoadTalkingText(talkingFileName);
        ModeChangeTalking();
    }

    public void ModeChangeTalking()
    {
        StopMode();
        talkingModeManager.StartMode();
        modeStack.Push(talkingModeManager);
        Debug.Log(modeStack.Count);
    }

    public void ModeChangeAction()
    {
        StopMode();
        actionModeManager.StartMode();
        modeStack.Push(actionModeManager);
    }
}
