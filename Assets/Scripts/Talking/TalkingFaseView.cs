using System.Collections.Generic;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class TalkingFaseView : MonoBehaviour
{
    GameObject leftTalker;
    GameObject rightTalker;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] List<TextMeshProUGUI> choiceList;
    
    [SerializeField] TalkFaseManager talkFaseManager;
    private TalkerLine talkerLine;
    private List<TalkerLine> choices;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        //leftTalker = Resources.Load("Prefabs/" + talkFaseManager.GetTalkers()[0]) as GameObject;
        //rightTalker = Resources.Load("Prefabs/" + talkFaseManager.GetTalkers()[1]) as GameObject;

    }

    private void ResetChoiceList()
    {
        foreach (TextMeshProUGUI choice in choiceList)
        {
            choice.gameObject.SetActive(false);
            choice.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetChoiceList();
        talkerLine = talkFaseManager.GetTalkerLine();
        choices = talkFaseManager.GetChoices();
        for(int i = 0; i < choices.Count; i++)
        {
            choiceList[i].gameObject.SetActive(true);
            choiceList[i].text = choices[i].sentence; 
        }
        choiceList[talkFaseManager.GetChoiceId()].color = Color.red; 
        textBox.text = talkerLine.sentence;

        if (talkFaseManager.CheckIsEnded())
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        //Destroy(leftTalker);
        //Destroy(rightTalker);
    }
}
