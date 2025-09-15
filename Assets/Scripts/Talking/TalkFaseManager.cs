using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

class Choice
{
    public TalkerLine talkerLine;
    public string tag;

    public Choice(string choiceText) {
        string[] choiceDatum = choiceText.Split(" ");
        talkerLine = new TalkerLine(choiceDatum[0]);
        tag = choiceDatum[1];
    }
}

public class TalkFaseManager : MonoBehaviour
{
    [SerializeField] Thief thief;
    private StringReader reader;
    private List<Choice> thiefChoices;
    private int choiceId;
    private string line;
    private bool isEnd;
    private TalkerLine talkerLine;
    string[] talkers;

    private TalkingInput talkingInput;
    public TalkFaseManager(string stageName)
    {

    }

    private void Start()
    {
        choiceId = 0;
        talkerLine = new TalkerLine();
        thiefChoices = new List<Choice>();

        isEnd = true;

        talkingInput = new TalkingInput();
        talkingInput.process.decide.started += ctx => {
            if (thiefChoices.Count > 0) DecideChoice();
            else Next();

            Debug.Log(line);
            Debug.Log(talkerLine.name + ":" + "'" + talkerLine.sentence + "'");
        };

        talkingInput.process.up.started += ctx =>
        {
            ChangeChoiceId(false);
        };

        talkingInput.process.down.started += ctx =>
        {
            ChangeChoiceId(true);
        };

        talkingInput.Enable();
        StartTalking("sample");
    }

    public void StartTalking(string talkingFileName)
    {
        Debug.Log(Resources.Load<TextAsset>(talkingFileName));
        TextAsset talkingText = Resources.Load<TextAsset>(talkingFileName);
        reader = new StringReader(talkingText.text);
        isEnd = false;
        talkingInput.Enable();

        line = reader.ReadLine();
        talkers = line.Split(",");
        Next();
    }

    private void Finish()
    {
        isEnd = true;
        talkingInput.Disable();
    }

    public string[] GetTalkers()
    {
        return talkers;
    }

    public TalkerLine GetTalkerLine()
    {
        return talkerLine;
    }

    public List<TalkerLine> GetChoices()
    {
        List<TalkerLine> choicesText = new List<TalkerLine>();
        foreach(Choice choice in thiefChoices)
        {
            choicesText.Add(choice.talkerLine);
        }
        return choicesText;
    }

    public int GetChoiceId()
    {
        return choiceId;
    }

    private void Next()
    {
        Debug.Log("Next");
        if (reader.Peek() == -1) {
            Finish();
            return;
        }
        line = reader.ReadLine();
        if (line == "")
        {
            Next();
            return;
        }
        
        if (line[0] == '@')
        {
            TalkingTagParser(line);
        }
        else
        {
            TalkingLineParser(line);
        }
    }

    private void ChangeChoiceId(bool isDown)//isDown‚ªtrue‚Å+1,false‚Å-1‚·‚éD
    {
        if(thiefChoices.Count > 0)
        {
            if(isDown)choiceId = (choiceId + 1) % thiefChoices.Count;
            else choiceId = (choiceId + thiefChoices.Count - 1) % thiefChoices.Count;   
        }
        Debug.Log(choiceId);
    }

    private void DecideChoice()
    {
        talkerLine = thiefChoices[choiceId].talkerLine;
        JumpReadLine(thiefChoices[choiceId].tag);
        Debug.Log(thiefChoices[choiceId].tag);
        Debug.Log(line);
        thiefChoices.Clear();
        Next();
    }

    public bool CheckIsEnded()
    {
        return isEnd;
    }

    private void TalkingTagParser(string tag)
    {
        string[] talkingTag = tag.Split(" ");
        switch (talkingTag[0])
        {
            case "@end":
                Finish();
                return;
            case "@ThiefType":
                
                break;
            case "@choice":
                line = reader.ReadLine();
                while (line != "@choiceEnd")
                {
                    thiefChoices.Add(new Choice(line));
                    line = reader.ReadLine();
                }
                break;
            case "@eventFlg":

                break;
            case "@jump":
                JumpReadLine(talkingTag[1]);
                Next();
                break;
            default:
                Next();
                break;
        }
    }

    private void TalkingLineParser(string line)
    {
        if (line.Split(",").Length >= 3)
        {
            talkerLine = new TalkerLine(line);
        }
        else
        {
            Next();
        }
    }

    private void JumpReadLine(string talkingTag)
    {
        //Debug.Log(talkingTag);
        while (line != talkingTag && reader.Peek() != -1)
        {
            line = reader.ReadLine();
        }
    }
}
