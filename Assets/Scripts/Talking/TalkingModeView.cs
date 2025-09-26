using System.Collections.Generic;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class TalkingFaseView : MonoBehaviour
{
    [SerializeField] Vector3 leftTalkerPosition;
    [SerializeField] Vector3 rightTalkerPosition;

    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] List<TextMeshProUGUI> choiceList;
    
    [SerializeField] TalkingModeManager talkingModeManager;
    private TalkerLine talkerLine;
    private List<TalkerLine> choices;
    [SerializeField] Camera uiCamera;
    private List<GameObject> talkerModels;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        talkerModels = new List<GameObject>();
    }

    private void ResetChoiceList()
    {
        foreach (TextMeshProUGUI choice in choiceList)
        {
            choice.gameObject.SetActive(false);
            choice.color = Color.white;
        }
    }

    private void UpdateTalkerModels()
    {
        if(talkerModels.Count == 0)
        {
            string[] talkerNames = talkingModeManager.GetTalkers();
            foreach (string name in talkerNames)
            {
                talkerModels.Add(Resources.Load("Prefabs/TalkerModels" + name) as GameObject);
            }
        }
        //talkerModels[0].transform.position = leftTalkerPosition;
        //talkerModels[1].transform.position = rightTalkerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        uiCamera.enabled = !talkingModeManager.CheckIsEnd();
        if (!uiCamera.enabled)
        {
            talkerModels.Clear();
            return;
        }
        UpdateTalkerModels();
        ResetChoiceList();
        talkerLine = talkingModeManager.GetTalkerLine();
        choices = talkingModeManager.GetChoices();
        for(int i = 0; i < choices.Count; i++)
        {
            choiceList[i].gameObject.SetActive(true);
            if (i == talkingModeManager.GetChoiceId())
            {
                choiceList[i].text = "¨" + choices[i].sentence;
            }
            else choiceList[i].text = choices[i].sentence;
        }
        textBox.text = talkerLine.sentence;
    }

    private void OnDisable()
    {
        //Destroy(leftTalker);
        //Destroy(rightTalker);
    }
}
